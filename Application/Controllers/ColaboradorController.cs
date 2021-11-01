using Domain.DTOs;
using Domain.Enums;
using Domain.Interfaces.Services;
using Domain.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class ColaboradorController : BaseApiController
    {
        private readonly ILogger<ColaboradorController> _logger;
        private readonly IColaboradorService _colaboradorService;

        public ColaboradorController(ILogger<ColaboradorController> logger,
            IColaboradorService colaboradorService)
        {
            _logger = logger;
            _colaboradorService = colaboradorService;
        }

        /// <summary>
        /// Busca os colaboradores de forma paginada.
        /// </summary>
        /// <param name="cpf">Busca pelo CPF do colaborador.</param>
        /// <param name="matricula">Busca pela matrícula do colaborador.</param>
        /// <param name="nome">Busca pelo nome do colaborador.</param>
        /// <param name="numeroPagina">Número da página iniciando pelo índice 0.</param>
        /// <param name="tamanhoPagina">Tamanho da página.</param>
        /// <returns></returns>
        [HttpGet]
        //[AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType(typeof(List<ColaboradorListagemDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Get([FromQuery] string cpf, string matricula, string nome, int numeroPagina, int tamanhoPagina)
        {
            try
            {
                var retorno = _colaboradorService.ListaPaginada(cpf, matricula, nome, numeroPagina, tamanhoPagina);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao buscar os colaboradores:\n {msgErro}");
            }
        }

        /// <summary>
        /// Cadastra um novo colaborador.
        /// </summary>
        /// <remarks>Perfil: 1-Colaborador, 2-Supervisor, 3-Gerente<br/>
        /// Role: 1-Administrador, 2-Colaborador
        /// </remarks>
        /// <param name="dto">Informações do colaborador: CPF, matrícula, nome, sobrenome, email, telefone, cargo,
        /// empresa, perfil, role, identificador do supervisor.</param>
        /// <returns></returns>
        [HttpPost]
        //[AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] ColaboradorCriacaoDTO dto)
        {
            try
            {
                await _colaboradorService.AdicionarAsync(dto, base.UsuarioLogadoID.Value);
                return Ok("Colaborador cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao cadastrar o colaborador:\n {msgErro}");
            }
        }

        /// <summary>
        /// Atualiza um colaborador.
        /// </summary>
        /// <remarks>Perfil: 1-Colaborador, 2-Supervisor, 3-Gerente<br/>
        /// Role: 1-Administrador, 2-Colaborador
        /// </remarks>
        /// <param name="dto">Informações do colaborador: CPF, matrícula, nome, sobrenome, email, telefone, cargo,
        /// empresa, perfil, role, identificador do supervisor.</param>
        /// <returns></returns>
        [HttpPut]
        //[AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put([FromBody] ColaboradorEdicaoDTO dto)
        {
            try
            {
                await _colaboradorService.AtualizarAsync(dto, base.UsuarioLogadoID.Value);
                return Ok("Colaborador atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao atualizar o colaborador:\n {msgErro}");
            }
        }

        /// <summary>
        /// Inativa um colaborador.
        /// </summary>
        /// <param name="id">Identificador do colaborador.</param>
        /// <returns></returns>
        [HttpPut("Inativar")]
        //[AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Inativar(Guid id)
        {
            try
            {
                await _colaboradorService.InativarAsync(id, base.UsuarioLogadoID.Value);
                return Ok("Colaborador inativado com sucesso!");
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao inativar o colaborador:\n {msgErro}");
            }
        }

        /// <summary>
        /// Ativa um colaborador.
        /// </summary>
        /// <param name="id">Identificador do colaborador.</param>
        /// <returns></returns>
        [HttpPut("Ativar")]
        //[AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Ativar(Guid id)
        {
            try
            {
                await _colaboradorService.AtivarAsync(id, base.UsuarioLogadoID.Value);
                return Ok("Colaborador ativado com sucesso!");
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao ativar o colaborador:\n {msgErro}");
            }
        }

        /// <summary>
        /// Busca um colaborador.
        /// </summary>
        /// <param name="id">Identificador do colaborador.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        //[AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType(typeof(ColaboradorDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var colaborador = await _colaboradorService.BuscarPorIDAsync(id);
                return Ok(colaborador);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao buscar o colaborador:\n {msgErro}");
            }
        }
    }
}
