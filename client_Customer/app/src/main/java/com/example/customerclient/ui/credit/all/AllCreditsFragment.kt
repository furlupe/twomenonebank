package com.example.customerclient.ui.credit.all

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.example.customerclient.databinding.FragmentAllCreditsBinding
import com.example.customerclient.ui.credit.CreditsListener
import org.koin.androidx.viewmodel.ext.android.viewModel

class AllCreditsFragment : Fragment() {
    private lateinit var binding: FragmentAllCreditsBinding
    private var callback: CreditsListener? = null

    private val viewModel: AllCreditsViewModel by viewModel()
    override fun onAttach(context: Context) {
        callback = activity as CreditsListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentAllCreditsBinding.inflate(inflater, container, false)

        return binding.root
    }
}