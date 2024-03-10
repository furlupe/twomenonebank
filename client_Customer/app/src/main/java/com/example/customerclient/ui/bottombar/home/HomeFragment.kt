package com.example.customerclient.ui.bottombar.home

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.customerclient.R
import com.example.customerclient.databinding.FragmentHomeBinding
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class HomeFragment : Fragment() {
    private lateinit var binding: FragmentHomeBinding

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val homeViewModel: HomeViewModel by viewModel()

        binding = FragmentHomeBinding.inflate(inflater, container, false)
        val root: View = binding.root

        lifecycleScope.launch {
            homeViewModel.uiState.collect { homeState ->
                val userWelcomeText: TextView = binding.userWelcome
                val name = homeState.userName
                userWelcomeText.text = "Здравствуйте,\n$name"

                val euroAndDollarExchangeRate: TextView = binding.exchangeRate
                val euro = homeState.euroExchangeRate
                val dollar = homeState.dollarExchangeRate
                euroAndDollarExchangeRate.text = "$euro\n$dollar"

                binding.addCreditButton.setOnClickListener {
                    navigateToCreateCreditActivity()
                }

                binding.addBillButton.setOnClickListener {
                    navigateToCreateBillActivity()
                }

                when (homeState.billsInfo.size) {
                    0 -> {
                        binding.billInfoRecyclerView.visibility = View.GONE
                        binding.openAllBillsButton.visibility = View.GONE
                        binding.createNewBillCard.visibility = View.VISIBLE

                        binding.createNewBillCard.setOnClickListener {
                            navigateToCreateBillActivity()
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
                            navigateToAllBillsActivity()
                        }
                    }
                }
                val billInfoRecyclerView = binding.billInfoRecyclerView
                billInfoRecyclerView.layoutManager =
                    LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)
                context?.let {
                    billInfoRecyclerView.adapter =
                        BillsInfoRecyclerAdapter(items = homeState.billsInfo,
                            onBillClick = { billId -> navigateToBillInfoActivity(billId) })
                }

                when (homeState.creditsInfo.size) {
                    0 -> {
                        binding.creditInfoRecyclerView.visibility = View.GONE
                        binding.openAllCreditsButton.visibility = View.GONE
                        binding.createNewCreditCard.visibility = View.VISIBLE

                        binding.createNewCreditCard.setOnClickListener {
                            navigateToCreateCreditActivity()
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
                            navigateToAllCreditsActivity()
                        }
                    }
                }
                val creditInfoRecyclerView = binding.creditInfoRecyclerView
                creditInfoRecyclerView.layoutManager =
                    LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)
                context?.let {
                    creditInfoRecyclerView.adapter =
                        CreditsInfoRecyclerAdapter(items = homeState.creditsInfo,
                            onCreditClick = { creditId -> navigateToCreditInfoActivity(creditId) })
                }
            }
        }
        return root
    }

    private fun navigateToCreateCreditActivity() {
        findNavController().navigate(R.id.action_navigation_home_to_createCreditActivity)
    }

    private fun navigateToAllCreditsActivity() {
        findNavController().navigate(R.id.action_navigation_home_to_allCreditsActivity)
    }

    private fun navigateToCreditInfoActivity(creditId: String) {
        val action =
            HomeFragmentDirections.actionNavigationHomeToCreditInfoActivity(creditId)
        findNavController().navigate(action)
    }

    private fun navigateToCreateBillActivity() {
        findNavController().navigate(R.id.action_navigation_home_to_createBillActivity)
    }

    private fun navigateToAllBillsActivity() {
        findNavController().navigate(R.id.action_navigation_home_to_allBillsActivity)
    }

    private fun navigateToBillInfoActivity(billId: String) {
        val action = HomeFragmentDirections.actionNavigationHomeToBillInfoActivity(billId)
        findNavController().navigate(action)
    }
}