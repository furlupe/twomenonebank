package com.example.customerclient.ui.credit.create.components

import android.annotation.SuppressLint
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.paging.PagingDataAdapter
import androidx.recyclerview.widget.RecyclerView
import com.example.customerclient.R
import com.example.customerclient.databinding.ItemTariffInfoBinding
import com.example.customerclient.ui.credit.create.Tariff

class TariffsPagingRecyclerAdapter(
    private val onTariffClick: (String) -> Unit
) : PagingDataAdapter<Tariff, TariffsPagingRecyclerAdapter.TariffViewHolder>(
    TariffsDiffCallback()
) {

    class TariffViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        private val binding = ItemTariffInfoBinding.bind(itemView)

        val tariffCard = binding.tariff

        val name = binding.tariffName
        val rate = binding.rateTitle
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): TariffViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_tariff_info, parent, false)
        return TariffViewHolder(itemView)
    }

    @SuppressLint("SetTextI18n")
    override fun onBindViewHolder(holder: TariffViewHolder, position: Int) {
        val item = getItem(position)

        if (item != null) {
            holder.tariffCard.setOnClickListener {
                onTariffClick(item.id)
            }

            holder.name.text = item.name
            holder.rate.text = "${item.rate} %"
        }
    }
}