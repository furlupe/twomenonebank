package com.example.employeeclient.data.db.converters

import androidx.room.TypeConverter
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import java.lang.reflect.Type

class ListIntConverter {

    @TypeConverter
    fun fromListIntToString(listInt: List<Int>?): String? {
        if (listInt == null) {
            return null
        }
        val gson = Gson()
        val type: Type = object : TypeToken<List<Int>>() {}.type
        return gson.toJson(listInt, type)
    }

    @TypeConverter
    fun toListIntFromString(data: String?): List<Int>? {
        if (data == null) {
            return null
        }
        val gson = Gson()
        val type: Type = object : TypeToken<List<Int>>() {}.type
        return gson.fromJson(data, type)
    }
}
