package com.example.employeeclient.data.db.dao

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Query
import androidx.room.Upsert
import com.example.employeeclient.common.Constants.TARIFF_TABLE
import com.example.employeeclient.data.db.entity.TariffEntity

@Dao
interface TariffDao {
    @Upsert
    suspend fun upsert(entity: TariffEntity)

    @Query("SELECT * FROM $TARIFF_TABLE")
    suspend fun getAll(): List<TariffEntity>

    @Delete
    suspend fun delete(entity: TariffEntity)
}