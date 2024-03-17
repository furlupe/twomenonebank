package com.example.employeeclient.presentation.credit.creditinfo.util

import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentPagerAdapter
import androidx.viewpager2.adapter.FragmentStateAdapter
import com.example.employeeclient.presentation.credit.creditinfo.tabs.details.CreditDetailsTabFragment
import com.example.employeeclient.presentation.credit.creditinfo.tabs.operations.CreditOperationsTabFragment

private val TAB_TITLES = arrayOf(
    "Details",
    "Operations"
)

/**
 * A [FragmentPagerAdapter] that returns a fragment corresponding to
 * one of the sections/tabs/pages.
 */
class SectionsPagerAdapter(
    activity: AppCompatActivity,
    private val creditId: String,
) : FragmentStateAdapter(activity) {
    override fun getItemCount(): Int {
        return 2
    }

    override fun createFragment(position: Int): Fragment {
        return when (position) {
            0 -> {
                CreditDetailsTabFragment.newInstance(creditId)
            }

            else -> {
                CreditOperationsTabFragment.newInstance(creditId)
            }
        }
    }

    fun getPageTitle(position: Int): CharSequence? {
        return TAB_TITLES[position]
    }
}