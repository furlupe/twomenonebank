package com.example.employeeclient.presentation.account.accountinfo.tabs.operations

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ProgressBar
import android.widget.TextView
import androidx.core.view.isGone
import androidx.core.view.isVisible
import androidx.recyclerview.widget.RecyclerView
import com.example.employeeclient.R
import com.example.employeeclient.domain.model.account.event.AccountEventDomain
import com.example.employeeclient.domain.model.enums.AccountEventType
import java.util.LinkedList

class AccountOperationTabAdapter(
    private val context: Context?,
    private val onLoadNextClick: () -> Unit,
) : RecyclerView.Adapter<RecyclerView.ViewHolder>() {
    private var operations: MutableList<AccountEventDomain>? = null
    private var isLoadingAdded = false

    init {
        operations = LinkedList()
    }

    fun setUsers(operations: MutableList<AccountEventDomain>?) {
        this.operations = operations
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RecyclerView.ViewHolder {
        var viewHolder: RecyclerView.ViewHolder? = null
        val inflater = LayoutInflater.from(parent.context)

        when (viewType) {
            BALANCE_CHANGE -> {
                val viewItem: View = inflater.inflate(R.layout.item_balance_change, parent, false)
                viewHolder = BalanceChangeViewHolder(viewItem)
            }

            TRANSFER -> {
                val viewItem: View = inflater.inflate(R.layout.item_transfer, parent, false)
                viewHolder = TransferViewHolder(viewItem)
            }

            LOADING -> {
                val viewLoading: View = inflater.inflate(R.layout.item_loading, parent, false)
                viewHolder = LoadingViewHolder(viewLoading)
            }
        }

        return viewHolder!!
    }

    override fun onBindViewHolder(holder: RecyclerView.ViewHolder, position: Int) {
        val operation = operations!![position]

        when (getItemViewType(position)) {
            BALANCE_CHANGE -> {
                val operationViewHolder = holder as BalanceChangeViewHolder
                operationViewHolder.event.text = operation.balanceChange?.eventType?.descr
                operationViewHolder.amount.text = operation.balanceChange?.value.toString()
                operationViewHolder.date.text = operation.resolvedAt
            }

            TRANSFER -> {
                val operationViewHolder = holder as TransferViewHolder
                operationViewHolder.event.text = operation.eventType.descr
                operationViewHolder.amount.text = operation.balanceChange?.value.toString()
                operationViewHolder.source.text = operation.transfer?.source?.accountId
                operationViewHolder.target.text = operation.transfer?.target?.accountId
                operationViewHolder.date.text = operation.resolvedAt
            }

            LOADING -> {
                val loadingViewHolder = holder as LoadingViewHolder
                loadingViewHolder.loadProgressButton.visibility = View.VISIBLE

                loadingViewHolder.loadProgressButton.setOnClickListener {
                    onLoadNextClick()
                    loadingViewHolder.progressbar.isGone = false
                    loadingViewHolder.loadProgressButton.isVisible = false
                }
            }
        }
    }

    override fun getItemCount(): Int {
        return if (operations == null) 0 else operations!!.size
    }

    override fun getItemViewType(position: Int): Int {
        if (position == operations!!.size - 1 && isLoadingAdded) return LOADING

        val operationType = operations!![position].eventType
        return when (operationType) {
            AccountEventType.BalanceChange -> BALANCE_CHANGE
            AccountEventType.Transfer -> TRANSFER
        }
    }

    fun addLoadingFooter() {
        isLoadingAdded = true
        add(AccountEventDomain("-1"))
    }

    fun removeLoadingFooter() {
        isLoadingAdded = false
        val position = operations!!.size - 1
        val result = getItem(position)
        if (result != null) {
            operations!!.removeAt(position)
            notifyItemRemoved(position)
        }
    }

    fun add(operation: AccountEventDomain) {
        operations!!.add(operation)
        notifyItemInserted(operations!!.size - 1)
    }

    fun addAll(operations: List<AccountEventDomain>) {
        for (operation in operations) {
            add(operation)
        }
    }

    fun getItem(position: Int): AccountEventDomain? {
        return operations?.let { it[position] }
    }

    inner class BalanceChangeViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        internal val event: TextView
        internal val amount: TextView
        internal val date: TextView

        init {
            event = itemView.findViewById<View>(R.id.tvEventType) as TextView
            amount = itemView.findViewById<View>(R.id.tvAmount) as TextView
            date = itemView.findViewById<View>(R.id.tvDate) as TextView
        }
    }

    inner class TransferViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        internal val event: TextView
        internal val amount: TextView
        internal val source: TextView
        internal val target: TextView
        internal val date: TextView

        init {
            event = itemView.findViewById<View>(R.id.tvEventType) as TextView
            amount = itemView.findViewById<View>(R.id.tvAmount) as TextView
            source = itemView.findViewById<View>(R.id.tvSource) as TextView
            target = itemView.findViewById<View>(R.id.tvTarget) as TextView
            date = itemView.findViewById<View>(R.id.tvDate) as TextView
        }
    }

    inner class LoadingViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        internal val loadProgressButton: Button
        internal val progressbar: ProgressBar

        init {
            loadProgressButton = itemView.findViewById<View>(R.id.btLoadNext) as Button
            progressbar = itemView.findViewById<View>(R.id.progressbar) as ProgressBar
        }
    }

    companion object {
        private const val LOADING = 0
        private const val BALANCE_CHANGE = 1
        private const val TRANSFER = 2
    }
}