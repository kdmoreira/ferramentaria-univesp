using Domain.DTOs;
using Domain.Enums;
using Domain.Interfaces.Services;
using Domain.OperationResponses;
using Domain.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class FerramentaController : BaseApiController
    {
        private readonly ILogger<FerramentaController> _logger;
        private readonly IFerramentaService _ferramentaService;

        public FerramentaController(ILogger<FerramentaController> logger,
            IFerramentaService ferramentaService)
        {
            _logger = logger;
            _ferramentaService = ferramentaService;
        }

        /// <summary>
        /// Busca as ferramentas de forma paginada.
        /// </summary>
        /// <param name="codigo">Busca pelo código da ferramenta.</param>
        /// <param name="nome">Busca pelo nome da ferramenta.</param>
        /// <param name="numeroPagina">Número da página iniciando pelo índice 0.</param>
        /// <param name="tamanhoPagina">Tamanho da página.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeRoles(RoleEnum.Administrador, RoleEnum.Colaborador)]
        [ProducesResponseType(typeof(ListagemResponse<FerramentaListagemDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Get([FromQuery] string codigo, string nome, int numeroPagina, int tamanhoPagina)
        {
            try
            {
                var retorno = _ferramentaService.ListaPaginada(codigo, nome, numeroPagina, tamanhoPagina);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao buscar as ferramentas:\n{msgErro}");
            }
        }

        /// <summary>
        /// Busca uma ferramenta.
        /// </summary>
        /// <param name="id">Identificador da ferramenta.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType(typeof(FerramentaDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var ferramenta = await _ferramentaService.BuscarPorIDAsync(id);
                return Ok(ferramenta);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao buscar a ferramenta:\n{msgErro}");
            }
        }

        /// <summary>
        /// Cadastra uma nova ferramenta.
        /// </summary>
        /// <param name="dto">Informações da ferramenta: código, descrição, quantidade, valor de compra, número patrimonial, 
        /// fabricante, localização.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] FerramentaCriacaoDTO dto)
        {
            try
            {
                await _ferramentaService.AdicionarAsync(dto, base.UsuarioLogadoID);
                return Ok("Ferramenta cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao cadastrar a ferramenta:\n{msgErro}");
            }
        }

        /// <summary>
        /// Atualiza uma ferramenta.
        /// </summary>
        /// <param name="dto">Informações da ferramenta: id, código, descrição, quantidade, valor de compra, número patrimonial,
        /// fabricante, localização.</param>
        /// <returns></returns>
        [HttpPut]
        [AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put([FromBody] FerramentaEdicaoDTO dto)
        {
            try
            {
                await _ferramentaService.AtualizarAsync(dto, base.UsuarioLogadoID);
                return Ok("Ferramenta atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao atualizar a ferramenta:\n{msgErro}");
            }
        }

        /// <summary>
        /// Inativa uma ferramenta.
        /// </summary>
        /// <param name="id">Identificador da ferramenta.</param>
        /// <returns></returns>
        [HttpPut("Inativar")]
        [AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Inativar(Guid id)
        {
            try
            {
                await _ferramentaService.InativarAsync(id, base.UsuarioLogadoID);
                return Ok("Ferramenta inativada com sucesso!");
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao inativar a ferramenta:\n{msgErro}");
            }
        }

        /// <summary>
        /// Ativa uma ferramenta.
        /// </summary>
        /// <param name="id">Identificador da ferramenta.</param>
        /// <returns></returns>
        [HttpPut("Ativar")]
        [AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Ativar(Guid id)
        {
            try
            {
                await _ferramentaService.AtivarAsync(id, base.UsuarioLogadoID);
                return Ok("Ferramenta ativada com sucesso!");
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao ativar a ferramenta:\n{msgErro}");
            }
        }

        /// <summary>
        /// Busca as categorias de ferramentas.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Categoria")]
        [AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType(typeof(ListagemResponse<CategoriaDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCategorias()
        {
            try
            {
                var categorias = await _ferramentaService.BuscarCategoriasAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao buscar as categorias:\n{msgErro}");
            }
        }
    }
}