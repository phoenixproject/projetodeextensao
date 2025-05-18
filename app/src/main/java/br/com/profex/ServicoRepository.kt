package br.com.profex

import kotlinx.coroutines.flow.Flow

class ServicoRepository(private val servicoDao: ServicoDao) {
    val todosServicos: Flow<List<Servico>> = servicoDao.obterTodosServicos()

    fun obterServicosPorMei(meiId: Int): Flow<List<Servico>> {
        return servicoDao.obterServicosPorMei(meiId)
    }

    fun obterServicoPorId(id: Int): Flow<Servico> {
        return servicoDao.obterServicoPorId(id)
    }

    suspend fun inserirServico(servico: Servico) {
        servicoDao.inserirServico(servico)
    }

    suspend fun atualizarServico(servico: Servico) {
        servicoDao.atualizarServico(servico)
    }

    suspend fun deletarServico(servico: Servico) {
        servicoDao.deletarServico(servico)
    }
}