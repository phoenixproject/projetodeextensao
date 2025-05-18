package br.com.profex

import android.app.Application
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.SupervisorJob

class MeiApplication : Application() {
    val applicationScope = CoroutineScope(SupervisorJob())

    val database by lazy { AppDatabase.getDatabase(this) }
    val meiRepository by lazy { MeiRepository(database.meiDao()) }
    val servicoRepository by lazy { ServicoRepository(database.servicoDao()) }
}