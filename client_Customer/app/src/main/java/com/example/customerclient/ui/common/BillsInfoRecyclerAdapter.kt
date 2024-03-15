package com.example.customerclient.ui.common

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.customerclient.R
import com.example.customerclient.databinding.ItemBillInfoBinding
import com.example.customerclient.ui.bottombar.home.BillInfo

class BillsInfoRecyclerAdapter(
    private val items: List<BillInfo>,
    private val onBillClick: (String) -> Unit
) :
    RecyclerView.Adapter<BillsInfoRecyclerAdapter.BillInfoViewHolder>() {

    class BillInfoViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        private val binding = ItemBillInfoBinding.bind(itemView)

        val billItem = binding.bill
        val number = binding.numberOfBillTitle
        val balance = binding.balanceBillTitle
        val type = binding.typeOfBillTitle
        val duration = binding.billDurationTitle
    }

    override fun onCreateViewHolder(
        parent: ViewGroup,
        viewType: Int
    ): BillInfoViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_bill_info, parent, false)
        return BillInfoViewHolder(itemView)
    }

    override fun getItemCount(): Int = items.size

    override fun onBindViewHolder(
        holder: BillInfoViewHolder,
        position: Int
    ) {
        holder.billItem.setOnClickListener {
            onBillClick(items[position].id)
        }
        holder.number.text = items[position].number
        holder.balance.text = items[position].balance
        holder.type.text = items[position].type
        holder.duration.text = items[position].duration
    }
}