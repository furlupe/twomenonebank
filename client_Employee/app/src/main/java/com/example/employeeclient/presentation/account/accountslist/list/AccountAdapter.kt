package com.example.employeeclient.presentation.account.accountslist.list

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ProgressBar
import android.widget.TextView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.isGone
import androidx.core.view.isVisible
import androidx.recyclerview.widget.RecyclerView
import com.example.employeeclient.R
import com.example.employeeclient.domain.model.account.AccountDomain
import java.util.LinkedList

class AccountAdapter(
    private val context: Context?,
    private val onLoadNextClick: () -> Unit,
    private val onClick: (id: String) -> Unit,
) : RecyclerView.Adapter<RecyclerView.ViewHolder>() {
    private var accounts: MutableList<AccountDomain>? = null
    private var isLoadingAdded = false

    init {
        accounts = LinkedList()
    }

    fun setUsers(accounts: MutableList<AccountDomain>?) {
        this.accounts = accounts
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RecyclerView.ViewHolder {
        var viewHolder: RecyclerView.ViewHolder? = null
        val inflater = LayoutInflater.from(parent.context)

        when (viewType) {
            ITEM -> {
                val viewItem: View = inflater.inflate(R.layout.item_account, parent, false)
                viewHolder = AccountViewHolder(viewItem)
            }

            LOADING -> {
                val viewLoading: View = inflater.inflate(R.layout.item_loading, parent, false)
                viewHolder = LoadingViewHolder(viewLoading)
            }
        }

        return viewHolder!!
    }

    override fun onBindViewHolder(holder: RecyclerView.ViewHolder, position: Int) {
        val account = accounts!![position]

        when (getItemViewType(position)) {
            ITEM -> {
                val userViewHolder = holder as AccountViewHolder
                userViewHolder.name.text = account.name
                userViewHolder.balance.text = buildString {
                    append("Balance: ")
                    append(account.balance.amount.toString())
                    append(" ")
                    append(account.balance.currency.symbol)
                }

                userViewHolder.item.setOnClickListener {
                    onClick(account.id)
                }

                if (account.isDefault) {
                    userViewHolder.item.background = context?.getDrawable(R.drawable.bg_default_account)
                }

                if (account.isClosed) {
                    userViewHolder.item.background = context?.getDrawable(R.drawable.bg_closed_account)
                }
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
        return if (accounts == null) 0 else accounts!!.size
    }

    override fun getItemViewType(position: Int): Int {
        return if (position == accounts!!.size - 1 && isLoadingAdded) LOADING else ITEM
    }

    fun addLoadingFooter() {
        isLoadingAdded = true
        add(AccountDomain("-1"))
    }

    fun removeLoadingFooter() {
        isLoadingAdded = false
        val position = accounts!!.size - 1
        val result = getItem(position)
        if (result != null) {
            accounts!!.removeAt(position)
            notifyItemRemoved(position)
        }
    }

    fun add(account: AccountDomain) {
        accounts!!.add(account)
        notifyItemInserted(accounts!!.size - 1)
    }

    fun addAll(accounts: List<AccountDomain>) {
        for (account in accounts) {
            add(account)
        }
    }

    fun getItem(position: Int): AccountDomain? {
        return accounts?.let { it[position] }
    }

    inner class AccountViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        internal val item: ConstraintLayout
        internal val name: TextView
        internal val balance: TextView

        init {
            item = itemView.findViewById<View>(R.id.accountItem) as ConstraintLayout
            name = itemView.findViewById<View>(R.id.tvName) as TextView
            balance = itemView.findViewById<View>(R.id.tvBalance) as TextView
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