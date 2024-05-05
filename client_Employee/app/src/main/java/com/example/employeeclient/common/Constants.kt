package com.example.employeeclient.common

object Constants {

    const val SHARED_PREFS_THEME = "theme"

    const val DEEP_LINK = "myschema://twomenonebank.com/employee"

    const val BASE_URL_AUTH = "http://185.177.219.207:28000/"
    const val BASE_URL_CORE = "http://185.177.219.207:27000/"
    const val BASE_URL_CREDIT = "http://185.177.219.207:29000/"

    const val BASE_BFF_URL = "http://10.0.2.2:5055/"

    const val CONNECT_TIMEOUT = 15L
    const val READ_TIMEOUT = 60L
    const val WRITE_TIMEOUT = 60L

    /* Navigation constants */
    const val USER_ID = "userId"

    /* Database */
    const val DATABASE_NAME = "SyncDatabase"

    const val BAN_TABLE = "ban"
    const val TARIFF_TABLE = "tariff"
    const val USER_TABLE = "user"

    // Firebase
    const val EMPLOYEE_TOPIC = "employee"

    // Firebase local notifications
    const val LOCAL_CHANNEL_ID = "firebase-local"
    const val LOCAL_CHANNEL = "firebase-local"

}