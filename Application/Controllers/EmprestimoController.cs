using Domain.DTOs;
using Domain.Enums;
using Domain.Interfaces.Services;
using Domain.OperationResponses;
using Domain.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class EmprestimoController : BaseApiController
    {
        private readonly ILogger<EmprestimoController> _logger;
        private readonly IEmprestimoService _emprestimoService;

        public EmprestimoController(ILogger<EmprestimoController> logger,
            IEmprestimoService emprestimoService)
        {
            _logger = logger;
            _emprestimoService = emprestimoService;
        }

        /// <summary>
        /// Busca os empréstimos em andamento de forma paginada.
        /// </summary>
        /// <param name="ferramenta">Busca pelo nome da ferramenta.</param>
        /// <param name="colaborador">Busca pelo nome do colaborador.</param>
        /// <param name="numeroPagina">Número da página iniciando pelo índice 0.</param>
        /// <param name="tamanhoPagina">Tamanho da página.</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType(typeof(ListagemResponse<EmprestimoListagemDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromQuery] string ferramenta, string colaborador, int numeroPagina, int tamanhoPagina)
        {
            try
            {
                var retorno = await _emprestimoService.ListaPaginadaAsync(ferramenta, colaborador, numeroPagina, tamanhoPagina, base.UsuarioLogadoID);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao buscar os empréstimos:\n{msgErro}");
            }
        }

        /// <summary>
        /// Busca um empréstimo.
        /// </summary>
        /// <param name="id">Identificador do empréstimo.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var emprestimo = await _emprestimoService.BuscarPorIDAsync(id);
                return Ok(emprestimo);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao buscar o empréstimo:\n{msgErro}");
            }
        }

        /// <summary>
        /// Realiza um novo empréstimo de ferramenta.
        /// </summary>
        /// <param name="dto">Informações do empréstimo> ID da ferramenta, ID do colaborador, prazo em dias, observação opcional.</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeRoles(RoleEnum.Administrador, RoleEnum.Colaborador)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] EmprestimoCriacaoDTO dto)
        {
            try
            {
                await _emprestimoService.EmprestarAsync(dto, base.UsuarioLogadoID);
                return Ok("Empréstimo realizado com sucesso!");
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao realizar o empréstimo:\n{msgErro}");
            }
        }

        /// <summary>
        /// Realiza uma devolução de ferramenta.
        /// </summary>
        /// <param name="id">Identificador do empréstimo.</param>
        /// <returns></returns>
        [HttpPut]
        [AuthorizeRoles(RoleEnum.Administrador)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(Guid id)
        {
            try
            {
                await _emprestimoService.DevolverAsync(id, base.UsuarioLogadoID);
                return Ok("Devolução realizada com sucesso!");
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao realizar a devolução:\n{msgErro}");
            }
        }

        /// <summary>
        /// Busca os empréstimos ativos de um colaborador.
        /// </summary>
        /// <param name="id">Identificador do colaborador.</param>
        /// <returns></returns>
        [HttpGet("Colaborador/{id}")]
        [AuthorizeRoles(RoleEnum.Administrador, RoleEnum.Colaborador)]
        [ProducesResponseType(typeof(ListagemResponse<EmprestimoPorColaboradorDTO>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByColaboradorID(Guid id)
        {
            try
            {
                var emprestimos = await _emprestimoService.BuscarPorColaboradorAsync(id, base.UsuarioLogadoID);
                return Ok(emprestimos);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao buscar os empréstimos:\n{msgErro}");
            }
        }
    }
}
