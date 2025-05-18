package br.com.profex

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.runtime.livedata.observeAsState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavController



@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ServicoDetailScreen(
    navController: NavController,
    servicoId: Int,
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
    val servicoState = viewModel.obterServicoPorId(servicoId).observeAsState()
    val servico = servicoState.value

    // Busca informações do MEI associado ao serviço
    val meiState = servico?.let {
        meiViewModel.obterMeiPorId(it.meiId).observeAsState()
    }
    val mei = meiState?.value

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text("Detalhes do Serviço") },
                navigationIcon = {
                    IconButton(onClick = { navController.popBackStack() }) {
                        Icon(Icons.Default.ArrowBack, contentDescription = "Voltar")
                    }
                },
                actions = {
                    // Botão para editar serviço
                    IconButton(onClick = {
                        servico?.let {
                            navController.navigate("servico_form?servicoId=${it.id}&meiId=${it.meiId}")
                        }
                    }) {
                        Icon(Icons.Default.Edit, contentDescription = "Editar")
                    }
                    // Botão para excluir serviço
                    IconButton(onClick = {
                        servico?.let {
                            viewModel.deletarServico(it)
                            navController.popBackStack()
                        }
                    }) {
                        Icon(Icons.Default.Delete, contentDescription = "Excluir")
                    }
                }
            )
        }
    ) { paddingValues ->
        if (servico == null) {
            Box(
                modifier = Modifier
                    .fillMaxSize()
                    .padding(paddingValues),
                contentAlignment = Alignment.Center
            ) {
                CircularProgressIndicator()
            }
        } else {
            Column(
                modifier = Modifier
                    .fillMaxSize()
                    .padding(paddingValues)
                    .padding(16.dp),
                verticalArrangement = Arrangement.spacedBy(16.dp)
            ) {
                // Status do serviço
                Surface(
                    shape = RoundedCornerShape(8.dp),
                    color = when(servico.status) {
                        "Agendado" -> MaterialTheme.colorScheme.primaryContainer
                        "Em andamento" -> MaterialTheme.colorScheme.tertiaryContainer
                        "Concluído" -> MaterialTheme.colorScheme.secondaryContainer
                        "Cancelado" -> MaterialTheme.colorScheme.errorContainer
                        else -> MaterialTheme.colorScheme.surfaceVariant
                    },
                    modifier = Modifier.fillMaxWidth()
                ) {
                    Column(
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(16.dp),
                        horizontalAlignment = Alignment.CenterHorizontally
                    ) {
                        Text(
                            text = servico.status,
                            style = MaterialTheme.typography.titleLarge,
                            fontWeight = FontWeight.Bold
                        )
                    }
                }

                // Detalhes do serviço
                Card(
                    modifier = Modifier.fillMaxWidth()
                ) {
                    Column(
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(16.dp),
                        verticalArrangement = Arrangement.spacedBy(8.dp)
                    ) {
                        Text(
                            text = servico.descricao,
                            style = MaterialTheme.typography.headlineMedium,
                            fontWeight = FontWeight.Bold
                        )

                        Divider()

                        DetailRow(label = "Data", value = servico.dataServico)
                        DetailRow(label = "Valor", value = "R$ ${String.format("%.2f", servico.valor)}")
                        DetailRow(label = "Cliente", value = servico.clienteNome)
                        DetailRow(label = "Telefone Cliente", value = servico.clienteTelefone)

                        // Se tiver informações do MEI, exibe
                        mei?.let {
                            Divider(modifier = Modifier.padding(vertical = 8.dp))
                            Text(
                                text = "Informações do MEI",
                                style = MaterialTheme.typography.titleMedium,
                                fontWeight = FontWeight.Bold
                            )
                            DetailRow(label = "Nome", value = it.nome)
                            DetailRow(label = "CNPJ", value = it.cnpj)
                            DetailRow(label = "Telefone", value = it.telefone)
                        }
                    }
                }

                // Botão para editar
                OutlinedButton(
                    onClick = { navController.navigate("servico_form?servicoId=${servico.id}&meiId=${servico.meiId}") },
                    modifier = Modifier.fillMaxWidth()
                ) {
                    Icon(Icons.Default.Edit, contentDescription = null)
                    Spacer(modifier = Modifier.width(8.dp))
                    Text("Editar Serviço")
                }

                // Botão para excluir
                Button(
                    onClick = {
                        viewModel.deletarServico(servico)
                        navController.popBackStack()
                    },
                    modifier = Modifier.fillMaxWidth(),
                    colors = ButtonDefaults.buttonColors(
                        containerColor = MaterialTheme.colorScheme.error
                    )
                ) {
                    Icon(Icons.Default.Delete, contentDescription = null)
                    Spacer(modifier = Modifier.width(8.dp))
                    Text("Excluir Serviço")
                }
            }
        }
    }
}