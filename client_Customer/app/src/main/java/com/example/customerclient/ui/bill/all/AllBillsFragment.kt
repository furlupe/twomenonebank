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
import com.example.customerclient.ui.common.BillsInfoPagingRecyclerAdapter
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

    override fun onStart() {
        super.onStart()
        viewModel.getBills()
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentAllBillsBinding.inflate(layoutInflater)
        val root: View = binding.root

        // Кнопка назад
        binding.backAllBillsButton.setOnClickListener { callback?.backToMainFragment() }

        // Список счетов
        val allCreditsRecyclerView = binding.allBillsRecyclerView
        allCreditsRecyclerView.layoutManager =
            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)

        val adapter = BillsInfoPagingRecyclerAdapter(onBillClick = { creditId ->
            navigateToBillInfoFragment(creditId)
        })
        context?.let { allCreditsRecyclerView.adapter = adapter }

        lifecycleScope.launch {
            viewModel.billsInfoState.collectLatest {
                adapter.submitData(it)
            }
        }


        return root
    }

    private fun navigateToBillInfoFragment(billId: String) {
        val action = AllBillsFragmentDirections.actionNavigationAllBillsToNavigationBillInfo(billId)
        findNavController().navigate(action)
    }
}