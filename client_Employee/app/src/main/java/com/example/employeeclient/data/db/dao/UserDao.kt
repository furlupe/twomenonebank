package com.example.employeeclient.data.db.dao

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Query
import androidx.room.Upsert
import com.example.employeeclient.common.Constants.USER_TABLE
import com.example.employeeclient.data.db.entity.UserEntity

@Dao
interface UserDao {
    @Upsert
    suspend fun upsert(entity: UserEntity)

    @Query("SELECT * FROM $USER_TABLE")
    suspend fun getAll(): List<UserEntity>

    @Delete
    suspend fun delete(entity: UserEntity)
}