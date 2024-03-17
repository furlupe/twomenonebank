package com.example.employeeclient.presentation.account.accountinfo.tabs.operations

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.employeeclient.R
import com.example.employeeclient.databinding.FragmentAccountOperationsTabBinding
import com.example.employeeclient.presentation.account.accountinfo.util.AccountInfoListener
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel
import org.koin.core.parameter.parametersOf

class AccountOperationsTabFragment : Fragment() {

    private lateinit var binding: FragmentAccountOperationsTabBinding
    private var callback: AccountInfoListener? = null

    private val viewModel by viewModel<AccountOperationsTabViewModel> {
        parametersOf(callback?.getAccountId())
    }

    override fun onAttach(context: Context) {
        callback = activity as AccountInfoListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_account_operations_tab, container, false)
        binding = FragmentAccountOperationsTabBinding.bind(view)

        val linearLayoutManager = LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)
        binding.rvAccounts.setLayoutManager(linearLayoutManager)

        val adapter = AccountOperationTabAdapter(
            context = context,
            onLoadNextClick = {  },
        )
        binding.rvAccounts.setAdapter(adapter)

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
        fun newInstance(accountId: String) =
            AccountOperationsTabFragment().apply {
                arguments = Bundle().apply {
                    putString("accountId", accountId)
                }
            }
    }
}