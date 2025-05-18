package br.com.profex

import androidx.lifecycle.*
import kotlinx.coroutines.launch

class MeiViewModel(private val repository: MeiRepository) : ViewModel() {
    val todosMei: LiveData<List<MEI>> = repository.todosMei.asLiveData()

    fun obterMeiPorId(id: Int): LiveData<MEI> {
        return repository.obterMeiPorId(id).asLiveData()
    }

    fun inserirMei(mei: MEI) = viewModelScope.launch {
        repository.inserirMei(mei)
    }

    fun atualizarMei(mei: MEI) = viewModelScope.launch {
        repository.atualizarMei(mei)
    }

    fun deletarMei(mei: MEI) = viewModelScope.launch {
        repository.deletarMei(mei)
    }
}