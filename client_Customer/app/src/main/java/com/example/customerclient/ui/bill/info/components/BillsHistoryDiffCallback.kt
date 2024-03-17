package com.example.customerclient.ui.bill.info.components

import androidx.recyclerview.widget.DiffUtil
import com.example.customerclient.ui.bill.info.BillHistory

class BillsHistoryDiffCallback : DiffUtil.ItemCallback<BillHistory>() {
    override fun areItemsTheSame(oldItem: BillHistory, newItem: BillHistory): Boolean {
        return oldItem.id == newItem.id
    }

    override fun areContentsTheSame(oldItem: BillHistory, newItem: BillHistory): Boolean {
        return oldItem == newItem
    }
}