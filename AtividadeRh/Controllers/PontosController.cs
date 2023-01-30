using ApiRh.Domain.Models;
using ApiRh.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AtividadeRh.Controllers
{

    [Authorize]
    [ApiController]
    public class PontosController : ControllerBase
    {
        private readonly PontosService _service;
        public PontosController(PontosService service)
        {
            _service = service;
        }

        /// <summary>
        /// Rota que permite listar os pontos registrados - 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "2")]
        [HttpGet("ponto")]
        public IActionResult Listar([FromQuery] int idPonto)
        {
            return StatusCode(200, _service.ListarPontos(idPonto));
        }

        /// <summary>
        /// Rota que permite listar os pontos registrados em especifíco -  - 
        /// Campos obrigatórios: pontoId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna o elemento encontrado via ID</response>
        [Authorize(Roles = "2")]
        [HttpGet("ponto/{pontoId}")]
        public IActionResult Obter([FromRoute] int pontoId)
        {
            return StatusCode(200, _service.Obter(pontoId));
        }

        /// <summary>
        /// Rota  para realizar registros de pontos - 
        /// Campos obrigatórios: dataHorarioPonto, funcionarioId, justificativa
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna o elemento encontrado via ID</response>
        [HttpPost("ponto")]
        public IActionResult Inserir([FromBody] Pontos model)
        {
            try
            {
                _service.Inserir(model);
                return StatusCode(201);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        /// <summary>
        /// Rota para apagar um registro em específico - 
        /// Campos obrigatórios: pontoId 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna o elemento encontrado via ID</response>
        [Authorize(Roles = "2")]
        [HttpDelete("ponto/{pontoId}")]
        public IActionResult Deletar([FromRoute] int pontoId)
        {
            _service.Deletar(pontoId);
            return StatusCode(200);
        }

        /// <summary>
        /// Rota para atualizar um registro em específico  - 
        /// Campos obrigatórios: pontoId, dataHorarioPonto, justificativa, funcionarioId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna o elemento encontrado via ID</response>
        [Authorize(Roles = "2")]
        [HttpPut("ponto")]
        public IActionResult Atualizar([FromBody] Pontos model)
        {
            try
            {
                _service.Atualizar(model);
                return StatusCode(201);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
