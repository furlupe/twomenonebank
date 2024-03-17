package com.example.employeeclient.data.storage

import android.content.Context
import androidx.security.crypto.EncryptedSharedPreferences
import androidx.security.crypto.MasterKeys
import com.example.employeeclient.data.remote.dto.TokenDto
import java.util.Date

class EncryptedSharedPreferencesStorage(context: Context) : TokenStorage {

    companion object {
        const val ENCRYPTED_SHARED_PREFS_NAME = "encryptedSharedPreferences"
    }

    private val masterKeyAlias = MasterKeys.getOrCreate(MasterKeys.AES256_GCM_SPEC)

    private val sharedPreferences = EncryptedSharedPreferences.create(
        ENCRYPTED_SHARED_PREFS_NAME,
        masterKeyAlias,
        context,
        EncryptedSharedPreferences.PrefKeyEncryptionScheme.AES256_SIV,
        EncryptedSharedPreferences.PrefValueEncryptionScheme.AES256_GCM
    )


    override fun saveToken(token: TokenDto) {
        val currentDateTime = Date()

        sharedPreferences.edit()
            .putString(TokenStorage.TOKEN_KEY, token.access_token)
            .putString(TokenStorage.REFRESH_TOKEN_KEY, token.refresh_token)
            .putInt(TokenStorage.TOKEN_EXPIRE_TIME, token.expires_in)
            .putString(TokenStorage.TOKEN_TYPE, token.token_type)
            .putLong(TokenStorage.TOKEN_CREATION_TIME, currentDateTime.time)
            .apply()
    }

    override fun getToken(): TokenDto {
        return TokenDto(
            sharedPreferences.getString(TokenStorage.TOKEN_KEY, "").toString(),
            sharedPreferences.getInt(TokenStorage.TOKEN_EXPIRE_TIME, 0),
            sharedPreferences.getString(TokenStorage.REFRESH_TOKEN_KEY, "").toString(),
            sharedPreferences.getString(TokenStorage.TOKEN_TYPE, "").toString(),
        )
    }

    override fun deleteToken() {
        sharedPreferences.edit()
            .remove(TokenStorage.TOKEN_KEY)
            .remove(TokenStorage.TOKEN_EXPIRE_TIME)
            .remove(TokenStorage.TOKEN_TYPE)
            .remove(TokenStorage.REFRESH_TOKEN_KEY)
            .remove(TokenStorage.TOKEN_CREATION_TIME)
            .apply()
    }

    override fun isTokenAlive(): Boolean {
        val tokenCreationTime = sharedPreferences.getLong(TokenStorage.TOKEN_CREATION_TIME, 0)
        val tokenExpireTime = sharedPreferences.getInt(TokenStorage.TOKEN_EXPIRE_TIME, 0)

        return (tokenCreationTime + tokenExpireTime*1000) > Date().time
    }
}