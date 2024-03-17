package com.example.customerclient.data.remote.database.entity

import android.os.Parcelable
import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.PrimaryKey
import kotlinx.parcelize.Parcelize

@Parcelize
@Entity(tableName = "bill")
data class BillEntity(
    @ColumnInfo(name = "id")
    @PrimaryKey val id: String = "",

    @ColumnInfo(name = "name")
    val name: String,

    @ColumnInfo(name = "balance")
    val balance: String,

    @ColumnInfo(name = "type")
    val type: String,

    @ColumnInfo(name = "duration")
    val duration: String,

) : Parcelable
