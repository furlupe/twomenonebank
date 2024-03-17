package com.example.employeeclient.presentation.credit.creditinfo.tabs.operations

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.employeeclient.R
import com.example.employeeclient.common.Constants.USER_ID
import com.example.employeeclient.databinding.FragmentCreditOperationsTabBinding
import com.example.employeeclient.presentation.credit.creditinfo.util.CreditInfoListener
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel
import org.koin.core.parameter.parametersOf

class CreditOperationsTabFragment : Fragment() {

    private lateinit var binding: FragmentCreditOperationsTabBinding
    private var callback: CreditInfoListener? = null

    private val viewModel by viewModel<CreditOperationsTabViewModel> {
        parametersOf(callback?.getCreditId())
    }

    override fun onAttach(context: Context) {
        callback = activity as CreditInfoListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_credit_operations_tab, container, false)
        binding = FragmentCreditOperationsTabBinding.bind(view)

        val linearLayoutManager = LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)
        binding.rvCredits.setLayoutManager(linearLayoutManager)

        val adapter = CreditTabAdapter(
            context = context,
            onLoadNextClick = {  },
        )
        binding.rvCredits.setAdapter(adapter)

        lifecycleScope.launch {
            viewModel.state.collect {
                if (it.currentPage != 1) adapter.removeLoadingFooter()

                adapter.addAll(it.items)
                if (!it.isLastPage) adapter.addLoadingFooter()
            }
        }

        return binding.root
    }

    companion object {
        @JvmStatic
        fun newInstance(userId: String) =
            CreditOperationsTabFragment().apply {
                arguments = Bundle().apply {
                    putString(USER_ID, userId)
                }
            }
    }
}