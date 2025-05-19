package br.com.profex

import android.widget.Toast
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Save
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.runtime.livedata.observeAsState
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.unit.dp
import androidx.lifecycle.LifecycleOwner
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavController
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ServicoFormScreen(
    navController: NavController,
    servicoId: Int,
    meiId: Int,
    // Adicione o MeiViewModel
    viewModel: ServicoViewModel = viewModel(
        factory = ServicoViewModelFactory(
            (navController.context.applicationContext as MeiApplication).servicoRepository
        )
    ),
    meiViewModel: MeiViewModel = viewModel(
        factory = MeiViewModelFactory(
            (navController.context.applicationContext as MeiApplication).meiRepository
        )
    )
) {
    // Obter lista de MEIs
    val meiList by meiViewModel.todosMei.observeAsState(initial = emptyList())

    // Estado para o MEI selecionado
    var selectedMeiId by remember { mutableStateOf(meiId) }

    // Serviço que está sendo editado/criado
    var servico by remember {
        mutableStateOf(
            Servico(
                0,
                selectedMeiId,
                "",
                0.0,
                "",
                "",
                "",
                "Agendado"
            )
        )
    }

    // ... restante do código

    Scaffold(
        topBar = { /* ... */ }
    ) { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(16.dp),
            verticalArrangement = Arrangement.spacedBy(16.dp)
        ) {
            // Dropdown para selecionar MEI
            Text("MEI",
                style = MaterialTheme.typography.bodyMedium,
                color = MaterialTheme.colorScheme.primary
            )

            var expandedMei by remember { mutableStateOf(false) }

            // Nome do MEI selecionado (ou texto padrão)
            val selectedMeiName = meiList.find { it.id == selectedMeiId }?.nome ?: "Selecione um MEI"

            ExposedDropdownMenuBox(
                expanded = expandedMei,
                onExpandedChange = { expandedMei = it }
            ) {
                OutlinedTextField(
                    value = selectedMeiName,
                    onValueChange = {},
                    readOnly = true,
                    trailingIcon = { ExposedDropdownMenuDefaults.TrailingIcon(expanded = expandedMei) },
                    modifier = Modifier
                        .fillMaxWidth()
                        .menuAnchor()
                )

                ExposedDropdownMenu(
                    expanded = expandedMei,
                    onDismissRequest = { expandedMei = false }
                ) {
                    meiList.forEach { mei ->
                        DropdownMenuItem(
                            text = { Text(mei.nome) },
                            onClick = {
                                selectedMeiId = mei.id
                                servico = servico.copy(meiId = mei.id)
                                expandedMei = false
                            }
                        )
                    }
                }
            }

            // Descrição do Serviço
            OutlinedTextField(
                value = servico.descricao,
                onValueChange = { servico = servico.copy(descricao = it) },
                label = { Text("Descrição do Serviço") },
                modifier = Modifier.fillMaxWidth()
            )

            // Valor
            OutlinedTextField(
                value = servico.valor.toString(),
                onValueChange = {
                    val valorNovo = it.toDoubleOrNull() ?: 0.0
                    servico = servico.copy(valor = valorNovo)
                },
                label = { Text("Valor") },
                keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Number),
                modifier = Modifier.fillMaxWidth()
            )

            // Data do Serviço
            OutlinedTextField(
                value = servico.dataServico,
                onValueChange = { servico = servico.copy(dataServico = it) },
                label = { Text("Data do Serviço (DD/MM/AAAA)") },
                modifier = Modifier.fillMaxWidth()
            )

            // Nome do Cliente
            OutlinedTextField(
                value = servico.clienteNome,
                onValueChange = { servico = servico.copy(clienteNome = it) },
                label = { Text("Nome do Cliente") },
                modifier = Modifier.fillMaxWidth()
            )

            // Telefone do Cliente
            OutlinedTextField(
                value = servico.clienteTelefone,
                onValueChange = { servico = servico.copy(clienteTelefone = it) },
                label = { Text("Telefone do Cliente") },
                modifier = Modifier.fillMaxWidth()
            )

            // Status
            var expandedStatus by remember { mutableStateOf(false) }
            val statusOptions = listOf("Agendado", "Em andamento", "Concluído", "Cancelado")

            ExposedDropdownMenuBox(
                expanded = expandedStatus,
                onExpandedChange = { expandedStatus = it }
            ) {
                OutlinedTextField(
                    value = servico.status,
                    onValueChange = {},
                    readOnly = true,
                    label = { Text("Status") },
                    trailingIcon = { ExposedDropdownMenuDefaults.TrailingIcon(expanded = expandedStatus) },
                    modifier = Modifier
                        .fillMaxWidth()
                        .menuAnchor()
                )

                ExposedDropdownMenu(
                    expanded = expandedStatus,
                    onDismissRequest = { expandedStatus = false }
                ) {
                    statusOptions.forEach { opcao ->
                        DropdownMenuItem(
                            text = { Text(opcao) },
                            onClick = {
                                servico = servico.copy(status = opcao)
                                expandedStatus = false
                            }
                        )
                    }
                }
            }

            Spacer(modifier = Modifier.height(16.dp))


            Button(
                onClick = {
                    // Verificar se um MEI foi selecionado
                    if (selectedMeiId <= 0 || meiList.none { it.id == selectedMeiId }) {
                        Toast.makeText(
                            navController.context,
                            "Selecione um MEI válido antes de salvar",
                            Toast.LENGTH_LONG
                        ).show()
                        return@Button
                    }


                    val servicoFinal = servico.copy(meiId = selectedMeiId)

                    if (servicoId > 0) {
                        viewModel.atualizarServico(servicoFinal)
                    } else {
                        viewModel.inserirServico(servicoFinal)
                    }
                    navController.popBackStack()
                },
                modifier = Modifier.fillMaxWidth()
            ) {
                Text(if (servicoId > 0) "Atualizar" else "Cadastrar")
            }
        }
    }
}