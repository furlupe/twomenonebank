package com.example.customerclient.data.remote.database


import androidx.room.Dao
import androidx.room.Insert
import androidx.room.Query
import com.example.customerclient.data.remote.database.entity.BillEntity

@Dao
interface BillDao {
    @Insert
    fun insertBill(bill: BillEntity)

    @Query("SELECT * FROM bill WHERE is_hide = 1")
    fun getHideBills(): List<BillEntity>

    @Query("UPDATE bill SET is_hide = 1 WHERE id=:billId")
    fun addHideBill(billId: String): Int

    @Query("SELECT * FROM bill")
    fun getBills(): List<BillEntity>
}
