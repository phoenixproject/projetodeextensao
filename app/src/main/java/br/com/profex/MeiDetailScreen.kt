package br.com.profex
import androidx.compose.foundation.layout.*
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
fun MeiDetailScreen(
    navController: NavController,
    meiId: Int,
    viewModel: MeiViewModel = viewModel(
        factory = MeiViewModelFactory(
            (navController.context.applicationContext as MeiApplication).meiRepository
        )
    )
) {
    val meiState = viewModel.obterMeiPorId(meiId).observeAsState()
    val mei = meiState.value

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text("Detalhes do MEI") },
                navigationIcon = {
                    IconButton(onClick = { navController.popBackStack() }) {
                        Icon(Icons.Default.ArrowBack, contentDescription = "Voltar")
                    }
                },
                actions = {
                    // Botão para editar MEI
                    IconButton(onClick = { navController.navigate("mei_form?meiId=${meiId}") }) {
                        Icon(Icons.Default.Edit, contentDescription = "Editar")
                    }
                    // Botão para excluir MEI
                    IconButton(onClick = {
                        mei?.let {
                            viewModel.deletarMei(it)
                            navController.popBackStack()
                        }
                    }) {
                        Icon(Icons.Default.Delete, contentDescription = "Excluir")
                    }
                }
            )
        },
        floatingActionButton = {
            FloatingActionButton(
                onClick = { navController.navigate("servico_list?meiId=${meiId}") }
            ) {
                Icon(Icons.Default.WorkOutline, contentDescription = "Serviços")
            }
        }
    ) { paddingValues ->
        if (mei == null) {
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
                // Detalhes do MEI
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
                            text = mei.nome,
                            style = MaterialTheme.typography.headlineMedium,
                            fontWeight = FontWeight.Bold
                        )

                        Divider()

                        DetailRow(label = "CNPJ", value = mei.cnpj)
                        DetailRow(label = "Data de Abertura", value = mei.dataAbertura)
                        DetailRow(label = "Telefone", value = mei.telefone)
                        DetailRow(label = "Email", value = mei.email)
                        DetailRow(label = "Endereço", value = mei.endereco)
                    }
                }

                // Botão para ver serviços
                Button(
                    onClick = { navController.navigate("servico_list?meiId=${meiId}") },
                    modifier = Modifier.fillMaxWidth()
                ) {
                    Icon(Icons.Default.WorkOutline, contentDescription = null)
                    Spacer(modifier = Modifier.width(8.dp))
                    Text("Ver Serviços")
                }

                // Botão para editar
                OutlinedButton(
                    onClick = { navController.navigate("mei_form?meiId=${meiId}") },
                    modifier = Modifier.fillMaxWidth()
                ) {
                    Icon(Icons.Default.Edit, contentDescription = null)
                    Spacer(modifier = Modifier.width(8.dp))
                    Text("Editar MEI")
                }

                // Botão para excluir
                Button(
                    onClick = {
                        mei.let {
                            viewModel.deletarMei(it)
                            navController.popBackStack()
                        }
                    },
                    modifier = Modifier.fillMaxWidth(),
                    colors = ButtonDefaults.buttonColors(
                        containerColor = MaterialTheme.colorScheme.error
                    )
                ) {
                    Icon(Icons.Default.Delete, contentDescription = null)
                    Spacer(modifier = Modifier.width(8.dp))
                    Text("Excluir MEI")
                }
            }
        }
    }
}

@Composable
fun DetailRow(label: String, value: String) {
    Row(
        modifier = Modifier.fillMaxWidth(),
        horizontalArrangement = Arrangement.SpaceBetween
    ) {
        Text(
            text = label,
            style = MaterialTheme.typography.bodyLarge,
            fontWeight = FontWeight.Bold,
            color = MaterialTheme.colorScheme.primary
        )
        Text(
            text = value,
            style = MaterialTheme.typography.bodyLarge
        )
    }
}