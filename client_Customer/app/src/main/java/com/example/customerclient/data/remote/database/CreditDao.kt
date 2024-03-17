package com.example.customerclient.data.remote.database

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.Query
import com.example.customerclient.data.remote.database.entity.CreditEntity

@Dao
interface CreditDao {
    @Insert
    fun insertCredit(credit: CreditEntity)

    @Query("SELECT * FROM credit")
    fun getCredits(): List<CreditEntity>
}