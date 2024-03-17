package com.example.customerclient.data.remote.database


import androidx.room.Dao
import androidx.room.Insert
import androidx.room.Query
import com.example.customerclient.data.remote.database.entity.BillEntity

@Dao
interface BillDao {
    @Insert
    fun insertBill(collection: BillEntity)

    @Query("SELECT * FROM bill")
    fun getBills(): List<BillEntity>

    @Query("DELETE FROM bill WHERE id = :id")
    fun deleteBill(id: String)

    @Query("DELETE FROM bill")
    fun deleteAllBills()
}
