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
    fun inserir(mei: MEI)

    @Update
    fun atualizar(mei: MEI)

    @Delete
    fun deletar(mei: MEI)

    @Query("SELECT * FROM mei_table ORDER BY nome ASC")
    fun obterTodos(): Flow<List<MEI>>

    @Query("SELECT * FROM mei_table WHERE id = :id")
    fun obterPorId(id: Int): Flow<MEI>

    @Query("SELECT COUNT(*) FROM mei_table WHERE id = :id")
    fun contarPorId(id: Int): Int

}