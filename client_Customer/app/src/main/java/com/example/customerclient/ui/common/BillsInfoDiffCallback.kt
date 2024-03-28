package com.example.customerclient.ui.common

import androidx.recyclerview.widget.DiffUtil
import com.example.customerclient.ui.home.BillInfo

class BillsInfoDiffCallback : DiffUtil.ItemCallback<BillInfo>() {
    override fun areItemsTheSame(oldItem: BillInfo, newItem: BillInfo): Boolean {
        return oldItem.id == newItem.id
    }

    override fun areContentsTheSame(oldItem: BillInfo, newItem: BillInfo): Boolean {
        return oldItem == newItem
    }
}