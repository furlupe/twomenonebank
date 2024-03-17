package com.example.customerclient.ui.common

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import androidx.recyclerview.widget.RecyclerView.Adapter
import com.example.customerclient.R
import com.example.customerclient.databinding.ItemCreditInfoBinding
import com.example.customerclient.ui.bottombar.home.CreditShortInfo

class CreditsInfoRecyclerAdapter(
    private val items: List<CreditShortInfo>,
    private val onCreditClick: (String) -> Unit
) : Adapter<CreditsInfoRecyclerAdapter.CreditInfoViewHolder>() {

    class CreditInfoViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        private val binding = ItemCreditInfoBinding.bind(itemView)

        val creditItem = binding.credit
        val type = binding.creditTypeTitle
        val balance = binding.balanceCreditTitle
        val nextWithdrawDate = binding.nextWithdrawDateTitle
        val nextFee = binding.nextFeeTitle
        val divider = binding.creditDivider
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CreditInfoViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_credit_info, parent, false)
        return CreditInfoViewHolder(itemView)
    }

    override fun getItemCount(): Int = items.size

    override fun onBindViewHolder(holder: CreditInfoViewHolder, position: Int) {
        holder.creditItem.setOnClickListener {
            onCreditClick(items[position].id)
        }
        holder.type.text = items[position].type
        holder.balance.text = items[position].balance
        holder.nextWithdrawDate.text = items[position].date
        holder.nextFee.text = items[position].nextFee

        if (position == itemCount - 1) {
            holder.divider.visibility = View.GONE
        }
    }
}