package com.example.customerclient.ui.bill.all

import android.content.Context
import android.graphics.Canvas
import android.graphics.drawable.ColorDrawable
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.ItemTouchHelper
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.customerclient.databinding.FragmentAllBillsBinding
import com.example.customerclient.ui.bill.BillsListener
import com.example.customerclient.ui.common.BillsInfoPagingRecyclerAdapter
import kotlinx.coroutines.flow.collectLatest
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class AllBillsFragment : Fragment() {

    private lateinit var binding: FragmentAllBillsBinding
    private var callback: BillsListener? = null

    val viewModel: AllBillsViewModel by viewModel()

    override fun onAttach(context: Context) {
        callback = activity as BillsListener
        super.onAttach(context)
    }

    override fun onStart() {
        super.onStart()
        viewModel.getBills()
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentAllBillsBinding.inflate(layoutInflater)
        val root: View = binding.root

        // Кнопка назад
        binding.backAllBillsButton.setOnClickListener { callback?.backToMainFragment() }

        // Список счетов
        val allBillsRecyclerView = binding.allBillsRecyclerView
        allBillsRecyclerView.layoutManager =
            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)

        val adapter = BillsInfoPagingRecyclerAdapter(onBillClick = { creditId ->
            navigateToBillInfoFragment(creditId)
        })
        context?.let { allBillsRecyclerView.adapter = adapter }

        val itemTouchHelperCallback =
            object : ItemTouchHelper.SimpleCallback(0, ItemTouchHelper.LEFT) {
                private val background =
                    ColorDrawable(android.graphics.Color.RED) // Background color for swipe

                override fun onMove(
                    recyclerView: RecyclerView,
                    viewHolder: RecyclerView.ViewHolder,
                    target: RecyclerView.ViewHolder
                ): Boolean {
                    return false // Disable drag & drop
                }

                override fun onSwiped(viewHolder: RecyclerView.ViewHolder, direction: Int) {
                    val position = viewHolder.adapterPosition
                    adapter.notifyItemRemoved(position) // Update the RecyclerView
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

                override fun getSwipeThreshold(viewHolder: RecyclerView.ViewHolder): Float {
                    return 0.5f // Set swipe threshold to half the width of the view
                }
            }

        val itemTouchHelper = ItemTouchHelper(itemTouchHelperCallback)
        itemTouchHelper.attachToRecyclerView(allBillsRecyclerView)

        lifecycleScope.launch {
            viewModel.billsInfoState.collectLatest {
                adapter.submitData(it)
            }
        }


        return root
    }

    private fun navigateToBillInfoFragment(billId: String) {
        val action = AllBillsFragmentDirections.actionNavigationAllBillsToNavigationBillInfo(billId)
        findNavController().navigate(action)
    }
}