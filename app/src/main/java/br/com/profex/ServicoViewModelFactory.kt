package br.com.profex

import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider

class ServicoViewModelFactory(private val repository: ServicoRepository) : ViewModelProvider.Factory {
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        if (modelClass.isAssignableFrom(ServicoViewModel::class.java)) {
            @Suppress("UNCHECKED_CAST")
            return ServicoViewModel(repository) as T
        }
        throw IllegalArgumentException("Unknown ViewModel class")
    }
}