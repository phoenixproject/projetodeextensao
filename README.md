# PROFEX - Aplicativo de Gerenciamento de MEI e Serviços

![Banner PROFEX](https://raw.githubusercontent.com/seu-usuario/profex/main/imagens/banner.png)

## 📱 Sobre o projeto

PROFEX é um aplicativo mobile desenvolvido em Kotlin com Jetpack Compose que permite o gerenciamento de Microempreendedores Individuais (MEI) e seus serviços prestados. O aplicativo permite o cadastro completo de MEIs, registro de serviços associados, e acompanhamento do status dos serviços.

![Tela Principal](https://raw.githubusercontent.com/seu-usuario/profex/main/imagens/tela_principal.png)

## 🛠️ Tecnologias utilizadas

- **Kotlin**: Linguagem de programação principal
- **Jetpack Compose**: Framework moderno para UI
- **Room Database**: Persistência de dados local
- **Coroutines e Flow**: Programação assíncrona
- **MVVM**: Padrão de arquitetura
- **LiveData**: Componente de arquitetura para observar dados
- **Navigation Component**: Navegação entre telas

## 📊 Estrutura do banco de dados

O aplicativo utiliza duas tabelas principais:

### Tabela MEI (`mei_table`)
Armazena informações sobre os Microempreendedores Individuais cadastrados.

![Entidade MEI](https://raw.githubusercontent.com/seu-usuario/profex/main/imagens/entidade_mei.png)

### Tabela Serviço (`servico_table`)
Armazena informações sobre os serviços prestados pelos MEIs.

![Entidade Serviço](https://raw.githubusercontent.com/seu-usuario/profex/main/imagens/entidade_servico.png)

## 📝 Queries SQL utilizadas

### MeiDao

```kotlin
@Dao
interface MeiDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun inserir(mei: MEI)
    // SQL: INSERT INTO mei_table (nome, cnpj, dataAbertura, telefone, email, endereco) 
    //      VALUES (?, ?, ?, ?, ?, ?)
    
    @Update
    fun atualizar(mei: MEI)
    // SQL: UPDATE mei_table 
    //      SET nome = ?, cnpj = ?, dataAbertura = ?, telefone = ?, email = ?, endereco = ? 
    //      WHERE id = ?
    
    @Delete
    fun deletar(mei: MEI)
    // SQL: DELETE FROM mei_table WHERE id = ?
    
    @Query("SELECT * FROM mei_table ORDER BY nome ASC")
    fun obterTodos(): Flow<List<MEI>>
    // Seleciona todos os MEIs ordenados pelo nome
    
    @Query("SELECT * FROM mei_table WHERE id = :id")
    fun obterPorId(id: Int): Flow<MEI>
    // Seleciona um MEI específico pelo ID
    
    @Query("SELECT COUNT(*) FROM mei_table WHERE id = :id")
    fun contarPorId(id: Int): Int
    // Verifica se existe um MEI com o ID especificado
}
```

### ServicoDao

```kotlin
@Dao
interface ServicoDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun inserir(servico: Servico)
    // SQL: INSERT INTO servico_table (meiId, descricao, valor, dataServico, 
    //      clienteNome, clienteTelefone, status) 
    //      VALUES (?, ?, ?, ?, ?, ?, ?)
    
    @Update
    fun atualizar(servico: Servico)
    // SQL: UPDATE servico_table 
    //      SET meiId = ?, descricao = ?, valor = ?, dataServico = ?, 
    //      clienteNome = ?, clienteTelefone = ?, status = ? 
    //      WHERE id = ?
    
    @Delete
    fun deletar(servico: Servico)
    // SQL: DELETE FROM servico_table WHERE id = ?
    
    @Query("SELECT * FROM servico_table ORDER BY dataServico DESC")
    fun obterTodos(): Flow<List<Servico>>
    // Seleciona todos os serviços ordenados pela data (mais recentes primeiro)
    
    @Query("SELECT * FROM servico_table WHERE meiId = :meiId ORDER BY dataServico DESC")
    fun obterPorMei(meiId: Int): Flow<List<Servico>>
    // Seleciona serviços de um MEI específico
    
    @Query("SELECT * FROM servico_table WHERE id = :id")
    fun obterPorId(id: Int): Flow<Servico>
    // Seleciona um serviço específico pelo ID
}
```

## 🔄 Relacionamento entre as tabelas

![Diagrama de Relacionamento](https://raw.githubusercontent.com/seu-usuario/profex/main/imagens/relacionamento.png)

O relacionamento entre as tabelas é definido da seguinte forma:

```kotlin
@Entity(
    tableName = "servico_table",
    foreignKeys = [
        ForeignKey(
            entity = MEI::class,
            parentColumns = ["id"],
            childColumns = ["meiId"],
            onDelete = ForeignKey.CASCADE
        )
    ],
    indices = [
        Index(value = ["meiId"])
    ]
)
```

Este relacionamento garante:
- Integridade referencial: cada serviço deve estar associado a um MEI válido
- Exclusão em cascata: se um MEI for excluído, todos os seus serviços também serão
- Índice na coluna meiId para melhorar a performance das consultas

## 🏛️ Arquitetura

O aplicativo segue a arquitetura MVVM (Model-View-ViewModel) com as seguintes camadas:

![Arquitetura MVVM](https://raw.githubusercontent.com/seu-usuario/profex/main/imagens/arquitetura.png)

1. **Model**: Entidades (MEI e Servico) e banco de dados Room
2. **View**: Componentes Compose para UI (telas e componentes visuais)
3. **ViewModel**: Gerencia os dados da UI e comunica-se com os repositórios
4. **Repository**: Camada intermediária que acessa os DAOs

## 📱 Funcionalidades principais

- Cadastro, edição e exclusão de MEIs
- Registro, atualização e remoção de serviços
- Visualização de todos os MEIs cadastrados
- Visualização de serviços filtrados por MEI
- Acompanhamento do status dos serviços

![Telas do App](https://raw.githubusercontent.com/seu-usuario/profex/main/imagens/telas.png)

## 🔧 Como executar o projeto

1. Clone o repositório
   ```
   git clone https://github.com/seu-usuario/profex.git
   ```

2. Abra o projeto no Android Studio

3. Sincronize o projeto com os arquivos Gradle

4. Execute o aplicativo em um emulador ou dispositivo físico

## 📝 Notas importantes

- O aplicativo utiliza Room Database para persistência local de dados
- As operações de banco de dados são executadas em threads secundárias usando Dispatchers.IO
- É necessário selecionar um MEI válido ao cadastrar um serviço para satisfazer a restrição de chave estrangeira

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir um issue ou enviar um pull request.

## 📄 Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo LICENSE para mais detalhes.
