package br.com.profex

import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.withContext

class ServicoRepository(private val servicoDao: ServicoDao) {
    val todosServicos: Flow<List<Servico>> = servicoDao.obterTodos()

    fun obterServicosPorMei(meiId: Int): Flow<List<Servico>> {
        return servicoDao.obterPorMei(meiId)
    }

    fun obterServicoPorId(id: Int): Flow<Servico> {
        return servicoDao.obterPorId(id)
    }

    suspend fun inserirServico(servico: Servico) {
        withContext(Dispatchers.IO) {
            servicoDao.inserir(servico)
        }
    }

    suspend fun atualizarServico(servico: Servico) {
        withContext(Dispatchers.IO) {
            servicoDao.atualizar(servico)
        }
    }

    suspend fun deletarServico(servico: Servico) {
        withContext(Dispatchers.IO) {
            servicoDao.deletar(servico)
        }
    }
}