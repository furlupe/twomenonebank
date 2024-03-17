package com.example.customerclient.ui.credit.create.components

import androidx.recyclerview.widget.DiffUtil
import com.example.customerclient.ui.credit.create.Tariff

class TariffsDiffCallback : DiffUtil.ItemCallback<Tariff>() {
    override fun areItemsTheSame(oldItem: Tariff, newItem: Tariff): Boolean {
        return oldItem.id == newItem.id
    }

    override fun areContentsTheSame(oldItem: Tariff, newItem: Tariff): Boolean {
        return oldItem == newItem
    }
}