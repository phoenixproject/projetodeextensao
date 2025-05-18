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
fun MeiFormScreen(
    navController: NavController,
    meiId: Int,
    viewModel: MeiViewModel = viewModel(
        factory = MeiViewModelFactory(
            (navController.context.applicationContext as MeiApplication).meiRepository
        )
    )
) {
    val scope = rememberCoroutineScope()
    var mei by remember { mutableStateOf(MEI(0, "", "", "", "", "", "")) }
    var isEdit by remember { mutableStateOf(false) }

    // Carrega dados do MEI se for edição
    LaunchedEffect(meiId) {
        if (meiId > 0) {
            isEdit = true
            viewModel.obterMeiPorId(meiId).observe(navController.context as LifecycleOwner) { meiEncontrado ->
                mei = meiEncontrado
            }
        }
    }

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text(if (isEdit) "Editar MEI" else "Novo MEI") },
                actions = {
                    IconButton(onClick = {
                        if (isEdit) {
                            viewModel.atualizarMei(mei)
                        } else {
                            viewModel.inserirMei(mei)
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
            OutlinedTextField(
                value = mei.nome,
                onValueChange = { mei = mei.copy(nome = it) },
                label = { Text("Nome") },
                modifier = Modifier.fillMaxWidth()
            )

            OutlinedTextField(
                value = mei.cnpj,
                onValueChange = { mei = mei.copy(cnpj = it) },
                label = { Text("CNPJ") },
                modifier = Modifier.fillMaxWidth()
            )

            OutlinedTextField(
                value = mei.dataAbertura,
                onValueChange = { mei = mei.copy(dataAbertura = it) },
                label = { Text("Data de Abertura (DD/MM/AAAA)") },
                modifier = Modifier.fillMaxWidth()
            )

            OutlinedTextField(
                value = mei.telefone,
                onValueChange = { mei = mei.copy(telefone = it) },
                label = { Text("Telefone") },
                modifier = Modifier.fillMaxWidth()
            )

            OutlinedTextField(
                value = mei.email,
                onValueChange = { mei = mei.copy(email = it) },
                label = { Text("Email") },
                modifier = Modifier.fillMaxWidth()
            )

            OutlinedTextField(
                value = mei.endereco,
                onValueChange = { mei = mei.copy(endereco = it) },
                label = { Text("Endereço") },
                modifier = Modifier.fillMaxWidth()
            )

            Button(
                onClick = {
                    if (isEdit) {
                        viewModel.atualizarMei(mei)
                    } else {
                        viewModel.inserirMei(mei)
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