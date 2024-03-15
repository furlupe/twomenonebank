package com.example.customerclient.ui.common

import android.app.AlertDialog
import android.app.Dialog
import android.os.Bundle
import android.widget.EditText
import android.widget.LinearLayout
import androidx.fragment.app.DialogFragment
import com.example.customerclient.R

class AlertDialogWithEditTextField(
    private val title: String,
    private val description: String,
    private val onPositiveButtonClick: (String) -> Unit,
) : DialogFragment() {

    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {

        val inputEditTextField = EditText(requireActivity())

        val layoutParams = LinearLayout.LayoutParams(
            LinearLayout.LayoutParams.MATCH_PARENT, // Width
            LinearLayout.LayoutParams.MATCH_PARENT // Height
        )

        val margin =
            resources.getDimensionPixelSize(R.dimen.activity_vertical_margin)
        layoutParams.setMargins(margin, margin, margin, margin)

        inputEditTextField.layoutParams = layoutParams

        inputEditTextField.hint = description
        return activity?.let {
            AlertDialog.Builder(it)
                .setTitle(this.title)
                .setView(inputEditTextField)
                .setPositiveButton("Ок") { dialog, id ->
                    onPositiveButtonClick(inputEditTextField.text.toString())
                    dialog.cancel()
                }
                .create()
        } ?: throw IllegalStateException("Activity cannot be null")
    }

}