package br.com.profex

import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider

class MeiViewModelFactory(private val repository: MeiRepository) : ViewModelProvider.Factory {
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        if (modelClass.isAssignableFrom(MeiViewModel::class.java)) {
            @Suppress("UNCHECKED_CAST")
            return MeiViewModel(repository) as T
        }
        throw IllegalArgumentException("Unknown ViewModel class")
    }
}