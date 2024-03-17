package com.example.customerclient.ui.common

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.paging.PagingDataAdapter
import androidx.recyclerview.widget.RecyclerView
import com.example.customerclient.R
import com.example.customerclient.databinding.ItemBillInfoBinding
import com.example.customerclient.ui.bottombar.home.BillInfo

class BillsInfoPagingRecyclerAdapter(
    private val onBillClick: (String) -> Unit
) : PagingDataAdapter<BillInfo, BillsInfoPagingRecyclerAdapter.BillInfoViewHolder>(
    BillsInfoDiffCallback()
) {

    class BillInfoViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        private val binding = ItemBillInfoBinding.bind(itemView)

        val billItem = binding.bill
        val number = binding.numberOfBillTitle
        val balance = binding.balanceBillTitle
        val type = binding.typeOfBillTitle
        val duration = binding.billDurationTitle
        val divider = binding.billDivider
    }

    override fun onCreateViewHolder(
        parent: ViewGroup,
        viewType: Int
    ): BillInfoViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_bill_info, parent, false)
        return BillInfoViewHolder(itemView)
    }

    override fun onBindViewHolder(
        holder: BillInfoViewHolder,
        position: Int
    ) {
        val item = getItem(position)

        if (item != null) {
            holder.billItem.setOnClickListener {
                onBillClick(item.id)
            }
            holder.number.text = item.name
            holder.balance.text = item.balance
            holder.type.text = item.type
            holder.duration.text = item.duration

            if (position == itemCount - 1) {
                holder.divider.visibility = View.GONE
            }
        }
    }
}