package com.example.customerclient.ui.bill.info.components

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.content.ContextCompat
import androidx.recyclerview.widget.RecyclerView
import com.example.customerclient.R
import com.example.customerclient.databinding.ItemHistoryBillInfoBinding
import com.example.customerclient.ui.bill.info.BillHistory
import com.example.customerclient.ui.bill.info.HistoryOperationType

class BillsHistoryRecyclerAdapter(
    private val items: List<BillHistory>
) : RecyclerView.Adapter<BillsHistoryRecyclerAdapter.BillsHistoryViewHolder>() {

    class BillsHistoryViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        private val binding = ItemHistoryBillInfoBinding.bind(itemView)

        val historyIcon = binding.historyIcon
        val billInfoHistoryTitle = binding.billInfoHistoryTitle
        val moneyHistoryTitle = binding.moneyHistoryTitle
        val transferHistoryTitle = binding.transferHistoryTitle
        val date =binding.billInfoHistoryDateTitle
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): BillsHistoryViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_history_bill_info, parent, false)
        return BillsHistoryViewHolder(itemView)
    }

    override fun getItemCount(): Int = items.size

    override fun onBindViewHolder(holder: BillsHistoryViewHolder, position: Int) {
        holder.historyIcon.setImageResource(
            when (items[position].type) {
                HistoryOperationType.WITHDRAW -> R.drawable.ic_outline_remove_32dp
                HistoryOperationType.TOP_UP -> R.drawable.ic_round_add_32dp
            }
        )
        holder.billInfoHistoryTitle.text = items[position].billNumber
        holder.moneyHistoryTitle.text = items[position].amount

        holder.moneyHistoryTitle.text =
            when (items[position].type) {
                HistoryOperationType.WITHDRAW -> "- ${items[position].amount} ₽"
                HistoryOperationType.TOP_UP -> "+ ${items[position].amount} ₽"
            }


        holder.moneyHistoryTitle.setTextColor(
            when (items[position].type) {
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
        holder.transferHistoryTitle.text = when (items[position].type) {
            HistoryOperationType.WITHDRAW -> "Снятие со счёта"
            HistoryOperationType.TOP_UP -> "Перевод на счёт"
        }

        holder.billInfoHistoryTitle.text = items[position].date
    }
}