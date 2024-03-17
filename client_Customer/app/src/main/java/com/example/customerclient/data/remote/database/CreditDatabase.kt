package com.example.customerclient.data.remote.database

import androidx.room.Database
import androidx.room.RoomDatabase
import com.example.customerclient.data.remote.database.entity.CreditEntity

@Database(entities = [CreditEntity::class], version = 1)
abstract class CreditDatabase : RoomDatabase() {
    abstract fun creditDao(): CreditDao
}