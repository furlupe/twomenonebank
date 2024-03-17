package com.example.employeeclient.presentation.credit.creditslist.list

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ImageView
import android.widget.ProgressBar
import android.widget.TextView
import androidx.appcompat.content.res.AppCompatResources
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.isGone
import androidx.core.view.isVisible
import androidx.recyclerview.widget.RecyclerView
import com.example.employeeclient.R
import com.example.employeeclient.domain.model.credit.CreditDomain
import java.util.LinkedList

class CreditAdapter(
    private val context: Context?,
    private val onLoadNextClick: () -> Unit,
    private val onClick: (id: String) -> Unit,
) : RecyclerView.Adapter<RecyclerView.ViewHolder>() {
    private var credits: MutableList<CreditDomain>? = null
    private var isLoadingAdded = false

    init {
        credits = LinkedList()
    }

    fun setUsers(users: MutableList<CreditDomain>?) {
        this.credits = users
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RecyclerView.ViewHolder {
        var viewHolder: RecyclerView.ViewHolder? = null
        val inflater = LayoutInflater.from(parent.context)

        when (viewType) {
            ITEM -> {
                val viewItem: View = inflater.inflate(R.layout.item_credit, parent, false)
                viewHolder = CreditViewHolder(viewItem)
            }

            LOADING -> {
                val viewLoading: View = inflater.inflate(R.layout.item_loading, parent, false)
                viewHolder = LoadingViewHolder(viewLoading)
            }
        }

        return viewHolder!!
    }

    override fun onBindViewHolder(holder: RecyclerView.ViewHolder, position: Int) {
        val credit = credits!![position]

        when (getItemViewType(position)) {
            ITEM -> {
                val userViewHolder = holder as CreditViewHolder
                userViewHolder.tariff.text = credit.tariff
                userViewHolder.amount.text = credit.amount.toString()
                userViewHolder.days.text = credit.days.toString()

                userViewHolder.item.setOnClickListener {
                    onClick(credit.id)
                }

                val drawable = if (credit.isClosed) {
                    context?.let { AppCompatResources.getDrawable(it, R.drawable.baseline_check_circle_24) }
                } else {
                    context?.let { AppCompatResources.getDrawable(it, R.drawable.baseline_access_time_filled_24) }
                }
                userViewHolder.closedIcon.setImageDrawable(drawable)
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
        return if (credits == null) 0 else credits!!.size
    }

    override fun getItemViewType(position: Int): Int {
        return if (position == credits!!.size - 1 && isLoadingAdded) LOADING else ITEM
    }

    fun addLoadingFooter() {
        isLoadingAdded = true
        add(CreditDomain("-1"))
    }

    fun removeLoadingFooter() {
        isLoadingAdded = false
        val position = credits!!.size - 1
        val result = getItem(position)
        if (result != null) {
            credits!!.removeAt(position)
            notifyItemRemoved(position)
        }
    }

    fun add(user: CreditDomain) {
        credits!!.add(user)
        notifyItemInserted(credits!!.size - 1)
    }

    fun addAll(credits: List<CreditDomain>) {
        for (credit in credits) {
            add(credit)
        }
    }

    fun getItem(position: Int): CreditDomain? {
        return credits?.let { it[position] }
    }

    inner class CreditViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        internal val item: ConstraintLayout
        internal val tariff: TextView
        internal val amount: TextView
        internal val days: TextView
        internal val closedIcon: ImageView

        init {
            item = itemView.findViewById<View>(R.id.creditItem) as ConstraintLayout
            tariff = itemView.findViewById<View>(R.id.tvTariff) as TextView
            amount = itemView.findViewById<View>(R.id.tvAmount) as TextView
            days = itemView.findViewById<View>(R.id.tvDays) as TextView
            closedIcon = itemView.findViewById<View>(R.id.ivClosed) as ImageView
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
        private const val ITEM = 1
    }
}