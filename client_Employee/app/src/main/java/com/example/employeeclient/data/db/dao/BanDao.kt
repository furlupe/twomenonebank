package com.example.employeeclient.data.db.dao

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Query
import androidx.room.Upsert
import com.example.employeeclient.common.Constants.BAN_TABLE
import com.example.employeeclient.data.db.entity.BanEntity

@Dao
interface BanDao {
    @Upsert
    suspend fun upsert(entity: BanEntity)

    @Query("SELECT * FROM $BAN_TABLE")
    suspend fun getAll(): List<BanEntity>

    @Delete
    suspend fun delete(entity: BanEntity)
}