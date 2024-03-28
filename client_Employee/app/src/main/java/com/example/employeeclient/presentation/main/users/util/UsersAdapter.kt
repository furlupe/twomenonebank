package com.example.employeeclient.presentation.main.users.util

import android.content.Context
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ProgressBar
import android.widget.TextView
import androidx.appcompat.content.res.AppCompatResources
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.core.view.isGone
import androidx.core.view.isVisible
import androidx.recyclerview.widget.RecyclerView
import com.example.employeeclient.R
import com.example.employeeclient.domain.model.enums.Role
import com.example.employeeclient.domain.model.user.UserDomain
import java.util.LinkedList

class UsersAdapter(
    private val context: Context?,
    private val onLoadNextClick: () -> Unit,
    private val onAccountsClick: (id: String, username: String) -> Unit,
    private val onCreditsClick: (id: String, username: String) -> Unit,
) : RecyclerView.Adapter<RecyclerView.ViewHolder>() {
    private var users: MutableList<UserDomain>? = null
    private var isLoadingAdded = false

    init {
        users = LinkedList()
    }

    fun setUsers(users: MutableList<UserDomain>?) {
        this.users = users
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RecyclerView.ViewHolder {
        var viewHolder: RecyclerView.ViewHolder? = null
        val inflater = LayoutInflater.from(parent.context)

        when (viewType) {
            ITEM -> {
                val viewItem: View = inflater.inflate(R.layout.item_user, parent, false)
                viewHolder = UserViewHolder(viewItem)
            }

            LOADING -> {
                val viewLoading: View = inflater.inflate(R.layout.item_loading, parent, false)
                viewHolder = LoadingViewHolder(viewLoading)
            }
        }

        return viewHolder!!
    }

    override fun onBindViewHolder(holder: RecyclerView.ViewHolder, position: Int) {
        val user = users!![position]

        Log.d("MY", "${users}")

        when (getItemViewType(position)) {
            ITEM -> {
                val userViewHolder = holder as UserViewHolder
                userViewHolder.name.text = user.name

                userViewHolder.adminRole.background = context?.let {
                    val drawable =
                        if (user.roles.contains(Role.Admin)) R.drawable.bg_role_text else R.drawable.bg_disabled_role_text
                    AppCompatResources.getDrawable(it, drawable)
                }
                userViewHolder.employeeRole.background = context?.let {
                    val drawable =
                        if (user.roles.contains(Role.Employee)) R.drawable.bg_role_text else R.drawable.bg_disabled_role_text
                    AppCompatResources.getDrawable(it, drawable)
                }
                userViewHolder.userRole.background = context?.let {
                    val drawable =
                        if (user.roles.contains(Role.User)) R.drawable.bg_role_text else R.drawable.bg_disabled_role_text
                    AppCompatResources.getDrawable(it, drawable)
                }

                userViewHolder.accounts.setOnClickListener {
                    onAccountsClick(user.id, user.name)
                }
                userViewHolder.credits.setOnClickListener {
                    onCreditsClick(user.id, user.name)
                }

                if (user.isBanned) {
                    userViewHolder.card.background =
                        context?.let {
                            AppCompatResources.getDrawable(
                                it,
                                R.drawable.bg_banned_card
                            )
                        }
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
        return if (users == null) 0 else users!!.size
    }

    override fun getItemViewType(position: Int): Int {
        return if (position == users!!.size - 1 && isLoadingAdded) LOADING else ITEM
    }

    fun addLoadingFooter() {
        if (isLoadingAdded) return

        isLoadingAdded = true
        add(UserDomain("-1"))
    }

    fun removeLoadingFooter() {
        isLoadingAdded = false
        val position = users!!.size - 1
        val result = getItem(position)
        if (result != null) {
            users!!.removeAt(position)
            notifyItemRemoved(position)
        }
    }

    fun add(user: UserDomain) {
        users!!.add(user)
        notifyItemInserted(users!!.size - 1)
    }

    fun addAll(users: List<UserDomain>) {
        for (user in users) {
            add(user)
        }
    }

    fun getItem(position: Int): UserDomain? {
        return users?.getOrNull(position)
    }

    inner class UserViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        internal val card: ConstraintLayout
        internal val name: TextView
        internal val adminRole: TextView
        internal val employeeRole: TextView
        internal val userRole: TextView
        internal val accounts: Button
        internal val credits: Button

        init {
            card = itemView.findViewById<View>(R.id.userItem) as ConstraintLayout
            name = itemView.findViewById<View>(R.id.tvUserName) as TextView
            adminRole = itemView.findViewById<View>(R.id.tvUserRoleAdmin) as TextView
            employeeRole = itemView.findViewById<View>(R.id.tvUserRoleEmployee) as TextView
            userRole = itemView.findViewById<View>(R.id.tvUserRoleUser) as TextView
            accounts = itemView.findViewById<View>(R.id.btAccounts) as Button
            credits = itemView.findViewById<View>(R.id.btCredits) as Button
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