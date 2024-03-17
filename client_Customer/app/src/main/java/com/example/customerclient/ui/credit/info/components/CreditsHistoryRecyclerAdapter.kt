package com.example.customerclient.ui.credit.info.components

import android.annotation.SuppressLint
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.content.ContextCompat
import androidx.paging.PagingDataAdapter
import androidx.recyclerview.widget.RecyclerView
import com.example.customerclient.R
import com.example.customerclient.databinding.ItemHistoryCreditInfoBinding
import com.example.customerclient.ui.credit.info.CreditHistory

class CreditsHistoryRecyclerAdapter :
    PagingDataAdapter<CreditHistory, CreditsHistoryRecyclerAdapter.CreditsHistoryViewHolder>(
        CreditsHistoryDiffCallback()
    ) {

    class CreditsHistoryViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        private val binding = ItemHistoryCreditInfoBinding.bind(itemView)

        val historyIcon = binding.historyCreditIcon
        val creditInfoHistoryTitle = binding.creditInfoHistoryTitle
        val creditActionHistoryTitle = binding.creditActionHistoryTitle
        val creditInfoHistoryDateTitle = binding.creditInfoHistoryDateTitle
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CreditsHistoryViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_history_credit_info, parent, false)
        return CreditsHistoryViewHolder(itemView)
    }

    @SuppressLint("SetTextI18n")
    override fun onBindViewHolder(holder: CreditsHistoryViewHolder, position: Int) {
        val item = getItem(position)

        if (item != null) {
            when (item.type) {
                0 -> {
                    holder.historyIcon.setImageResource(R.drawable.round_close_24)
                    holder.creditInfoHistoryTitle.text = "Закрытие кредита"
                    holder.creditActionHistoryTitle.visibility = View.GONE
                }

                1 -> {
                    holder.historyIcon.setImageResource(R.drawable.round_arrow_forward_24)
                    holder.creditInfoHistoryTitle.text = "Смещение сроков выплаты"
                    holder.creditActionHistoryTitle.visibility = View.VISIBLE
                    holder.creditActionHistoryTitle.text = "Новый срок выплат: ${item.to}"
                    holder.creditActionHistoryTitle.setTextColor(
                        ContextCompat.getColor(
                            holder.itemView.context,
                            R.color.black
                        )
                    )
                }

                2 -> {
                    holder.historyIcon.setImageResource(R.drawable.round_add_green_24)
                    holder.creditInfoHistoryTitle.text = "Выплата по кредиту"
                    holder.creditActionHistoryTitle.visibility = View.VISIBLE
                    holder.creditActionHistoryTitle.text = "+ ${item.amount} ₽"
                    holder.creditActionHistoryTitle.setTextColor(
                        ContextCompat.getColor(
                            holder.itemView.context,
                            R.color.green
                        )
                    )
                }

                3 -> {
                    holder.historyIcon.setImageResource(R.drawable.round_error_24)
                    holder.creditInfoHistoryTitle.text = "Пропущена выплата"
                    holder.creditActionHistoryTitle.visibility = View.GONE
                }

                4 -> {
                    holder.historyIcon.setImageResource(R.drawable.round_error_24)
                    holder.creditInfoHistoryTitle.text = "Назначена пени"
                    holder.creditActionHistoryTitle.visibility = View.VISIBLE
                    holder.creditActionHistoryTitle.text = "${item.amount} ₽"
                    holder.creditActionHistoryTitle.setTextColor(
                        ContextCompat.getColor(
                            holder.itemView.context,
                            R.color.red
                        )
                    )
                }

                5 -> {
                    holder.historyIcon.setImageResource(R.drawable.round_add_green_24)
                    holder.creditInfoHistoryTitle.text = "Пени выплачена"
                    holder.creditActionHistoryTitle.visibility = View.VISIBLE
                    holder.creditActionHistoryTitle.text = "+ ${item.amount} ₽"
                    holder.creditActionHistoryTitle.setTextColor(
                        ContextCompat.getColor(
                            holder.itemView.context,
                            R.color.green
                        )
                    )
                }

                6 -> {
                    holder.historyIcon.setImageResource(R.drawable.round_arrow_forward_24)
                    holder.creditInfoHistoryTitle.text = "Применена ставка"
                    holder.creditActionHistoryTitle.visibility = View.GONE
                }
            }
            holder.creditInfoHistoryDateTitle.text = item.date
        }
    }
}