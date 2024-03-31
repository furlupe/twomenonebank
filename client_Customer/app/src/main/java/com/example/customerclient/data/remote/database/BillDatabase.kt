package com.example.customerclient.data.remote.database

import androidx.room.Database
import androidx.room.RoomDatabase
import com.example.customerclient.data.remote.database.entity.BillEntity

@Database(entities = [BillEntity::class], version = 2)
abstract class BillDatabase : RoomDatabase() {
    abstract fun billDao(): BillDao
}
