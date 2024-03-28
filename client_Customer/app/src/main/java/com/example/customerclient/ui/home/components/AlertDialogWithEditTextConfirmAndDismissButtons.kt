package com.example.customerclient.ui.home.components

import android.app.AlertDialog
import android.app.Dialog
import android.os.Bundle
import android.widget.EditText
import android.widget.LinearLayout
import androidx.fragment.app.DialogFragment
import com.example.customerclient.R

class AlertDialogWithEditTextConfirmAndDismissButtons(
    private val title: String,
    private val description: String,

    private val positiveButtonText: String,
    private val negativeButtonText: String,

    private val onPositiveButtonClick: (String) -> Unit,
) : DialogFragment() {

    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {

        val inputEditTextField = EditText(requireActivity())

        val layoutParams = LinearLayout.LayoutParams(
            LinearLayout.LayoutParams.MATCH_PARENT, // Width
            LinearLayout.LayoutParams.MATCH_PARENT // Height
        )

        val margin = resources.getDimensionPixelSize(R.dimen.activity_vertical_margin)
        layoutParams.setMargins(margin, margin, margin, margin)

        inputEditTextField.layoutParams = layoutParams

        inputEditTextField.hint = "Введите название для счёта"

        return activity?.let {
            AlertDialog.Builder(it)
                .setTitle(this.title)
                .setMessage(this.description)
                .setView(inputEditTextField)
                .setPositiveButton(positiveButtonText) { dialog, id ->
                    onPositiveButtonClick(inputEditTextField.text.toString())
                    dialog.cancel()
                }
                .setNegativeButton(negativeButtonText) { dialog, id -> dialog.cancel() }
                .create()
        } ?: throw IllegalStateException("Activity cannot be null")
    }

}