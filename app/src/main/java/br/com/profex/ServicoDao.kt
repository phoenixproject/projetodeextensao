package br.com.profex

import androidx.room.*
import kotlinx.coroutines.flow.Flow

@Dao
interface ServicoDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun inserir(servico: Servico)  // Sem tipo de retorno, sem suspend

    @Update
    fun atualizar(servico: Servico)  // Sem tipo de retorno, sem suspend

    @Delete
    fun deletar(servico: Servico)  // Sem tipo de retorno, sem suspend

    @Query("SELECT * FROM servico_table ORDER BY dataServico DESC")
    fun obterTodos(): Flow<List<Servico>>

    @Query("SELECT * FROM servico_table WHERE meiId = :meiId ORDER BY dataServico DESC")
    fun obterPorMei(meiId: Int): Flow<List<Servico>>

    @Query("SELECT * FROM servico_table WHERE id = :id")
    fun obterPorId(id: Int): Flow<Servico>
}