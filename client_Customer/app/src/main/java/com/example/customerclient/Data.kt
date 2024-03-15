package com.example.customerclient

import com.example.customerclient.ui.bill.info.BillHistory
import com.example.customerclient.ui.bill.info.HistoryOperationType
import com.example.customerclient.ui.bottombar.home.BillInfo
import com.example.customerclient.ui.bottombar.home.CreditInfo

val userName = "Иван Иванович"

val dollarExchangeRate = 90.82
val euroExchangeRate = 99.06

val creditsInfo = listOf<CreditInfo>(
    CreditInfo(
        id = "0",
        type = "Аннуитетный кредит",
        balance = "40000 ₽",
        nextWithdrawDate = "08.03.2024",
        nextFee = "2000 ₽"
    )
)
val billsInfo = listOf<BillInfo>(
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    ),
    BillInfo(
        id = "0",
        number = "****1344",
        balance = "1200 ₽",
        type = "Сберегательный счёт",
        duration = "бессрочный",
    )
)

val billHistory = listOf<BillHistory>(
    BillHistory(
        type = HistoryOperationType.WITHDRAW,
        amount = "10000",
        billNumber = "347289",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.TOP_UP,
        amount = "10000",
        billNumber = "41332",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.WITHDRAW,
        amount = "10000",
        billNumber = "347289",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.TOP_UP,
        amount = "10000",
        billNumber = "41332",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.WITHDRAW,
        amount = "10000",
        billNumber = "347289",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.TOP_UP,
        amount = "10000",
        billNumber = "41332",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.WITHDRAW,
        amount = "10000",
        billNumber = "347289",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.TOP_UP,
        amount = "10000",
        billNumber = "41332",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.WITHDRAW,
        amount = "10000",
        billNumber = "347289",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.TOP_UP,
        amount = "10000",
        billNumber = "41332",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.WITHDRAW,
        amount = "10000",
        billNumber = "347289",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.TOP_UP,
        amount = "10000",
        billNumber = "41332",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.WITHDRAW,
        amount = "10000",
        billNumber = "347289",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.TOP_UP,
        amount = "10000",
        billNumber = "41332",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.WITHDRAW,
        amount = "10000",
        billNumber = "347289",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.TOP_UP,
        amount = "10000",
        billNumber = "41332",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.WITHDRAW,
        amount = "10000",
        billNumber = "347289",
        date = "20.03.2023"
    ),
    BillHistory(
        type = HistoryOperationType.TOP_UP,
        amount = "10000",
        billNumber = "41332",
        date = "20.03.2023"
    )
)
