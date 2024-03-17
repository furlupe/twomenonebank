package com.example.employeeclient.presentation.credit.creditinfo.tabs.details

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.content.res.AppCompatResources
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import com.example.employeeclient.R
import com.example.employeeclient.common.Constants.USER_ID
import com.example.employeeclient.databinding.FragmentCreditDetailsTabBinding
import com.example.employeeclient.presentation.credit.creditinfo.util.CreditInfoListener
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel
import org.koin.core.parameter.parametersOf

class CreditDetailsTabFragment : Fragment() {

    private lateinit var binding: FragmentCreditDetailsTabBinding
    private var callback: CreditInfoListener? = null

    private val viewModel: CreditDetailsTabViewModel by viewModel {
        parametersOf(callback?.getCreditId())
    }

    override fun onAttach(context: Context) {
        callback = activity as CreditInfoListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_credit_details_tab, container, false)
        binding = FragmentCreditDetailsTabBinding.bind(view)

        lifecycleScope.launch {
            viewModel.state.collect { state ->
                if (state == null) {
                    return@collect
                }

                binding.tvTariffName.text = "Tariff: ${state.tariff.name}"
                binding.tvTariffRate.text = "Rate: ${state.tariff.rate}%"
                binding.tvAmount.text = "Amount: ${state.amount}"
                binding.tvBaseAmount.text = "Base amount: ${state.baseAmount}"
                binding.tvDays.text = "Taken for ${state.days} days"
                binding.tvPayment.text = "Payment: ${state.periodicPayment}"
                binding.tvPenalty.text = "Penalty: ${state.penalty}"
                binding.tvClosed.text = if (state.isClosed) "Closed" else "Open"

                if (state.isClosed) {
                    binding.tvClosed.background = context?.let {
                        AppCompatResources.getDrawable(
                            it,
                            R.drawable.bg_closed_credit_text
                        )
                    }
                }
            }
        }

        return binding.root
    }

    companion object {
        @JvmStatic
        fun newInstance(userId: String) =
            CreditDetailsTabFragment().apply {
                arguments = Bundle().apply {
                    putString(USER_ID, userId)
                }
            }
    }
}