﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjetoDeExtensao.BlazorApp._MODEL._SISTEMAS;
using ProjetoDeExtensao.BlazorApp._REPOSITORIO;
using ProjetoDeExtensao.Shared._MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDeExtensao.BlazorApp._APLICACAO
{
	public class GestorDeTipoDeUsuario
	{
		public ProjetodeextensaoContext _projetoextensao { get; set; }
		public RepositorioDeTipoDeUsuario RepositorioDeTipoDeUsuario { get; set; }

		public GestorDeTipoDeUsuario(ProjetodeextensaoContext projetoextensao)
		{
			this.RepositorioDeTipoDeUsuario = new RepositorioDeTipoDeUsuario(projetoextensao);
		}

		public TipoUsuarioDTO ConverterParaTipoDeUsuarioDTO(TipoUsuario tipoUsuario)
		{
			return new TipoUsuarioDTO
			{
				TpUsuario = tipoUsuario.TpUsuario,
				DsTpUsuario = tipoUsuario.DsTpUsuario
			};
		}

		public List<TipoUsuarioDTO> ConverterListaDeTipoDeUsuarioParaFormatoDTO(List<TipoUsuario> listaDeTipoDeUsuario)
		{
			List<TipoUsuarioDTO> listaDeTipoDeUsuarioDTO = new List<TipoUsuarioDTO>();
			foreach (var tipoUsuario in listaDeTipoDeUsuario)
			{
				listaDeTipoDeUsuarioDTO.Add(ConverterParaTipoDeUsuarioDTO(tipoUsuario));
			}
			return listaDeTipoDeUsuarioDTO;
		}

		public TipoUsuarioDTO ObterTipoDeUsuarioPorId(int id)
		{
			return ConverterParaTipoDeUsuarioDTO(RepositorioDeTipoDeUsuario.ObterTipoDeUsuarioPorId(id));
		}

		public List<TipoUsuarioDTO> ObterTodosOsTiposDeUsuarios()
		{
			return ConverterListaDeTipoDeUsuarioParaFormatoDTO(RepositorioDeTipoDeUsuario.ObterTodosOsTiposDeUsuarios());
		}

		public TipoUsuarioDTO ObterTipoDeUsuarioPorDescricao(string descricao)
		{
			return ConverterParaTipoDeUsuarioDTO(RepositorioDeTipoDeUsuario.ObterTipoDeUsuarioPorDescricao(descricao));
		}

		public bool VerificarSeTipoDeUsuarioComMesmoEmailJaExiste(TipoUsuario tipoUsuario)
		{
			return RepositorioDeTipoDeUsuario.VerificarSeTipoDeUsuarioComMesmaDescricaoJaExiste(tipoUsuario);
		}

		public void InserirTipoDeUsuario(TipoUsuario tipoUsuario)
		{
			RepositorioDeTipoDeUsuario.InserirTipoDeUsuario(tipoUsuario);	
		}

		public int BuscarQuantidadeRegistros()
		{
			return RepositorioDeTipoDeUsuario.BuscarQuantidadeRegistros();
		}

		public void RemoverTipoDeUsuario(TipoUsuario tipoUsuario)
		{
			RepositorioDeTipoDeUsuario.RemoverTipoDeUsuarioUsuario(tipoUsuario);
		}

		public void AtualizarTipoDeUsuario(TipoUsuario tipoUsuario)
		{
			RepositorioDeTipoDeUsuario.AtualizarTipoDeUsuario(tipoUsuario);
		}
	}
}
