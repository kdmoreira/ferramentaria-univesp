using Domain.DTOs;
using Domain.DTOs.Usuario;
using Domain.Interfaces.Services;
using Domain.OperationResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class LoginController : BaseApiController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUsuarioService _usuarioService;

        public LoginController(ILogger<LoginController> logger,
            IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Login de um usuário.
        /// </summary>
        /// <param name="dto">Informações de login: login (matrícula) e senha.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var response = await _usuarioService.LoginAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro durante o login:\n{msgErro}");
            }
        }

        /// <summary>
        /// Altera a senha de um usuário.
        /// </summary>
        /// <param name="dto">Informações de troca de senha: login (matrícula), nova senha e token.</param>
        /// <returns></returns>
        [HttpPost("AlterarSenha")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DefaultSuccessResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaDTO dto)
        {
            try
            {
                await _usuarioService.AlterarSenhaAsync(dto);
                return Ok(new DefaultSuccessResponse() { Message = "Senha alterada com sucesso!" });
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao alterar a senha:\n{msgErro}");
            }
        }

        /// <summary>
        /// Realiza o primeiro acesso de um usuário.
        /// </summary>
        /// <param name="dto">Informações de primeiro acesso: nova senha e token.</param>
        /// <returns></returns>
        [HttpPost("PrimeiroAcesso")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DefaultSuccessResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PrimeiroAcesso([FromBody] NovaSenhaDTO dto)
        {
            try
            {
                await _usuarioService.PrimeiroAcessoAsync(dto);
                return Ok(new DefaultSuccessResponse()
                {
                    Message = "Tudo ok! Você já pode realizar o login com a senha que acabou de definir!"
                });
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao realizar o primeiro acesso:\n{msgErro}");
            }
        }

        /// <summary>
        /// Recupera a senha de um usuário.
        /// </summary>
        /// <param name="dto">Login.</param>
        /// <returns></returns>
        [HttpPost("RecuperarSenha")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RecuperarSenha([FromBody] RecuperarSenhaDTO dto)
        {
            try
            {
                var response = await _usuarioService.RecuperarSenhaAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao solicitar a recuperação de senha:\n{msgErro}");
            }
        }
    }
}
