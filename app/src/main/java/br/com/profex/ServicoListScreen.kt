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
fun ServicoListScreen(
    navController: NavController,
    meiId: Int = -1,
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
    // Se meiId for definido, busca serviços do MEI específico, caso contrário, busca todos
    val servicosState = if (meiId > 0) {
        viewModel.obterServicosPorMei(meiId).observeAsState(initial = emptyList())
    } else {
        viewModel.todosServicos.observeAsState(initial = emptyList())
    }

    // Se tiver meiId, buscar informações do MEI para exibir no título
    val meiState = if (meiId > 0) {
        meiViewModel.obterMeiPorId(meiId).observeAsState()
    } else {
        remember { mutableStateOf(null) }
    }

    val servicos = servicosState.value
    val mei = meiState.value

    Scaffold(
        topBar = {
            TopAppBar(
                title = {
                    Text(
                        if (meiId > 0) {
                            mei?.let { "Serviços - ${it.nome}" } ?: "Serviços"
                        } else {
                            "Todos os Serviços"
                        }
                    )
                },
                navigationIcon = {
                    IconButton(onClick = { navController.popBackStack() }) {
                        Icon(Icons.Default.ArrowBack, contentDescription = "Voltar")
                    }
                },
                actions = {
                    IconButton(onClick = {
                        val route = if (meiId > 0) {
                            "servico_form?meiId=$meiId"
                        } else {
                            "servico_form"
                        }
                        navController.navigate(route)
                    }) {
                        Icon(Icons.Default.Add, contentDescription = "Adicionar Serviço")
                    }
                }
            )
        }
    ) { paddingValues ->
        if (servicos.isEmpty()) {
            Box(
                modifier = Modifier
                    .fillMaxSize()
                    .padding(paddingValues),
                contentAlignment = Alignment.Center
            ) {
                Text("Nenhum serviço cadastrado")
            }
        } else {
            LazyColumn(
                modifier = Modifier
                    .fillMaxSize()
                    .padding(paddingValues),
                contentPadding = PaddingValues(16.dp)
            ) {
                items(items = servicos) { servico ->
                    ServicoItem(
                        servico = servico,
                        onItemClick = {
                            navController.navigate("servico_detail/${servico.id}")
                        }
                    )
                }
            }
        }
    }
}

@Composable
fun ServicoItem(
    servico: Servico,
    onItemClick: () -> Unit
) {
    Card(
        modifier = Modifier
            .fillMaxWidth()
            .padding(vertical = 8.dp)
            .clickable(onClick = onItemClick),
    ) {
        Column(
            modifier = Modifier
                .padding(16.dp)
                .fillMaxWidth()
        ) {
            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceBetween,
                verticalAlignment = Alignment.CenterVertically
            ) {
                Text(
                    text = servico.descricao,
                    style = MaterialTheme.typography.titleMedium,
                    fontWeight = FontWeight.Bold
                )

                // Status Chip
                Surface(
                    shape = RoundedCornerShape(16.dp),
                    color = when(servico.status) {
                        "Agendado" -> MaterialTheme.colorScheme.primaryContainer
                        "Em andamento" -> MaterialTheme.colorScheme.tertiaryContainer
                        "Concluído" -> MaterialTheme.colorScheme.secondaryContainer
                        "Cancelado" -> MaterialTheme.colorScheme.errorContainer
                        else -> MaterialTheme.colorScheme.surfaceVariant
                    }
                ) {
                    Text(
                        text = servico.status,
                        modifier = Modifier.padding(horizontal = 8.dp, vertical = 4.dp),
                        style = MaterialTheme.typography.bodySmall
                    )
                }
            }

            Spacer(modifier = Modifier.height(8.dp))

            Text(
                text = "Cliente: ${servico.clienteNome}",
                style = MaterialTheme.typography.bodyMedium
            )

            Spacer(modifier = Modifier.height(4.dp))

            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceBetween
            ) {
                Text(
                    text = "Data: ${servico.dataServico}",
                    style = MaterialTheme.typography.bodyMedium
                )

                Text(
                    text = "Valor: R$ ${String.format("%.2f", servico.valor)}",
                    style = MaterialTheme.typography.bodyMedium,
                    fontWeight = FontWeight.Bold,
                    color = MaterialTheme.colorScheme.primary
                )
            }
        }
    }
}