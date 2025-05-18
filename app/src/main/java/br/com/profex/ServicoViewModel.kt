package br.com.profex

import androidx.lifecycle.*
import kotlinx.coroutines.launch

class ServicoViewModel(private val repository: ServicoRepository) : ViewModel() {
    val todosServicos: LiveData<List<Servico>> = repository.todosServicos.asLiveData()

    fun obterServicosPorMei(meiId: Int): LiveData<List<Servico>> {
        return repository.obterServicosPorMei(meiId).asLiveData()
    }

    fun obterServicoPorId(id: Int): LiveData<Servico> {
        return repository.obterServicoPorId(id).asLiveData()
    }

    fun inserirServico(servico: Servico) = viewModelScope.launch {
        repository.inserirServico(servico)
    }

    fun atualizarServico(servico: Servico) = viewModelScope.launch {
        repository.atualizarServico(servico)
    }

    fun deletarServico(servico: Servico) = viewModelScope.launch {
        repository.deletarServico(servico)
    }
}