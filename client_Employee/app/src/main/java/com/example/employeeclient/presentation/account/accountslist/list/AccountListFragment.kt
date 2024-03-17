package com.example.employeeclient.presentation.account.accountslist.list

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.employeeclient.R
import com.example.employeeclient.databinding.FragmentAccountListBinding
import com.example.employeeclient.presentation.account.accountslist.AccountListListener
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel
import org.koin.core.parameter.parametersOf

class AccountListFragment : Fragment() {

    private lateinit var binding: FragmentAccountListBinding
    private var callback: AccountListListener? = null

    private val viewModel: AccountListViewModel by viewModel {
        parametersOf(callback?.getUserId())
    }

    override fun onAttach(context: Context) {
        callback = activity as AccountListListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_account_list, container, false)
        binding = FragmentAccountListBinding.bind(view)

        binding.tvUserName.text = callback?.getUserName()
        binding.btBan.setOnClickListener {
            viewModel.banUnbanUser()
        }

        val linearLayoutManager = LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)
        binding.rvAccounts.setLayoutManager(linearLayoutManager)

        val adapter = AccountAdapter(
            context = context,
            onLoadNextClick = { /*todo*/ },
            onClick = { id: String ->
                val action = AccountListFragmentDirections.actionAccountListFragmentToAccountInfoActivity(id)
                findNavController().navigate(action)
            }
        )
        binding.rvAccounts.setAdapter(adapter)

        lifecycleScope.launch {
            viewModel.state.collect { state ->
                if (state.currentPage != 1) adapter.removeLoadingFooter()

                adapter.addAll(state.items)
                if (!state.isLastPage) adapter.addLoadingFooter()

                val text = if (state.isBanned) "Unban" else "Ban"
                binding.btBan.text = text
            }
        }

        return binding.root
    }

}