package com.example.customerclient.ui.bottombar.home

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.customerclient.databinding.FragmentHomeBinding
import com.example.customerclient.ui.bottombar.home.components.AlertDialogWithEditTextConfirmAndDismissButtons
import com.example.customerclient.ui.bottombar.home.components.BillsInfoRecyclerAdapter
import com.example.customerclient.ui.common.CreditsInfoRecyclerAdapter
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class HomeFragment : Fragment() {
    private lateinit var binding: FragmentHomeBinding

    private val viewModel: HomeViewModel by viewModel()
    override fun onStart() {
        super.onStart()
        viewModel.getUserBillsAndCreditsInfo()
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        binding = FragmentHomeBinding.inflate(inflater, container, false)
        val root: View = binding.root

        lifecycleScope.launch {
            viewModel.uiState.collect { homeState ->
                when (homeState) {
                    is HomeState.Content -> homeFragmentContent(
                        name = homeState.userName,
                        billsInfo = homeState.billsInfo,
                        creditsInfo = homeState.creditsInfo,

                        onCreateBillClick = { name -> viewModel.createBill(name) },
                    )

                    else -> {}
                }
            }
        }
        return root
    }

    private fun homeFragmentContent(
        name: String,
        billsInfo: List<BillInfo>,
        creditsInfo: List<CreditShortInfo>,
        onCreateBillClick: (String) -> Unit,
    ) {
        binding.userWelcome.text = if (name == "") "Здравствуйте" else "Здравствуйте,\n$name"

        billsCardInfoContent(billsInfo, onCreateBillClick)
        creditsCardInfoContent(creditsInfo)
    }

    private fun billsCardInfoContent(
        billsInfo: List<BillInfo>,
        onCreateBillClick: (String) -> Unit,
    ) {
        binding.addBillButton.setOnClickListener {
            showCreateBillDialog { name ->
                onCreateBillClick(
                    name
                )
            }
        }

        when (billsInfo.size) {
            0 -> {
                binding.billInfoRecyclerView.visibility = View.GONE
                binding.openAllBillsButton.visibility = View.GONE
                binding.createNewBillCard.visibility = View.VISIBLE

                binding.createNewBillCard.setOnClickListener {
                    showCreateBillDialog { name ->
                        onCreateBillClick(
                            name
                        )
                    }
                }
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
        creditsInfo: List<CreditShortInfo>,
    ) {
        binding.addCreditButton.setOnClickListener {
            navigateToCreditsActivity("", "CREATE")
        }

        when (creditsInfo.size) {
            0 -> {
                binding.creditInfoRecyclerView.visibility = View.GONE
                binding.openAllCreditsButton.visibility = View.GONE
                binding.createNewCreditCard.visibility = View.VISIBLE

                binding.createNewCreditCard.setOnClickListener {
                    navigateToCreditsActivity("", "CREATE")
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
        onCreateClick: (String) -> Unit,
    ) {
        val dialog = AlertDialogWithEditTextConfirmAndDismissButtons(
            title = "Сберегательный счёт",
            description = "Валюта: Российский рубль\nНачисление процентов: На ежедневный остаток\nВаша ставка: 12%",
            onPositiveButtonClick = onCreateClick,
            positiveButtonText = "Создать",
            negativeButtonText = "Отменить"
        )

        val manager = parentFragmentManager
        dialog.show(manager, "addBillAlertDialog")
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