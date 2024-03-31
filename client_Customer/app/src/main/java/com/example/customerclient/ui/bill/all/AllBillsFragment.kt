package com.example.customerclient.ui.bill.all

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.customerclient.databinding.FragmentAllBillsBinding
import com.example.customerclient.ui.bill.BillsListener
import com.example.customerclient.ui.home.components.BillsInfoRecyclerAdapter
import kotlinx.coroutines.flow.collectLatest
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class AllBillsFragment : Fragment() {

    private lateinit var binding: FragmentAllBillsBinding
    private var callback: BillsListener? = null

    val viewModel: AllBillsViewModel by viewModel()

    override fun onAttach(context: Context) {
        callback = activity as BillsListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentAllBillsBinding.inflate(layoutInflater)
        val root: View = binding.root

        if (savedInstanceState == null) {
            viewModel.getBills()
        }

        // Кнопка назад
        binding.backAllBillsButton.setOnClickListener { callback?.backToMainFragment() }

        // Список счетов
        val allBillsRecyclerView = binding.allBillsRecyclerView
        allBillsRecyclerView.layoutManager =
            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)
        lifecycleScope.launch {
            viewModel.uiState.collectLatest {
                if (it is AllBillState.Content) {
                    val adapter = BillsInfoRecyclerAdapter(
                        items = it.items ?: listOf(),
                        onBillClick = { creditId -> navigateToBillInfoFragment(creditId) },
                        onLongClick = { billId ->
                            viewModel.hideBill(billId)
                        }
                    )
                    context?.let { allBillsRecyclerView.adapter = adapter }
                }
            }
        }


        return root
    }

    private fun navigateToBillInfoFragment(billId: String) {
        val action = AllBillsFragmentDirections.actionNavigationAllBillsToNavigationBillInfo(billId)
        findNavController().navigate(action)
    }
}