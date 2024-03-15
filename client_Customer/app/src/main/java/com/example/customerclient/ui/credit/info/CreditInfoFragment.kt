package com.example.customerclient.ui.credit.info

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.example.customerclient.databinding.FragmentCreditInfoBinding
import com.example.customerclient.ui.credit.CreditsListener
import org.koin.androidx.viewmodel.ext.android.viewModel
import org.koin.core.parameter.parametersOf

class CreditInfoFragment : Fragment() {
    private lateinit var binding: FragmentCreditInfoBinding
    private var callback: CreditsListener? = null

    override fun onAttach(context: Context) {
        callback = activity as CreditsListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentCreditInfoBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val viewModel: CreditInfoViewModel by viewModel { parametersOf(Bundle(), "vm1") }

        return root
    }
}