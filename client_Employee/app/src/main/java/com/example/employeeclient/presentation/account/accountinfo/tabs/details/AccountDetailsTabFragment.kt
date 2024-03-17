package com.example.employeeclient.presentation.account.accountinfo.tabs.details

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import com.example.employeeclient.R
import com.example.employeeclient.databinding.FragmentAccountDetailsTabBinding
import com.example.employeeclient.presentation.account.accountinfo.util.AccountInfoListener
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel
import org.koin.core.parameter.parametersOf

class AccountDetailsTabFragment : Fragment() {

    private lateinit var binding: FragmentAccountDetailsTabBinding
    private var callback: AccountInfoListener? = null

    private val viewModel by viewModel<AccountDetailsTabViewModel> {
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
        val view = inflater.inflate(R.layout.fragment_account_details_tab, container, false)
        binding = FragmentAccountDetailsTabBinding.bind(view)

        lifecycleScope.launch {
            viewModel.state.collect { state ->
                if (state == null) {
                    return@collect
                }

                binding.tvAccountName.text = state.name
                binding.tvAmount.text = state.balance.toString()
            }
        }

        return binding.root
    }

    companion object {
        @JvmStatic
        fun newInstance(accountId: String) =
            AccountDetailsTabFragment().apply {
                arguments = Bundle().apply {
                    putString("accountId", accountId)
                }
            }
    }
}