package com.example.employeeclient.data.db.entity

import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.PrimaryKey
import com.example.employeeclient.common.Constants.BAN_TABLE

@Entity(tableName = BAN_TABLE)
data class BanEntity(
    @PrimaryKey(autoGenerate = true)
    @ColumnInfo(name = "id")
    val id: Int = 0,
    @ColumnInfo(name = "externalId")
    val externalId: String,
    @ColumnInfo(name = "isBanned")
    val isBanned: Boolean = false
)
