﻿using Domain.DTOs.AnaliseDados;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnaliseDadosController : ControllerBase
    {
        private readonly ILogger<AnaliseDadosController> _logger;
        private readonly IAnaliseDadosService _analiseDadosService;

        public AnaliseDadosController(ILogger<AnaliseDadosController> logger,
            IAnaliseDadosService analiseDadosService)
        {
            _logger = logger;
            _analiseDadosService = analiseDadosService;
        }

        /// <summary>
        /// Gera massa de dados de 30 colaboradores, 50 ferramentas e 100 empréstimos.
        /// </summary>
        /// <returns></returns>
        [HttpPost("GerarDados")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post()
        {
            try
            {
                await _analiseDadosService.GerarMassaDados();
                return Ok("Dados gerados e cadastrados com sucesso.");
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao gerar massa de dados:\n{msgErro}");
            }
        }

        /// <summary>
        /// Gera relatório de ferramentas.
        /// </summary>
        /// <returns></returns>
        [HttpPost("RelatorioFerramentas")]
        [ProducesResponseType(typeof(RelatorioFerramentasDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRelatorioFerramentas()
        {
            try
            {
                var relatorio = await _analiseDadosService.GerarRelatorioFerramentas();
                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao gerar o relatório de ferramentas:\n{msgErro}");
            }
        }

        /// <summary>
        /// Gera relatório de empréstimos.
        /// </summary>
        /// <returns></returns>
        [HttpPost("RelatorioEmprestimos")]
        [ProducesResponseType(typeof(RelatorioEmprestimosDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRelatorioEmprestimos()
        {
            try
            {
                var relatorio = await _analiseDadosService.GerarRelatorioEmprestimos();
                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao gerar o relatório de empréstimos:\n{msgErro}");
            }
        }
    }
}
