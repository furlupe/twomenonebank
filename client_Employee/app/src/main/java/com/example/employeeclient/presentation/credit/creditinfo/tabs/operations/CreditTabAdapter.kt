package com.example.employeeclient.presentation.credit.creditinfo.tabs.operations

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
import com.example.employeeclient.domain.model.credit.operation.CreditOperationDomain
import com.example.employeeclient.domain.model.enums.CreditOperationType
import java.util.LinkedList

class CreditTabAdapter(
    private val context: Context?,
    private val onLoadNextClick: () -> Unit,
) : RecyclerView.Adapter<RecyclerView.ViewHolder>() {
    private var operations: MutableList<CreditOperationDomain>? = null
    private var isLoadingAdded = false

    init {
        operations = LinkedList()
    }

    fun setUsers(operations: MutableList<CreditOperationDomain>?) {
        this.operations = operations
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RecyclerView.ViewHolder {
        var viewHolder: RecyclerView.ViewHolder? = null
        val inflater = LayoutInflater.from(parent.context)

        when (viewType) {
            EVENT -> {
                val viewItem: View = inflater.inflate(R.layout.item_credit_operation, parent, false)
                viewHolder = EventViewHolder(viewItem)
            }

            EVENT_WITH_AMOUNT -> {
                val viewItem: View = inflater.inflate(R.layout.item_credit_operation_with_amount, parent, false)
                viewHolder = EventWithAmountViewHolder(viewItem)
            }

            EVENT_WITH_MOVED -> {
                val viewItem: View = inflater.inflate(R.layout.item_credit_operation_with_moved, parent, false)
                viewHolder = EventWithMovedViewHolder(viewItem)
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
            EVENT -> {
                val operationViewHolder = holder as EventViewHolder
                operationViewHolder.event.text = operation.type.descr
                operationViewHolder.date.text = operation.happenedAt

                if (operation.type == CreditOperationType.PaymentMissed) {
                    operationViewHolder.itemView.background = context?.getDrawable(R.drawable.bg_banned_card)
                }
            }

            EVENT_WITH_AMOUNT -> {
                val operationViewHolder = holder as EventWithAmountViewHolder
                operationViewHolder.event.text = operation.type.descr
                operationViewHolder.amount.text = operation.amount.toString()
                operationViewHolder.date.text = operation.happenedAt
            }

            EVENT_WITH_MOVED -> {
                val operationViewHolder = holder as EventWithMovedViewHolder
                operationViewHolder.event.text = operation.type.descr
                operationViewHolder.movedTo.text = "Moved to: ${operation.to}"
                operationViewHolder.date.text = operation.happenedAt
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

        val operationType = operations!![position].type
        return when (operationType) {
            CreditOperationType.Closed -> EVENT
            CreditOperationType.PaymentDateMoved -> EVENT_WITH_MOVED
            CreditOperationType.PaymentMade -> EVENT_WITH_AMOUNT
            CreditOperationType.PaymentMissed -> EVENT
            CreditOperationType.PenaltyAdded -> EVENT_WITH_AMOUNT
            CreditOperationType.PenaltyPaid -> EVENT_WITH_AMOUNT
            CreditOperationType.RateApplied -> EVENT
        }
    }

    fun addLoadingFooter() {
        isLoadingAdded = true
        add(CreditOperationDomain("-1"))
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

    fun add(operation: CreditOperationDomain) {
        operations!!.add(operation)
        notifyItemInserted(operations!!.size - 1)
    }

    fun addAll(operations: List<CreditOperationDomain>) {
        for (operation in operations) {
            add(operation)
        }
    }

    fun getItem(position: Int): CreditOperationDomain? {
        return operations?.let { it[position] }
    }

    inner class EventViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        internal val event: TextView
        internal val date: TextView

        init {
            event = itemView.findViewById<View>(R.id.tvEventType) as TextView
            date = itemView.findViewById<View>(R.id.tvDate) as TextView
        }
    }

    inner class EventWithAmountViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        internal val event: TextView
        internal val amount: TextView
        internal val date: TextView

        init {
            event = itemView.findViewById<View>(R.id.tvEventType) as TextView
            amount = itemView.findViewById<View>(R.id.tvAmount) as TextView
            date = itemView.findViewById<View>(R.id.tvDate) as TextView
        }
    }

    inner class EventWithMovedViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        internal val event: TextView
        internal val movedTo: TextView
        internal val date: TextView

        init {
            event = itemView.findViewById<View>(R.id.tvEventType) as TextView
            movedTo = itemView.findViewById<View>(R.id.tvMovedTo) as TextView
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
        private const val EVENT = 1
        private const val EVENT_WITH_AMOUNT = 2
        private const val EVENT_WITH_MOVED = 3
    }
}