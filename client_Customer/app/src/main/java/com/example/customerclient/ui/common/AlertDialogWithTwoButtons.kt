package com.example.customerclient.ui.common

import android.app.AlertDialog
import android.app.Dialog
import android.os.Bundle
import androidx.fragment.app.DialogFragment

class AlertDialogWithTwoButtons(
    private val title: String,
    private val onPositiveButtonClick: () -> Unit,
    private val onNegativeButtonClick: () -> Unit,
) : DialogFragment() {

    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {

        return activity?.let {
            AlertDialog.Builder(it)
                .setTitle(this.title)
                .setPositiveButton("Между своими") { dialog, id ->
                    onPositiveButtonClick()
                    dialog.cancel()
                }
                .setNegativeButton("На чужой") { dialog, id ->
                    onNegativeButtonClick()
                    dialog.cancel()
                }
                .create()
        } ?: throw IllegalStateException("Activity cannot be null")
    }

}