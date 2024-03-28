package com.example.customerclient.ui.credit.info

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
import com.example.customerclient.databinding.FragmentCreditInfoBinding
import com.example.customerclient.ui.credit.CreditsListener
import com.example.customerclient.ui.credit.info.components.CreditsHistoryRecyclerAdapter
import com.example.customerclient.ui.home.components.AlertDialogWithConfirmAndDismissButtons
import kotlinx.coroutines.flow.collectLatest
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel
import org.koin.core.parameter.parametersOf

class CreditInfoFragment : Fragment() {
    private lateinit var binding: FragmentCreditInfoBinding
    private var callback: CreditsListener? = null

    private val viewModel: CreditInfoViewModel by viewModel { parametersOf(Bundle(), "vm1") }

    override fun onAttach(context: Context) {
        callback = activity as CreditsListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentCreditInfoBinding.inflate(inflater, container, false)
        val root: View = binding.root

        // Список с историей кредита
        val creditHistoryRecyclerView = binding.creditHistoryRecyclerView
        creditHistoryRecyclerView.layoutManager =
            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)

        val adapter = CreditsHistoryRecyclerAdapter()
        context?.let { creditHistoryRecyclerView.adapter = adapter }

        lifecycleScope.launch {
            viewModel.creditsHistoryState.collectLatest {
                adapter.submitData(it)
            }
        }

        lifecycleScope.launch {
            viewModel.uiState.collect { creditInfoState ->
                creditInfoFragmentContent(
                    creditInfoState.info.penalty,
                    creditInfoState.info.amount,
                    onLoanPayment = { viewModel.payOffLoan() },
                    onPenyPayment = {}
                )
            }
        }

        return root
    }

    @SuppressLint("SetTextI18n")
    private fun creditInfoFragmentContent(
        penalty: Int,
        amount: String,
        onLoanPayment: () -> Unit,
        onPenyPayment: () -> Unit
    ) {
        // Кнопка назад
        binding.backCreditInfoButton.setOnClickListener {
            if (callback?.getStartCreditScreenType() == "ALL") findNavController().popBackStack()
            else callback?.backToMainFragment()
        }

        // Сумма кредита
        binding.creditMoneyTitle.text = "$amount $"

        // Кнопка "Внести платёж"
        if (penalty == 0) {
            binding.closeCreditTitle.text = "Внести платёж"
            binding.payOfCreditButton.setOnClickListener {
                showConfirmToLoanPaymentDialog(onLoanPayment)
            }
        }
        // Кнопка "Погасить пени"
        else {
            binding.closeCreditTitle.text = "Погасить пени"
            binding.payOfCreditButton.setOnClickListener {
                showConfirmToPenyPaymentDialog(onPenyPayment)
            }
        }
    }

    private fun showConfirmToLoanPaymentDialog(
        onConfirmClick: () -> Unit,
    ) {
        val dialogWithEditText = AlertDialogWithConfirmAndDismissButtons(
            title = "Вы хотите внести ежедневный платёж?",
            description = "",
            positiveButtonText = "Да",
            negativeButtonText = "Нет",

            onPositiveButtonClick = onConfirmClick,
        )
        val manager = parentFragmentManager
        dialogWithEditText.show(manager, "confirmToCloseBillAlertDialog")
    }

    private fun showConfirmToPenyPaymentDialog(
        onConfirmClick: () -> Unit,
    ) {
        val dialogWithEditText = AlertDialogWithConfirmAndDismissButtons(
            title = "Вы хотите выплатить пени?",
            description = "",
            positiveButtonText = "Да",
            negativeButtonText = "Нет",

            onPositiveButtonClick = onConfirmClick,
        )
        val manager = parentFragmentManager
        dialogWithEditText.show(manager, "confirmToCloseBillAlertDialog")
    }
}