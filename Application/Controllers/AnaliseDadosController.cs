using Domain.DTOs.AnaliseDados;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.IO;
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

        /// <summary>
        /// Exporta planilha de relatório de ferramentas.
        /// </summary>
        /// <returns></returns>
        [HttpPost("PlanilhaRelatorioFerramentas")]
        [ProducesResponseType(typeof(File), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ExportRelatorioFerramentas()
        {
            try
            {
                var stream = new MemoryStream();

                using (var ep = new ExcelPackage(stream))
                {
                    ep.Workbook.Properties.Author = "Ferramentaria FURNAS";
                    ep.Workbook.Properties.Title = "Relatório Ferramentaria";
                    ep.Workbook.Properties.Subject = "Análise de Dados de Ferramentas";
                    ep.Workbook.Properties.Created = DateTime.Now;
                    ep.Workbook.Date1904 = true;

                    ExcelWorksheet p1 = ep.Workbook.Worksheets.Add("Ferramentas");

                    var relatorio = await _analiseDadosService.GerarRelatorioFerramentas();

                    p1.Column(1).Style.Font.Bold = true;
                    p1.Column(1).Width = 35;

                    p1.Cells[1, 1].Value = "Quantidade Total";
                    p1.Cells[2, 1].Value = "Quantidade Disponível";
                    p1.Cells[3, 1].Value = "Quantidade Emprestada";
                    p1.Cells[4, 1].Value = "Média Quantidade por Ferramenta";
                    p1.Cells[5, 1].Value = "Quantidade Inativas";

                    p1.Cells[1, 2].Value = relatorio.QuantidadeTotal;
                    p1.Cells[2, 2].Value = relatorio.QuantidadeDisponivel;
                    p1.Cells[3, 2].Value = relatorio.QuantidadeEmprestada;
                    p1.Cells[4, 2].Value = relatorio.MediaQuantidadePorFerramenta;
                    p1.Cells[5, 2].Value = relatorio.QuantidadeInativas;

                    ep.Save();
                };

                stream.Position = 0;
                string excelName = $"RelatorioFerramentas-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception ex)
            {
                var msgErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, msgErro);
                return BadRequest($"Ocorreu um erro ao exportar o relatório de ferramentas:\n{msgErro}");
            }
        }
    }
}
