package br.com.profex

import androidx.room.*
import kotlinx.coroutines.flow.Flow

@Dao
interface ServicoDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun inserirServico(servico: Servico) // Removido o tipo de retorno Long

    @Update
    suspend fun atualizarServico(servico: Servico) // Removido o tipo de retorno Int

    @Delete
    suspend fun deletarServico(servico: Servico) // Removido o tipo de retorno Int

    @Query("SELECT * FROM servico_table ORDER BY dataServico DESC")
    fun obterTodosServicos(): Flow<List<Servico>>

    @Query("SELECT * FROM servico_table WHERE meiId = :meiId ORDER BY dataServico DESC")
    fun obterServicosPorMei(meiId: Int): Flow<List<Servico>>

    @Query("SELECT * FROM servico_table WHERE id = :id")
    fun obterServicoPorId(id: Int): Flow<Servico>
}