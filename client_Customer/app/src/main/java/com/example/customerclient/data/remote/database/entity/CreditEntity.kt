package com.example.customerclient.data.remote.database.entity

import android.os.Parcelable
import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.PrimaryKey
import kotlinx.parcelize.Parcelize

@Parcelize
@Entity(tableName = "credit")
data class CreditEntity(
    @ColumnInfo(name = "id")
    @PrimaryKey val id: String = "",

    @ColumnInfo(name = "amount")
    val amount: String,

    @ColumnInfo(name = "tariff")
    val tariff: String,

    @ColumnInfo(name = "days")
    val days: String,

    @ColumnInfo(name = "is_closed")
    val isClosed: Boolean,

    ) : Parcelable