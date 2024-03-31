package com.example.customerclient.ui.home.components

import android.annotation.SuppressLint
import android.app.AlertDialog
import android.app.Dialog
import android.content.Context
import android.os.Bundle
import android.widget.ArrayAdapter
import android.widget.AutoCompleteTextView
import android.widget.EditText
import androidx.fragment.app.DialogFragment
import com.example.customerclient.R

class AlertDialogWithEditTextConfirmAndDismissButtons(
    private val context: Context,

    private val title: String,
    private val description: String,

    private val positiveButtonText: String,
    private val negativeButtonText: String,

    private val onPositiveButtonClick: (String, String) -> Unit,
) : DialogFragment() {

    @SuppressLint("MissingInflatedId")
    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {

        val view = layoutInflater.inflate(R.layout.exposed_drop_down_menu, null)
        val inputField = view.findViewById<EditText>(R.id.nameOfBill)
        val menu = view.findViewById<AutoCompleteTextView>(R.id.fruitAuto)

        val programmingLanguages = resources.getStringArray(R.array.programming_languages)
        val adapter = ArrayAdapter(context, R.layout.dropdown_item, programmingLanguages)
        menu.setAdapter(adapter)

        return activity?.let {
            AlertDialog.Builder(it)
                .setTitle(this.title)
                .setMessage(this.description)
                .setView(view)
                .setPositiveButton(positiveButtonText) { dialog, id ->
                    onPositiveButtonClick(inputField.text.toString(), menu.text.toString())
                    dialog.cancel()
                }
                .setNegativeButton(negativeButtonText) { dialog, id -> dialog.cancel() }
                .create()
        } ?: throw IllegalStateException("Activity cannot be null")
    }

}