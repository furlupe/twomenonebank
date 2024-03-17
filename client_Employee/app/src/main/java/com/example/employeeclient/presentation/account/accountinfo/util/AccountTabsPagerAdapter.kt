package com.example.employeeclient.presentation.account.accountinfo.util

import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.Fragment
import androidx.viewpager2.adapter.FragmentStateAdapter
import com.example.employeeclient.presentation.account.accountinfo.tabs.details.AccountDetailsTabFragment
import com.example.employeeclient.presentation.account.accountinfo.tabs.operations.AccountOperationsTabFragment

private val TAB_TITLES = arrayOf(
    "Details",
    "Operations"
)

class AccountTabsPagerAdapter(
    activity: AppCompatActivity,
    private val accountId: String,
) : FragmentStateAdapter(activity) {
    override fun getItemCount(): Int {
        return 2
    }

    override fun createFragment(position: Int): Fragment {
        return when (position) {
            0 -> {
                AccountDetailsTabFragment.newInstance(accountId)
            }

            else -> {
                AccountOperationsTabFragment.newInstance(accountId)
            }
        }
    }

    fun getPageTitle(position: Int): CharSequence? {
        return TAB_TITLES[position]
    }
}