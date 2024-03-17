package com.example.customerclient.data.storage

import android.content.Context
import androidx.security.crypto.EncryptedSharedPreferences
import androidx.security.crypto.MasterKeys
import com.example.customerclient.data.api.dto.TokenDto

class SharedPreferencesStorage(context: Context) : TokenStorage, UserStorage {

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

    override fun saveTokens(token: TokenDto) {
        sharedPreferences.edit()
            .putString(TokenStorage.ACCESS_TOKEN, token.access_token)
            .putString(TokenStorage.REFRESH_TOKEN, token.refresh_token)
            .putLong(TokenStorage.EXPIRED_IN, token.expires_in)
            .apply()
    }

    override fun getRefreshToken(): String {
        return sharedPreferences.getString(
            TokenStorage.REFRESH_TOKEN, ""
        ).toString()
    }

    override fun getAccessToken(): String {
        return sharedPreferences.getString(
            TokenStorage.ACCESS_TOKEN, ""
        ).toString()
    }

    override fun getExpiredTime(): Long {
        return sharedPreferences.getLong(
            TokenStorage.EXPIRED_IN, 0L
        )
    }

    override fun deleteTokens() {
        sharedPreferences.edit()
            .remove(TokenStorage.ACCESS_TOKEN)
            .remove(TokenStorage.REFRESH_TOKEN)
            .apply()
    }

    override fun saveUserId(userId: String) {
        sharedPreferences.edit()
            .putString(UserStorage.USER_ID, userId)
            .apply()
    }

    override fun getUserId(): String {
        return sharedPreferences.getString(
            UserStorage.USER_ID, ""
        ).toString()
    }
}
