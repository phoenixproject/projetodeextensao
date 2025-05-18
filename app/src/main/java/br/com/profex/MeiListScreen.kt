package br.com.profex

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material3.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.livedata.observeAsState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavController

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun MeiListScreen(
    navController: NavController,
    viewModel: MeiViewModel = viewModel(
        factory = MeiViewModelFactory(
            (navController.context.applicationContext as MeiApplication).meiRepository
        )
    )
) {
    // Corrigindo o collectAsState com o tipo apropriado
    val meiList by viewModel.todosMei.observeAsState(initial = emptyList())

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text("Lista de MEIs") },
                actions = {
                    IconButton(onClick = { navController.navigate("mei_form") }) {
                        Icon(Icons.Default.Add, contentDescription = "Adicionar MEI")
                    }
                }
            )
        }
    ) { paddingValues ->
        if (meiList.isEmpty()) {
            Box(
                modifier = Modifier
                    .fillMaxSize()
                    .padding(paddingValues),
                contentAlignment = Alignment.Center
            ) {
                Text("Nenhum MEI cadastrado")
            }
        } else {
            LazyColumn(
                modifier = Modifier
                    .fillMaxSize()
                    .padding(paddingValues),
                contentPadding = PaddingValues(16.dp)
            ) {
                items(meiList) { mei ->
                    MeiItem(mei = mei, onItemClick = {
                        navController.navigate("mei_detail/${mei.id}")
                    })
                }
            }
        }
    }
}

@Composable
fun MeiItem(mei: MEI, onItemClick: () -> Unit) {
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
            Text(
                text = mei.nome,
                style = MaterialTheme.typography.titleMedium,
                fontWeight = FontWeight.Bold
            )
            Spacer(modifier = Modifier.height(4.dp))
            Text(
                text = "CNPJ: ${mei.cnpj}",
                style = MaterialTheme.typography.bodyMedium
            )
            Spacer(modifier = Modifier.height(4.dp))
            Text(
                text = "Telefone: ${mei.telefone}",
                style = MaterialTheme.typography.bodyMedium
            )
        }
    }
}