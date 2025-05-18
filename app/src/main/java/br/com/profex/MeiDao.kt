package br.com.profex

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import androidx.room.Update
import kotlinx.coroutines.flow.Flow

@Dao
interface MeiDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun inserirMei(mei: MEI) // Removido o tipo de retorno Long

    @Update
    suspend fun atualizarMei(mei: MEI) // Removido o tipo de retorno Int

    @Delete
    suspend fun deletarMei(mei: MEI) // Removido o tipo de retorno Int

    @Query("SELECT * FROM mei_table ORDER BY nome ASC")
    fun obterTodosMei(): Flow<List<MEI>>

    @Query("SELECT * FROM mei_table WHERE id = :id")
    fun obterMeiPorId(id: Int): Flow<MEI>
}