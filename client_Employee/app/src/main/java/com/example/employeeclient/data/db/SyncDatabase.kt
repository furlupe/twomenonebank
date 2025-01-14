package com.example.employeeclient.data.db

import androidx.room.Database
import androidx.room.RoomDatabase
import androidx.room.TypeConverters
import com.example.employeeclient.data.db.converters.ListIntConverter
import com.example.employeeclient.data.db.dao.BanDao
import com.example.employeeclient.data.db.dao.TariffDao
import com.example.employeeclient.data.db.dao.UserDao
import com.example.employeeclient.data.db.entity.BanEntity
import com.example.employeeclient.data.db.entity.TariffEntity
import com.example.employeeclient.data.db.entity.UserEntity

@Database(
    entities = [
        BanEntity::class,
        TariffEntity::class,
        UserEntity::class
    ],
    version = 2
)
@TypeConverters(ListIntConverter::class)
abstract class SyncDatabase : RoomDatabase() {
    abstract fun banDao(): BanDao
    abstract fun tariffDao(): TariffDao
    abstract fun userDao(): UserDao
}