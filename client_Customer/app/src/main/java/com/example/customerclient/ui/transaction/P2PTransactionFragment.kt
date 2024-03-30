package com.example.customerclient.ui.transaction

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.example.customerclient.databinding.FragmentTransactionP2pBinding
import com.example.customerclient.ui.bill.BillsListener
import org.koin.androidx.viewmodel.ext.android.viewModel
import org.koin.core.parameter.parametersOf

class P2PTransactionFragment : Fragment() {
    private lateinit var binding: FragmentTransactionP2pBinding
    private var callback: BillsListener? = null

    private val viewModel: P2PTransactionViewModel by viewModel { parametersOf(Bundle(), "vm1") }

    override fun onAttach(context: Context) {
        callback = activity as BillsListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentTransactionP2pBinding.inflate(inflater, container, false)

        binding.transactionButton.setOnClickListener {

            viewModel.p2pTransaction(
                phone = binding.phoneNumber.text.toString(),
                amount = binding.transactionAmount.text.toString(),
                message = binding.transactionComment.text.toString(),
            )
        }

        return binding.root
    }
}