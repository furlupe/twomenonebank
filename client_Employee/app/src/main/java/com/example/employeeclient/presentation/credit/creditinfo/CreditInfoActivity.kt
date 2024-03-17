package com.example.employeeclient.presentation.credit.creditinfo

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.navArgs
import androidx.viewpager2.widget.ViewPager2
import com.example.employeeclient.databinding.ActivityCreditInfoBinding
import com.example.employeeclient.presentation.credit.creditinfo.util.CreditInfoListener
import com.example.employeeclient.presentation.credit.creditinfo.util.SectionsPagerAdapter
import com.google.android.material.tabs.TabLayout
import com.google.android.material.tabs.TabLayoutMediator

class CreditInfoActivity : AppCompatActivity(), CreditInfoListener {

    private lateinit var binding: ActivityCreditInfoBinding
    private val args: CreditInfoActivityArgs by navArgs()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityCreditInfoBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val sectionsPagerAdapter = SectionsPagerAdapter(this, args.creditId)
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

    override fun getCreditId(): String {
        return args.creditId
    }
}