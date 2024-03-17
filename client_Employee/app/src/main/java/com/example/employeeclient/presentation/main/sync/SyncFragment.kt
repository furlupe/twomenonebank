package com.example.employeeclient.presentation.main.sync

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import com.example.employeeclient.R
import com.example.employeeclient.databinding.FragmentSyncBinding
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class SyncFragment : Fragment() {

    private lateinit var binding: FragmentSyncBinding
    private val viewModel: SyncViewModel by viewModel()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_sync, container, false)
        binding = FragmentSyncBinding.bind(view)

        binding.btSync.setOnClickListener {
            viewModel.sync()
        }

        lifecycleScope.launch {
            viewModel.state.collect {
                binding.progress.isIndeterminate = it.isLoading

                if (it.syncResult) {
                    val toast = if (it.error.isNotEmpty()) {
                         Toast.makeText(context, "Success", Toast.LENGTH_SHORT)
                    } else {
                        Toast.makeText(context, it.error, Toast.LENGTH_SHORT)
                    }

                    toast.show()
                }
            }
        }

        return binding.root
    }
}