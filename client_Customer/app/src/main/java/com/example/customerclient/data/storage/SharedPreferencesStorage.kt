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

    private val encryptedSharedPreferences = EncryptedSharedPreferences.create(
        ENCRYPTED_SHARED_PREFS_NAME,
        masterKeyAlias,
        context,
        EncryptedSharedPreferences.PrefKeyEncryptionScheme.AES256_SIV,
        EncryptedSharedPreferences.PrefValueEncryptionScheme.AES256_GCM
    )

    private val sharedPreferences = context.getSharedPreferences("prefs", Context.MODE_PRIVATE)

    override fun saveTokens(token: TokenDto) {
        encryptedSharedPreferences.edit()
            .putString(TokenStorage.ACCESS_TOKEN, token.access_token)
            .putString(TokenStorage.REFRESH_TOKEN, token.refresh_token)
            .putLong(TokenStorage.EXPIRED_IN, token.expires_in)
            .apply()
    }

    override fun getRefreshToken(): String {
        return encryptedSharedPreferences.getString(
            TokenStorage.REFRESH_TOKEN, ""
        ).toString()
    }

    override fun getAccessToken(): String {
        return encryptedSharedPreferences.getString(
            TokenStorage.ACCESS_TOKEN, ""
        ).toString()
    }

    override fun getExpiredTime(): Long {
        return encryptedSharedPreferences.getLong(
            TokenStorage.EXPIRED_IN, 0L
        )
    }

    override fun deleteTokens() {
        encryptedSharedPreferences.edit()
            .remove(TokenStorage.ACCESS_TOKEN)
            .remove(TokenStorage.REFRESH_TOKEN)
            .apply()
    }

    override fun saveUserId(userId: String) {
        encryptedSharedPreferences.edit()
            .putString(UserStorage.USER_ID, userId)
            .apply()
    }

    override fun getUserId(): String {
        return encryptedSharedPreferences.getString(
            UserStorage.USER_ID, ""
        ).toString()
    }

    override fun swipeUserTheme() {
        when (sharedPreferences.getInt(UserStorage.USER_THEME, 0)) {
            1 -> sharedPreferences.edit().putInt(UserStorage.USER_THEME, 2).apply()
            else -> sharedPreferences.edit().putInt(UserStorage.USER_THEME, 1).apply()
        }
    }

    override fun getCurrentUserTheme(): UserTheme {
        return when (sharedPreferences.getInt(UserStorage.USER_THEME, 0)) {
            1 -> UserTheme.LIGHT
            else -> UserTheme.DARK
        }
    }
}
