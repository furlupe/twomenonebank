package com.example.employeeclient.data.db.entity

import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.PrimaryKey
import com.example.employeeclient.common.Constants.TARIFF_TABLE

@Entity(tableName = TARIFF_TABLE)
data class TariffEntity(
    @PrimaryKey(autoGenerate = true)
    @ColumnInfo(name = "id")
    val id: Int = 0,
    @ColumnInfo(name = "name")
    val name: String,
    @ColumnInfo(name = "rate")
    val rate: Int
)
