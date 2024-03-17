package com.example.customerclient.ui.credit.info.components

import androidx.recyclerview.widget.DiffUtil
import com.example.customerclient.ui.credit.info.CreditHistory

class CreditsHistoryDiffCallback : DiffUtil.ItemCallback<CreditHistory>() {
    override fun areItemsTheSame(oldItem: CreditHistory, newItem: CreditHistory): Boolean {
        return oldItem.id == newItem.id
    }

    override fun areContentsTheSame(oldItem: CreditHistory, newItem: CreditHistory): Boolean {
        return oldItem == newItem
    }
}