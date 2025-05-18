package br.com.profex

import androidx.room.Entity
import androidx.room.ForeignKey
import androidx.room.PrimaryKey

@Entity(
    tableName = "servico_table",
    foreignKeys = [
        ForeignKey(
            entity = MEI::class,
            parentColumns = ["id"],
            childColumns = ["meiId"],
            onDelete = ForeignKey.CASCADE
        )
    ]
)
data class Servico(
    @PrimaryKey(autoGenerate = true) val id: Int = 0,
    val meiId: Int,
    val descricao: String,
    val valor: Double,
    val dataServico: String,
    val clienteNome: String,
    val clienteTelefone: String,
    val status: String
)
