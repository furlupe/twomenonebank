package com.example.employeeclient.presentation.main.users

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.os.bundleOf
import androidx.core.view.isGone
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.employeeclient.R
import com.example.employeeclient.databinding.FragmentUsersBinding
import com.example.employeeclient.presentation.main.users.util.UsersAdapter
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel


class UsersFragment : Fragment() {

    private lateinit var binding: FragmentUsersBinding
    private val viewModel: UsersViewModel by viewModel()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val view = inflater.inflate(R.layout.fragment_users, container, false)
        binding = FragmentUsersBinding.bind(view)

        val linearLayoutManager = LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)
        binding.rvUsers.setLayoutManager(linearLayoutManager)

        val adapter = UsersAdapter(
            context = context,
            onLoadNextClick = { viewModel.loadNextPage() },
            onAccountsClick = { id: String, username: String ->
                val action = UsersFragmentDirections.actionNavigationUsersToAccountListActivity(id, username)
                findNavController().navigate(action)
            },
            onCreditsClick = { id: String, username: String ->
                val action = UsersFragmentDirections.actionNavigationUsersToCreditsListActivity(id, username)
                findNavController().navigate(action)
            }
        )
        binding.rvUsers.setAdapter(adapter)

        lifecycleScope.launch {
            viewModel.state.collect {
                if (it.isLoading) {
                    showLoading()

                    return@collect
                }

                if (it.error.isNotBlank()) {
                    hideLoading()
                    binding.tvError.text = it.error
                    binding.tvError.isGone = false

                    return@collect
                }

                hideLoading()

                if (it.currentPage != 1) adapter.removeLoadingFooter()

                adapter.addAll(it.users)
                if (!it.isLastPage && it.users.isNotEmpty()) adapter.addLoadingFooter()
            }
        }

        return binding.root
    }

    private fun showLoading() {
        binding.tvError.isGone = true
        binding.rvUsers.isGone = true
        binding.loading.show()
    }

    private fun hideLoading() {
        binding.tvError.isGone = true
        binding.rvUsers.isGone = false
        binding.loading.hide()
    }

    private fun navigateToUserAccounts(id: String, username: String) {
        val bundle = bundleOf("userId" to id, "userName" to username)
    }

    private fun navigateToUserCredits(id: String, username: String) {
        val bundle = bundleOf("userId" to id, "userName" to username)
    }

    private fun navigateToUserInfo(id: String, username: String) {
        val bundle = bundleOf("userId" to id, "userName" to username)
//        findNavController().navigate(R.id.action_navigation_users_to_userInfoActivity, bundle)
    }
}