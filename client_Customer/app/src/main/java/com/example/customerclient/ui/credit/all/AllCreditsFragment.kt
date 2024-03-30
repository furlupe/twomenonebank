package com.example.customerclient.ui.credit.all

import android.content.Context
import android.graphics.Canvas
import android.graphics.drawable.ColorDrawable
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.toArgb
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.ItemTouchHelper
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.customerclient.databinding.FragmentAllCreditsBinding
import com.example.customerclient.ui.common.CreditsInfoPagingRecyclerAdapter
import com.example.customerclient.ui.credit.CreditsListener
import kotlinx.coroutines.flow.collectLatest
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class AllCreditsFragment : Fragment() {
    private lateinit var binding: FragmentAllCreditsBinding
    private var callback: CreditsListener? = null

    private val viewModel: AllCreditsViewModel by viewModel()
    override fun onAttach(context: Context) {
        callback = activity as CreditsListener
        super.onAttach(context)
    }

    override fun onStart() {
        super.onStart()
        viewModel.getCreditShortInfo()
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentAllCreditsBinding.inflate(inflater, container, false)

        // Кнопка назад
        binding.backAllCreditsButton.setOnClickListener { callback?.backToMainFragment() }

        // Список кредитов
        val allCreditsRecyclerView = binding.allCreditsRecyclerView
        allCreditsRecyclerView.layoutManager =
            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)

        val adapter = CreditsInfoPagingRecyclerAdapter(onCreditClick = { creditId ->
            navigateToCreditInfoFragment(creditId)
        })
        context?.let { allCreditsRecyclerView.adapter = adapter }

        val itemTouchHelperCallback =
            object : ItemTouchHelper.SimpleCallback(0, ItemTouchHelper.LEFT) {
                private val background =
                    ColorDrawable(Color.Red.toArgb()) // Background color for swipe

                override fun onMove(
                    recyclerView: RecyclerView,
                    viewHolder: RecyclerView.ViewHolder,
                    target: RecyclerView.ViewHolder
                ): Boolean {
                    return false // Disable drag & drop
                }

                override fun onSwiped(viewHolder: RecyclerView.ViewHolder, direction: Int) {
                    // Handle swipe action, e.g., remove item from list
                    //adapter.removeItem(viewHolder.adapterPosition)
                }

                override fun onChildDraw(
                    c: Canvas,
                    recyclerView: RecyclerView,
                    viewHolder: RecyclerView.ViewHolder,
                    dX: Float,
                    dY: Float,
                    actionState: Int,
                    isCurrentlyActive: Boolean
                ) {
                    super.onChildDraw(
                        c,
                        recyclerView,
                        viewHolder,
                        dX,
                        dY,
                        actionState,
                        isCurrentlyActive
                    )
                    val itemView = viewHolder.itemView
                    background.setBounds(
                        itemView.left,
                        itemView.top,
                        itemView.left + dX.toInt(),
                        itemView.bottom
                    )
                    background.draw(c)
                }
            }

        val itemTouchHelper = ItemTouchHelper(itemTouchHelperCallback)
        itemTouchHelper.attachToRecyclerView(allCreditsRecyclerView)

        lifecycleScope.launch {
            viewModel.creditShortInfoState.collectLatest {
                adapter.submitData(it)
            }
        }

        return binding.root
    }

    private fun navigateToCreditInfoFragment(billId: String) {
        val action =
            AllCreditsFragmentDirections.actionNavigationAllCreditsToNavigationCreditInfo(billId)
        findNavController().navigate(action)
    }
}