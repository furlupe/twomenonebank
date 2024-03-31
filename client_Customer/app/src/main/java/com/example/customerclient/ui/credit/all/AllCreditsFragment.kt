package com.example.customerclient.ui.credit.all

import android.content.Context
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.customerclient.databinding.FragmentAllCreditsBinding
import com.example.customerclient.ui.common.CreditsInfoPagingRecyclerAdapter
import com.example.customerclient.ui.credit.CreditsListener
import kotlinx.coroutines.flow.collectLatest
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class AllCreditsFragment : Fragment() {
    private lateinit var binding: FragmentAllCreditsBinding
    private var callback: CreditsListener? = null

    private val viewModel: AllCreditsViewModel by viewModel()
    override fun onAttach(context: Context) {
        callback = activity as CreditsListener
        super.onAttach(context)
    }

    override fun onStart() {
        super.onStart()
        viewModel.getCreditShortInfo()
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentAllCreditsBinding.inflate(inflater, container, false)

        // Кнопка назад
        binding.backAllCreditsButton.setOnClickListener { callback?.backToMainFragment() }

        // Список кредитов
        val allCreditsRecyclerView = binding.allCreditsRecyclerView
        allCreditsRecyclerView.layoutManager =
            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)

        val adapter = CreditsInfoPagingRecyclerAdapter(onCreditClick = { creditId ->
            navigateToCreditInfoFragment(creditId)
        })
        context?.let { allCreditsRecyclerView.adapter = adapter }

        lifecycleScope.launch {
            viewModel.creditShortInfoState.collectLatest {
                Log.d("CREDITS", "submitData: $it")
                adapter.submitData(it)
            }
        }

        return binding.root
    }

    private fun navigateToCreditInfoFragment(billId: String) {
        val action =
            AllCreditsFragmentDirections.actionNavigationAllCreditsToNavigationCreditInfo(billId)
        findNavController().navigate(action)
    }
}