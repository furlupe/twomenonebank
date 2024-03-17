package com.example.employeeclient.common

import java.time.OffsetDateTime
import java.time.format.DateTimeFormatter

fun String.parseDateToReadable(): String {
    val odt = OffsetDateTime.parse(this)
    val formatter = DateTimeFormatter.ofPattern("dd.MM.yyyy")
    val formattedDate = odt.format(formatter)

    return formattedDate
}