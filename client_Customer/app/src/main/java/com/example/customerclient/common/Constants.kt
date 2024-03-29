package com.example.customerclient.common

object Constants {
    const val BASE_AUTH_URL = "http://185.177.219.207:28000/"
    const val BASE_CORE_URL = "http://185.177.219.207:27000/"
    const val BASE_CREDIT_URL = "http://185.177.219.207:29000/"

    const val WEB_SITE_URL =
        "http://185.177.219.207:28000/login?ReturnUrl=%2Fconnect%2Fauthorize%3Fclient_id%3Damogus%26response_type%3Dcode%26redirect_uri%3Dtwomenonebank%253A%252F%252Fcustomer.com%26scope%3Doffline_access"

    const val PAGE_CREDIT_LIMIT = 30
    const val PAGE_BILL_LIMIT = 30

    const val BILL_DATABASE_NAME = "Bills"
    const val CREDIT_DATABASE_NAME = "Credits"
}