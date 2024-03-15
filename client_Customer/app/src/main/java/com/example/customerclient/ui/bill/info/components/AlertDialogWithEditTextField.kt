package com.example.customerclient.ui.bill.info.components

import android.app.AlertDialog
import android.app.Dialog
import android.os.Bundle
import androidx.fragment.app.DialogFragment
import com.example.customerclient.R
import com.example.customerclient.databinding.InputFieldViewInBillActionsAlertDialogBinding

class AlertDialogWithEditTextField(
    private val title: String,
    private val editTextTitle: String,
    private val onPositiveButtonClick: (String) -> Unit,
) : DialogFragment() {

    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {
        val binding = InputFieldViewInBillActionsAlertDialogBinding.inflate(layoutInflater)
        val editTextView = binding.inputSumEditText
        editTextView.hint = this.editTextTitle
        return activity?.let {
            AlertDialog.Builder(it)
                .setTitle(this.title)
                .setView(R.layout.input_field_view_in_bill_actions_alert_dialog)
                .setPositiveButton("Ок") { dialog, id ->
                    onPositiveButtonClick(editTextView.text.toString())
                    dialog.cancel()
                }
                .create()
        } ?: throw IllegalStateException("Activity cannot be null")
    }

}