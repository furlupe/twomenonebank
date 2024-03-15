package com.example.customerclient.ui.bottombar.home

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.customerclient.R
import com.example.customerclient.databinding.FragmentHomeBinding
import com.example.customerclient.ui.bottombar.home.components.AlertDialogWithConfirmAndDismissButtons
import com.example.customerclient.ui.common.AlertDialogWithEditTextField
import com.example.customerclient.ui.common.BillsInfoRecyclerAdapter
import com.example.customerclient.ui.common.CreditsInfoRecyclerAdapter
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class HomeFragment : Fragment() {
    private lateinit var binding: FragmentHomeBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val viewModel: HomeViewModel by viewModel()

        binding = FragmentHomeBinding.inflate(inflater, container, false)
        val root: View = binding.root

        lifecycleScope.launch {
            viewModel.uiState.collect { homeState ->
                homeFragmentContent(
                    name = homeState.userName,
                    euro = homeState.euroExchangeRate,
                    dollar = homeState.dollarExchangeRate,
                    billsInfo = homeState.billsInfo,
                    creditsInfo = homeState.creditsInfo,

                    onCreateBillClick = { viewModel.createBill() },
                    onCreateCreditClick = { amountOfCredit -> viewModel.createCredit(amountOfCredit) }
                )
            }
        }
        return root
    }

    private fun homeFragmentContent(
        name: String,
        euro: String,
        dollar: String,
        billsInfo: List<BillInfo>,
        creditsInfo: List<CreditInfo>,
        onCreateBillClick: () -> Unit,
        onCreateCreditClick: (String) -> Unit
    ) {
        binding.userWelcome.text = "Здравствуйте,\n$name"
        binding.exchangeRate.text = "$euro\n$dollar"

        billsCardInfoContent(billsInfo, onCreateBillClick)
        creditsCardInfoContent(creditsInfo, onCreateCreditClick)
    }

    private fun billsCardInfoContent(
        billsInfo: List<BillInfo>,
        onCreateBillClick: () -> Unit,
    ) {
        binding.addBillButton.setOnClickListener { showCreateBillDialog { onCreateBillClick() } }

        when (billsInfo.size) {
            0 -> {
                binding.billInfoRecyclerView.visibility = View.GONE
                binding.openAllBillsButton.visibility = View.GONE
                binding.createNewBillCard.visibility = View.VISIBLE

                binding.createNewBillCard.setOnClickListener { showCreateBillDialog { onCreateBillClick() } }
            }

            1 -> {
                binding.billInfoRecyclerView.visibility = View.VISIBLE
                binding.openAllBillsButton.visibility = View.GONE
                binding.createNewBillCard.visibility = View.GONE
            }

            else -> {
                binding.billInfoRecyclerView.visibility = View.VISIBLE
                binding.openAllBillsButton.visibility = View.VISIBLE
                binding.createNewBillCard.visibility = View.GONE

                binding.openAllBillsButton.setOnClickListener {
                    navigateToBillsActivity("", "ALL")
                }
            }
        }
        val billInfoRecyclerView = binding.billInfoRecyclerView
        billInfoRecyclerView.layoutManager =
            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)
        context?.let {
            billInfoRecyclerView.adapter =
                BillsInfoRecyclerAdapter(
                    items = billsInfo,
                    onBillClick = { billId -> navigateToBillsActivity(billId, "INFO") }
                )
        }
    }

    private fun creditsCardInfoContent(
        creditsInfo: List<CreditInfo>,
        onCreateCreditClick: (String) -> Unit
    ) {
        binding.addCreditButton.setOnClickListener { showCreateCreditDialog(onCreateCreditClick) }

        when (creditsInfo.size) {
            0 -> {
                binding.creditInfoRecyclerView.visibility = View.GONE
                binding.openAllCreditsButton.visibility = View.GONE
                binding.createNewCreditCard.visibility = View.VISIBLE

                binding.createNewCreditCard.setOnClickListener {
                    showCreateCreditDialog(onCreateCreditClick)
                }
            }

            1 -> {
                binding.creditInfoRecyclerView.visibility = View.VISIBLE
                binding.openAllCreditsButton.visibility = View.GONE
                binding.createNewCreditCard.visibility = View.GONE
            }

            else -> {
                binding.creditInfoRecyclerView.visibility = View.VISIBLE
                binding.openAllCreditsButton.visibility = View.VISIBLE
                binding.createNewCreditCard.visibility = View.GONE

                binding.openAllCreditsButton.setOnClickListener {
                    navigateToCreditsActivity("", "ALL")
                }
            }
        }
        val creditInfoRecyclerView = binding.creditInfoRecyclerView
        creditInfoRecyclerView.layoutManager =
            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)
        context?.let {
            creditInfoRecyclerView.adapter =
                CreditsInfoRecyclerAdapter(
                    items = creditsInfo,
                    onCreditClick = { creditId -> navigateToCreditsActivity(creditId, "INFO") }
                )
        }
    }

    private fun showCreateBillDialog(
        onCreateClick: () -> Unit,
    ) {
        val dialog = AlertDialogWithConfirmAndDismissButtons(
            title = "Сберегательный счёт",
            description = "Валюта: Российский рубль\nНачисление процентов: На ежедневный остаток\nВаша ставка: 12%",
            onPositiveButtonClick = onCreateClick,
            positiveButtonText = "Создать",
            negativeButtonText = "Отменить"
        )

        val manager = parentFragmentManager
        dialog.show(manager, "addBillAlertDialog")
    }

    private fun showCreateCreditDialog(
        onCreateClick: (String) -> Unit,
    ) {
        val dialog = AlertDialogWithEditTextField(
            title = "Аннуитетный кредит",
            description = getString(R.string.credit_amount),
            onPositiveButtonClick = onCreateClick,
        )

        val manager = parentFragmentManager
        dialog.show(manager, "addCreditAlertDialog")
    }

    private fun navigateToCreditsActivity(creditId: String, screenCreditType: String) {
        val action =
            HomeFragmentDirections.actionNavigationHomeToCreditsActivity(
                creditId,
                screenCreditType
            )
        findNavController().navigate(action)
    }

    private fun navigateToBillsActivity(billId: String, screenBillType: String) {
        val action =
            HomeFragmentDirections.actionNavigationHomeToBillsActivity(billId, screenBillType)
        findNavController().navigate(action)
    }
}