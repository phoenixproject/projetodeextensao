package br.com.profex

import br.com.profex.MeiDao
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.withContext

class MeiRepository(private val meiDao: MeiDao) {
    val todosMei: Flow<List<MEI>> = meiDao.obterTodos()

    fun obterMeiPorId(id: Int): Flow<MEI> {
        return meiDao.obterPorId(id)
    }

    suspend fun inserirMei(mei: MEI) {
        withContext(Dispatchers.IO) {
            meiDao.inserir(mei)
        }
    }

    suspend fun atualizarMei(mei: MEI) {
        withContext(Dispatchers.IO) {
            meiDao.atualizar(mei)
        }
    }

    suspend fun deletarMei(mei: MEI) {
        withContext(Dispatchers.IO) {
            meiDao.deletar(mei)
        }
    }
}