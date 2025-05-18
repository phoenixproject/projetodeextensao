package br.com.profex

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "mei_table")
data class MEI(
    @PrimaryKey(autoGenerate = true) val id: Int = 0,
    val nome: String,
    val cnpj: String,
    val dataAbertura: String,
    val telefone: String,
    val email: String,
    val endereco: String
)

