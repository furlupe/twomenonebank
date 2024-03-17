package com.example.employeeclient.presentation.account.accountinfo

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.navArgs
import androidx.viewpager2.widget.ViewPager2
import com.example.employeeclient.R
import com.example.employeeclient.databinding.ActivityAccountInfoBinding
import com.example.employeeclient.presentation.account.accountinfo.util.AccountInfoListener
import com.example.employeeclient.presentation.account.accountinfo.util.AccountTabsPagerAdapter
import com.google.android.material.tabs.TabLayout
import com.google.android.material.tabs.TabLayoutMediator

class AccountInfoActivity : AppCompatActivity(), AccountInfoListener {

    private lateinit var binding: ActivityAccountInfoBinding
    private val args: AccountInfoActivityArgs by navArgs()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setTheme(R.style.AppTheme)

        binding = ActivityAccountInfoBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val sectionsPagerAdapter = AccountTabsPagerAdapter(this, args.accountId)
        val viewPager: ViewPager2 = binding.viewPager
        viewPager.adapter = sectionsPagerAdapter

        val tabs: TabLayout = binding.tabs
        TabLayoutMediator(tabs, viewPager) { tab, position ->
            tab.text = sectionsPagerAdapter.getPageTitle(position)
        }.attach()
    }

    override fun backToUsersFragment() {
        finish()
    }

    override fun getAccountId(): String {
        return args.accountId
    }
}