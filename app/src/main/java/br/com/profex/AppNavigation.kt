package br.com.profex

import androidx.compose.runtime.Composable
import androidx.navigation.NavType
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import androidx.navigation.navArgument

@Composable
fun AppNavigation() {
    val navController = rememberNavController()

    NavHost(navController = navController, startDestination = "home") {
        composable("home") {
            HomeScreen(navController = navController)
        }
        composable("mei_list") {
            MeiListScreen(navController = navController)
        }
        composable(
            "mei_detail/{meiId}",
            arguments = listOf(navArgument("meiId") { type = NavType.IntType })
        ) { backStackEntry ->
            val meiId = backStackEntry.arguments?.getInt("meiId") ?: 0
            MeiDetailScreen(navController = navController, meiId = meiId)
        }
        composable("mei_form?meiId={meiId}",
            arguments = listOf(navArgument("meiId") {
                type = NavType.IntType
                defaultValue = -1
            })
        ) { backStackEntry ->
            val meiId = backStackEntry.arguments?.getInt("meiId") ?: -1
            MeiFormScreen(navController = navController, meiId = meiId)
        }
        composable("servico_list?meiId={meiId}",
            arguments = listOf(navArgument("meiId") {
                type = NavType.IntType
                defaultValue = -1
            })
        ) { backStackEntry ->
            val meiId = backStackEntry.arguments?.getInt("meiId") ?: -1
            ServicoListScreen(navController = navController, meiId = meiId)
        }
        composable(
            "servico_detail/{servicoId}",
            arguments = listOf(navArgument("servicoId") { type = NavType.IntType })
        ) { backStackEntry ->
            val servicoId = backStackEntry.arguments?.getInt("servicoId") ?: 0
            ServicoDetailScreen(navController = navController, servicoId = servicoId)
        }
        composable("servico_form?servicoId={servicoId}&meiId={meiId}",
            arguments = listOf(
                navArgument("servicoId") {
                    type = NavType.IntType
                    defaultValue = -1
                },
                navArgument("meiId") {
                    type = NavType.IntType
                    defaultValue = -1
                }
            )
        ) { backStackEntry ->
            val servicoId = backStackEntry.arguments?.getInt("servicoId") ?: -1
            val meiId = backStackEntry.arguments?.getInt("meiId") ?: -1
            ServicoFormScreen(
                navController = navController,
                servicoId = servicoId,
                meiId = meiId
            )
        }
    }
}