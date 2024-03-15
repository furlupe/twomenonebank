package com.example.customerclient.ui.bottombar.home.components

import android.app.AlertDialog
import android.app.Dialog
import android.os.Bundle
import androidx.fragment.app.DialogFragment

class AlertDialogWithConfirmAndDismissButtons(
    private val title: String,
    private val description: String,

    private val positiveButtonText: String,
    private val negativeButtonText: String,

    private val onPositiveButtonClick: () -> Unit,
) : DialogFragment() {

    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {
        return activity?.let {
            AlertDialog.Builder(it)
                .setTitle(this.title)
                .setMessage(this.description)
                .setPositiveButton(positiveButtonText) { dialog, id ->
                    onPositiveButtonClick()
                    dialog.cancel()
                }
                .setNegativeButton(negativeButtonText) { dialog, id -> dialog.cancel() }
                .create()
        } ?: throw IllegalStateException("Activity cannot be null")
    }

}