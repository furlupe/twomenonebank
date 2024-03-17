package com.example.customerclient.ui.common

import androidx.recyclerview.widget.DiffUtil
import com.example.customerclient.ui.bottombar.home.CreditShortInfo

class CreditsInfoDiffCallback : DiffUtil.ItemCallback<CreditShortInfo>() {
    override fun areItemsTheSame(oldItem: CreditShortInfo, newItem: CreditShortInfo): Boolean {
        return oldItem.id == newItem.id
    }

    override fun areContentsTheSame(oldItem: CreditShortInfo, newItem: CreditShortInfo): Boolean {
        return oldItem == newItem
    }
}