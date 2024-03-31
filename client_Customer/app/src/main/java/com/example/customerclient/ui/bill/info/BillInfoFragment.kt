package com.example.customerclient.ui.bill.info

import android.annotation.SuppressLint
import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.customerclient.R
import com.example.customerclient.databinding.FragmentBillInfoBinding
import com.example.customerclient.ui.bill.BillsListener
import com.example.customerclient.ui.bill.info.components.BillsHistoryRecyclerAdapter
import com.example.customerclient.ui.common.AlertDialogWithEditTextField
import com.example.customerclient.ui.common.AlertDialogWithTwoButtons
import com.example.customerclient.ui.home.components.AlertDialogWithConfirmAndDismissButtons
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel
import org.koin.core.parameter.parametersOf

class BillInfoFragment : Fragment() {
    private lateinit var binding: FragmentBillInfoBinding
    private var callback: BillsListener? = null

    private val viewModel: BillInfoViewModel by viewModel { parametersOf(Bundle(), "vm1") }

    override fun onAttach(context: Context) {
        callback = activity as BillsListener
        super.onAttach(context)
    }

    override fun onStart() {
        viewModel.openWebSocket()
        super.onStart()
    }

    @SuppressLint("SetTextI18n")
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        binding = FragmentBillInfoBinding.inflate(inflater, container, false)
        val root: View = binding.root

        // Список с историей счёта
        val billHistoryRecyclerView = binding.billHistoryRecyclerView
        billHistoryRecyclerView.layoutManager =
            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)
        val adapter = BillsHistoryRecyclerAdapter()
        context?.let { billHistoryRecyclerView.adapter = adapter }

        lifecycleScope.launch {
            viewModel.billsHistoryState.collect { billHistory ->
                adapter.submitData(billHistory)
            }
        }

        lifecycleScope.launch {
            viewModel.uiState.collect { billInfoState ->
                if (billInfoState is BillInfoState.Content) {
                    billInfoFragmentContent(
                        moneyOnBill = billInfoState.info.balance,
                        billId = billInfoState.info.id,

                        onTopUpBill = { amount -> viewModel.topUpBill(amount) },
                        onChargeBill = { amount -> viewModel.chargeBill(amount) },

                        onConfirmToCloseBillClick = { viewModel.closeBill() }
                    )
                } else if (billInfoState is BillInfoState.NavigateToMainScreen) callback?.backToMainFragment()
            }
        }

        return root
    }

    @SuppressLint("SetTextI18n")
    private fun billInfoFragmentContent(
        moneyOnBill: String,
        billId: String,

        onTopUpBill: (String) -> Unit,
        onChargeBill: (String) -> Unit,

        onConfirmToCloseBillClick: () -> Unit
    ) {
        // Кнопка назад
        binding.backBillInfoButton.setOnClickListener {
            if (callback?.getStartBillScreenType() == "ALL") findNavController().popBackStack()
            else callback?.backToMainFragment()
        }

        // Количество денег на счету
        binding.moneyTitle.text = moneyOnBill

        // Кнопка "Пополнить счёт"
        binding.topUpButton.setOnClickListener {
            showAlertDialogForChargeAndTopUp(
                title = "Пополнить счёт",
                editTextTitleResId = R.string.top_up_amount,
                onPositiveButtonClick = onTopUpBill,
                tag = "topUpAlertDialog"
            )
        }

        // Кнопка "Снять деньги со счёта"
        binding.withdrawMoneyButton.setOnClickListener {
            showAlertDialogForChargeAndTopUp(
                title = "Снять деньги со счёта",
                editTextTitleResId = R.string.withdraw_amount,
                onPositiveButtonClick = onChargeBill,
                tag = "withdrawAlertDialog"
            )
        }

        // Кнопка "Перевести"
        binding.transferBillButton.setOnClickListener {
            showAlertDialogForChoosingTypeOfTransaction(
                title = "Выберите какой перевод хотите совершить",
                onPositiveButtonClick = { navigateToTransactionMe2MeFragment(billId) },
                onNegativeButtonClick = { navigateToTransactionP2PFragment(billId) }
            )
        }

        // Кнопка "Закрыть счёт"
        binding.closeBillButton.setOnClickListener { showConfirmToCloseBillDialog { onConfirmToCloseBillClick() } }
    }

    override fun onPause() {
        viewModel.closeWebSocket()
        super.onPause()
    }

    private fun navigateToTransactionMe2MeFragment(billId: String) {
        val action = callback?.getBillId()
            ?.let {
                BillInfoFragmentDirections.actionNavigationBillInfoToNavigationTransactionMe2Me(
                    billId
                )
            }
        action?.let { findNavController().navigate(action) }
    }

    private fun navigateToTransactionP2PFragment(billId: String) {
        val action = callback?.getBillId()
            ?.let {
                BillInfoFragmentDirections.actionNavigationBillInfoToNavigationTransactionP2P(
                    billId
                )
            }
        action?.let { findNavController().navigate(action) }
    }

    private fun showConfirmToCloseBillDialog(
        onConfirmClick: () -> Unit,
    ) {
        val dialogWithEditText = AlertDialogWithConfirmAndDismissButtons(
            title = "Вы уверены, что хотите закрыть счёт?",
            description = "После закрытия все средства будут переведены на основную карту",

            positiveButtonText = "Да",
            negativeButtonText = "Нет",

            onPositiveButtonClick = onConfirmClick,
        )
        val manager = parentFragmentManager
        dialogWithEditText.show(manager, "confirmToCloseBillAlertDialog")
    }

    private fun showAlertDialogForChargeAndTopUp(
        title: String,
        editTextTitleResId: Int,
        onPositiveButtonClick: (String) -> Unit,
        tag: String
    ) {
        val dialogWithEditText = AlertDialogWithEditTextField(
            title = title,
            description = getString(editTextTitleResId),
            onPositiveButtonClick = onPositiveButtonClick
        )
        val manager = parentFragmentManager
        dialogWithEditText.show(manager, tag)
    }

    private fun showAlertDialogForChoosingTypeOfTransaction(
        title: String,
        onPositiveButtonClick: () -> Unit,
        onNegativeButtonClick: () -> Unit,
    ) {
        val dialogWithEditText = AlertDialogWithTwoButtons(
            title = title,
            onNegativeButtonClick = onNegativeButtonClick,
            onPositiveButtonClick = onPositiveButtonClick
        )
        val manager = parentFragmentManager
        dialogWithEditText.show(manager, tag)
    }
}