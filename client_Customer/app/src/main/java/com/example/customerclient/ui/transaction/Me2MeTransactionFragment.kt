package com.example.customerclient.ui.transaction

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.customerclient.databinding.FragmentTransactionMe2meBinding
import com.example.customerclient.ui.bill.BillsListener
import com.example.customerclient.ui.common.BillsInfoPagingRecyclerAdapter
import kotlinx.coroutines.flow.collectLatest
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel
import org.koin.core.parameter.parametersOf

class Me2MeTransactionFragment : Fragment() {
    private lateinit var binding: FragmentTransactionMe2meBinding
    private var callback: BillsListener? = null

    private val viewModel: Me2MeTransactionViewModel by viewModel { parametersOf(Bundle(), "vm1") }

    override fun onAttach(context: Context) {
        callback = activity as BillsListener
        super.onAttach(context)
    }

    override fun onStart() {
        super.onStart()
        viewModel.getBills()
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentTransactionMe2meBinding.inflate(inflater, container, false)

        lifecycleScope.launch {
            viewModel.uiState.collectLatest {
                when (it) {
                    is Me2MeTransactionState.ChooseBillContent -> {
                        binding.transactionAmount.visibility = View.GONE
                        binding.transactionButton.visibility = View.GONE
                        binding.transactionComment.visibility = View.GONE

                        binding.allBillsRecyclerView.visibility = View.VISIBLE

                        val allCreditsRecyclerView = binding.allBillsRecyclerView
                        allCreditsRecyclerView.layoutManager =
                            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)

                        val adapter = BillsInfoPagingRecyclerAdapter(onBillClick = { creditId ->
                            viewModel.chooseBill(creditId)
                        })
                        context?.let { allCreditsRecyclerView.adapter = adapter }
                        lifecycleScope.launch {
                            viewModel.billsInfoState.collectLatest {
                                adapter.submitData(it)
                            }
                        }
                    }

                    is Me2MeTransactionState.Error -> TODO()
                    Me2MeTransactionState.TransactionContent -> {
                        binding.transactionAmount.visibility = View.VISIBLE
                        binding.transactionButton.visibility = View.VISIBLE
                        binding.transactionComment.visibility = View.VISIBLE

                        binding.allBillsRecyclerView.visibility = View.GONE

                        binding.transactionButton.setOnClickListener {
                            viewModel.me2MeTransaction(
                                amount = binding.transactionAmount.text.toString(),
                                message = binding.transactionComment.text.toString()
                            )
                        }
                    }
                }
            }
        }

        return binding.root
    }
}