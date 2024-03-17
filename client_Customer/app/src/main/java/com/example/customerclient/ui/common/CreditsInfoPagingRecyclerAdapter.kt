package com.example.customerclient.ui.common

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.paging.PagingDataAdapter
import androidx.recyclerview.widget.RecyclerView
import com.example.customerclient.R
import com.example.customerclient.databinding.ItemCreditInfoBinding
import com.example.customerclient.ui.bottombar.home.CreditShortInfo

class CreditsInfoPagingRecyclerAdapter(
    private val onCreditClick: (String) -> Unit
) : PagingDataAdapter<CreditShortInfo, CreditsInfoPagingRecyclerAdapter.CreditInfoViewHolder>(
    CreditsInfoDiffCallback()
) {

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

    override fun onBindViewHolder(holder: CreditInfoViewHolder, position: Int) {
        val item = getItem(position)

        if (item != null) {
            holder.creditItem.setOnClickListener {
                onCreditClick(item.id)
            }
            holder.type.text = item.type
            holder.balance.text = item.balance
            holder.nextWithdrawDate.text = item.date
            holder.nextFee.text = item.nextFee

            if (position == itemCount - 1) {
                holder.divider.visibility = View.GONE
            }
        }
    }
}