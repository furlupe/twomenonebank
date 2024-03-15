package com.example.customerclient.ui.credit.info

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
import com.example.customerclient.ui.bottombar.home.components.AlertDialogWithConfirmAndDismissButtons
import com.example.customerclient.ui.credit.CreditsListener
import com.example.customerclient.ui.credit.info.components.CreditsHistoryRecyclerAdapter
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

        lifecycleScope.launch {
            viewModel.uiState.collect { creditInfoState ->
                creditInfoFragmentContent(
                    creditHistory = creditInfoState.history,
                    onLoanPayment = { viewModel.payOffLoan() }
                )
            }
        }

        return root
    }

    private fun creditInfoFragmentContent(
        creditHistory: List<CreditHistory>,
        onLoanPayment: () -> Unit
    ) {
        // Кнопка назад
        binding.backCreditInfoButton.setOnClickListener {
            if (callback?.getStartCreditScreenType() == "ALL") findNavController().popBackStack()
            else callback?.backToMainFragment()
        }

        // Сумма кредита
        binding.creditMoneyTitle.text = "32489203 ₽"

        // Кнопка "Внести платёж"
        binding.payOfCreditButton.setOnClickListener {
            showConfirmToLoanPaymentDialog(onLoanPayment)
        }

        // Список с историей кредита
        val creditHistoryRecyclerView = binding.creditHistoryRecyclerView
        creditHistoryRecyclerView.layoutManager =
            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)
        context?.let {
            creditHistoryRecyclerView.adapter =
                CreditsHistoryRecyclerAdapter(items = creditHistory)
        }
    }

    private fun showConfirmToLoanPaymentDialog(
        onConfirmClick: () -> Unit,
    ) {
        val dialogWithEditText = AlertDialogWithConfirmAndDismissButtons(
            title = "Вы хотите внести ежемесячный платёж?",
            description = "",
            positiveButtonText = "Да",
            negativeButtonText = "Нет",

            onPositiveButtonClick = onConfirmClick,
        )
        val manager = parentFragmentManager
        dialogWithEditText.show(manager, "confirmToCloseBillAlertDialog")
    }
}