package com.example.customerclient.ui.credit.info.components

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.customerclient.R
import com.example.customerclient.ui.credit.info.CreditHistory

class CreditsHistoryRecyclerAdapter(
    private val items: List<CreditHistory>
) : RecyclerView.Adapter<CreditsHistoryRecyclerAdapter.CreditsHistoryViewHolder>() {

    class CreditsHistoryViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
//        private val binding = ItemH.bind(itemView)
//
//        val historyIcon = binding.historyIcon
//        val billInfoHistoryTitle = binding.billInfoHistoryTitle
//        val moneyHistoryTitle = binding.moneyHistoryTitle
//        val transferHistoryTitle = binding.transferHistoryTitle
//        val date = binding.billInfoHistoryDateTitle
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CreditsHistoryViewHolder {
        // FIXME: item_history_credit_info
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_history_bill_info, parent, false)
        return CreditsHistoryViewHolder(itemView)
    }

    override fun getItemCount(): Int = items.size

    override fun onBindViewHolder(holder: CreditsHistoryViewHolder, position: Int) {}
}