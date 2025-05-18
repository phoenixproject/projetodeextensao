package br.com.profex

import androidx.compose.foundation.layout.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Save
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.lifecycle.LifecycleOwner
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavController
import kotlinx.coroutines.launch

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ServicoFormScreen(
    navController: NavController,
    servicoId: Int,
    meiId: Int,
    viewModel: ServicoViewModel = viewModel(
        factory = ServicoViewModelFactory(
            (navController.context.applicationContext as MeiApplication).servicoRepository
        )
    )
) {
    var servico by remember {
        mutableStateOf(
            Servico(
                0,
                meiId,
                "",
                0.0,
                "",
                "",
                "",
                "Agendado"
            )
        )
    }
    var isEdit by remember { mutableStateOf(false) }

    // Carrega dados do serviço se for edição
    LaunchedEffect(servicoId) {
        if (servicoId > 0) {
            isEdit = true
            // Usando observe para LiveData
            viewModel.obterServicoPorId(servicoId).observe(navController.context as LifecycleOwner) { servicoEncontrado ->
                servico = servicoEncontrado
            }
        }
    }

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text(if (isEdit) "Editar Serviço" else "Novo Serviço") },
                actions = {
                    IconButton(onClick = {
                        if (isEdit) {
                            viewModel.atualizarServico(servico)
                        } else {
                            viewModel.inserirServico(servico)
                        }
                        navController.popBackStack()
                    }) {
                        Icon(Icons.Default.Save, contentDescription = "Salvar")
                    }
                }
            )
        }
    ) { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(16.dp),
            verticalArrangement = Arrangement.spacedBy(16.dp)
        ) {
            // Campos para o Serviço
            OutlinedTextField(
                value = servico.descricao,
                onValueChange = { servico = servico.copy(descricao = it) },
                label = { Text("Descrição do Serviço") },
                modifier = Modifier.fillMaxWidth()
            )

            OutlinedTextField(
                value = servico.valor.toString(),
                onValueChange = {
                    val valorNovo = it.toDoubleOrNull() ?: 0.0
                    servico = servico.copy(valor = valorNovo)
                },
                label = { Text("Valor") },
                modifier = Modifier.fillMaxWidth()
            )

            OutlinedTextField(
                value = servico.dataServico,
                onValueChange = { servico = servico.copy(dataServico = it) },
                label = { Text("Data do Serviço (DD/MM/AAAA)") },
                modifier = Modifier.fillMaxWidth()
            )

            OutlinedTextField(
                value = servico.clienteNome,
                onValueChange = { servico = servico.copy(clienteNome = it) },
                label = { Text("Nome do Cliente") },
                modifier = Modifier.fillMaxWidth()
            )

            OutlinedTextField(
                value = servico.clienteTelefone,
                onValueChange = { servico = servico.copy(clienteTelefone = it) },
                label = { Text("Telefone do Cliente") },
                modifier = Modifier.fillMaxWidth()
            )

            // Dropdown para Status
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
                    if (isEdit) {
                        viewModel.atualizarServico(servico)
                    } else {
                        viewModel.inserirServico(servico)
                    }
                    navController.popBackStack()
                },
                modifier = Modifier.fillMaxWidth()
            ) {
                Text(if (isEdit) "Atualizar" else "Cadastrar")
            }
        }
    }
}