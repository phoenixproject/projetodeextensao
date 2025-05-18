package br.com.profex

import br.com.profex.MeiDao
import kotlinx.coroutines.flow.Flow

class MeiRepository(private val meiDao: MeiDao) {
    val todosMei: Flow<List<MEI>> = meiDao.obterTodosMei()

    fun obterMeiPorId(id: Int): Flow<MEI> {
        return meiDao.obterMeiPorId(id)
    }

    suspend fun inserirMei(mei: MEI) {
        meiDao.inserirMei(mei)
    }

    suspend fun atualizarMei(mei: MEI) {
        meiDao.atualizarMei(mei)
    }

    suspend fun deletarMei(mei: MEI) {
        meiDao.deletarMei(mei)
    }
}