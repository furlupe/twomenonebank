package com.example.employeeclient.domain.model.db

import com.example.employeeclient.data.db.entity.BanEntity
import com.example.employeeclient.data.db.entity.TariffEntity
import com.example.employeeclient.data.db.entity.UserEntity
import com.example.employeeclient.domain.model.tariff.TariffDomain
import com.example.employeeclient.domain.model.user.RegisterInfoDomain

fun BanDomain.toEntity() = BanEntity(
    externalId = userId,
    isBanned = isBanned
)

fun BanEntity.toDomain() = BanDomain(
    externalId,
    isBanned
)

fun TariffDomain.toEntity() = TariffEntity(
    name = name,
    rate = rate
)

fun TariffEntity.toDomain() = TariffDomain(
    id.toString(), name, rate
)

fun RegisterInfoDomain.toEntity() = UserEntity(
    username = username,
    email = email,
    password = password,
    role = roles
)

fun UserEntity.toDomain() = RegisterInfoDomain(
    username = username,
    email = email,
    password = password,
    roles = role
)
