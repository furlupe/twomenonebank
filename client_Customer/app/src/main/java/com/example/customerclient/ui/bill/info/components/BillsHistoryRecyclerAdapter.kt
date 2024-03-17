package com.example.customerclient.ui.bill.info.components

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.content.ContextCompat
import androidx.paging.PagingDataAdapter
import androidx.recyclerview.widget.RecyclerView
import com.example.customerclient.R
import com.example.customerclient.databinding.ItemHistoryBillInfoBinding
import com.example.customerclient.ui.bill.info.BillHistory
import com.example.customerclient.ui.bill.info.HistoryOperationType

class BillsHistoryRecyclerAdapter :
    PagingDataAdapter<BillHistory, BillsHistoryRecyclerAdapter.BillsHistoryViewHolder>(
        BillsHistoryDiffCallback()
    ) {

    class BillsHistoryViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        private val binding = ItemHistoryBillInfoBinding.bind(itemView)

        val historyIcon = binding.historyIcon
        val billInfoHistoryTitle = binding.billInfoHistoryTitle
        val moneyHistoryTitle = binding.moneyHistoryTitle
        val transferHistoryTitle = binding.transferHistoryTitle
        val date = binding.billInfoHistoryDateTitle
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): BillsHistoryViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_history_bill_info, parent, false)
        return BillsHistoryViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: BillsHistoryViewHolder, position: Int) {
        val item = getItem(position)

        if (item != null) {
            holder.historyIcon.setImageResource(
                when (item.type) {
                    HistoryOperationType.WITHDRAW -> R.drawable.ic_outline_remove_32dp
                    HistoryOperationType.TOP_UP -> R.drawable.ic_round_add_32dp
                }
            )
            holder.billInfoHistoryTitle.text = item.billId
            holder.moneyHistoryTitle.text = item.amount

            holder.moneyHistoryTitle.text =
                when (item.type) {
                    HistoryOperationType.WITHDRAW -> "- ${item.amount} ₽"
                    HistoryOperationType.TOP_UP -> "+ ${item.amount} ₽"
                }


            holder.moneyHistoryTitle.setTextColor(
                when (item.type) {
                    HistoryOperationType.WITHDRAW -> ContextCompat.getColor(
                        holder.itemView.context,
                        R.color.red
                    )

                    HistoryOperationType.TOP_UP -> ContextCompat.getColor(
                        holder.itemView.context,
                        R.color.green
                    )
                }
            )
            holder.transferHistoryTitle.text = when (item.type) {
                HistoryOperationType.WITHDRAW -> "Снятие со счёта"
                HistoryOperationType.TOP_UP -> "Перевод на счёт"
            }

            holder.date.text = item.date
        }
    }
}