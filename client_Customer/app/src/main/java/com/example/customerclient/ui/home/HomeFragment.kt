package com.example.customerclient.ui.home

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
import com.example.customerclient.databinding.FragmentHomeBinding
import com.example.customerclient.ui.MainListener
import com.example.customerclient.ui.common.CreditsInfoRecyclerAdapter
import com.example.customerclient.ui.home.components.AlertDialogWithEditTextConfirmAndDismissButtons
import com.example.customerclient.ui.home.components.BillsInfoRecyclerAdapter
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class HomeFragment : Fragment() {
    private lateinit var binding: FragmentHomeBinding
    private var callback: MainListener? = null

    private val viewModel: HomeViewModel by viewModel()

    override fun onAttach(context: Context) {
        callback = activity as MainListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val mainView = inflater.inflate(R.layout.fragment_home, container, false)
        binding = FragmentHomeBinding.bind(mainView)

        if (savedInstanceState == null) {
            viewModel.getUserBillsAndCreditsInfo()
        }

        lifecycleScope.launch {
            viewModel.uiState.collect { homeState ->
                when (homeState) {
                    is HomeState.Content -> homeFragmentContent(
                        name = homeState.userName,
                        billsInfo = homeState.billsInfo,
                        creditsInfo = homeState.creditsInfo,
                        isFromDatabase = homeState.fromDatabase,

                        onCreateBillClick = { name -> viewModel.createBill(name) },
                    )

                    else -> {}
                }
            }
        }

        binding.switchModeButton.setOnClickListener {
            callback?.swipeTheme()
            viewModel.swipeMode()
        }

        return binding.root
    }

    private fun homeFragmentContent(
        name: String,
        billsInfo: List<BillInfo>,
        creditsInfo: List<CreditShortInfo>,
        isFromDatabase: Boolean,
        onCreateBillClick: (String) -> Unit,
    ) {
        binding.userWelcome.text = if (name == "") "Здравствуйте" else "Здравствуйте,\n$name"

        billsCardInfoContent(billsInfo, onCreateBillClick, isFromDatabase)
        creditsCardInfoContent(creditsInfo, isFromDatabase)
    }

    private fun billsCardInfoContent(
        billsInfo: List<BillInfo>,
        onCreateBillClick: (String) -> Unit,
        isFromDatabase: Boolean,
    ) {
        binding.addBillButton.setOnClickListener {
            if (!isFromDatabase) {
                showCreateBillDialog { name ->
                    onCreateBillClick(
                        name
                    )
                }
            }
        }

        when (billsInfo.size) {
            0 -> {
                binding.billInfoRecyclerView.visibility = View.GONE
                binding.openAllBillsButton.visibility = View.GONE
                binding.createNewBillCard.visibility = View.VISIBLE

                binding.createNewBillCard.setOnClickListener {
                    if (!isFromDatabase) {
                        showCreateBillDialog { name ->
                            onCreateBillClick(
                                name
                            )
                        }
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
                    onBillClick = { billId ->
                        if (!isFromDatabase) {
                            navigateToBillsActivity(billId, "INFO")
                        }
                    }
                )
        }
    }

    private fun creditsCardInfoContent(
        creditsInfo: List<CreditShortInfo>,
        isFromDatabase: Boolean,
    ) {
        if (!isFromDatabase) {
            binding.addCreditButton.setOnClickListener {
                navigateToCreditsActivity("", "CREATE")
            }
        }

        when (creditsInfo.size) {
            0 -> {
                binding.creditInfoRecyclerView.visibility = View.GONE
                binding.openAllCreditsButton.visibility = View.GONE
                binding.createNewCreditCard.visibility = View.VISIBLE

                binding.createNewCreditCard.setOnClickListener {
                    if (!isFromDatabase) {
                        navigateToCreditsActivity("", "CREATE")
                    }
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
                    onCreditClick = { creditId ->
                        if (!isFromDatabase) {
                            navigateToCreditsActivity(creditId, "INFO")
                        }
                    }
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