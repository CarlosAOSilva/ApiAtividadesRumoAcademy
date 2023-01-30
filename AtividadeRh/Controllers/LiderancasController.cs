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
    public class LiderancasController : ControllerBase
    {
        private readonly LiderancasService _service;
        public LiderancasController(LiderancasService service)
        {
            _service = service;
        }

        /// <summary>
        /// Rota que permite listar lideranças registradas -
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "1,2")]
        [HttpGet("liderancas")]
        public IActionResult ListarLiderancas([FromQuery] string? descricaoEquipe)
        {
            return StatusCode(200, _service.ListarLiderancas(descricaoEquipe));
        }

        /// <summary>
        /// Rota que permite listar uma liderança em específico -
        /// Campos obrigatórios: liderancasId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "1,2")]
        [HttpGet("liderancas/{liderancaId}")]
        public IActionResult Obter([FromRoute] int liderancaId)
        {
            return StatusCode(200, _service.Obter(liderancaId));
        }

        /// <summary>
        /// Rota que permite registrar lideranças -
        /// Campos obrigatórios: funcionarioId e descricaoEquipe
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "1,2")]
        [HttpPost("liderancas")]
        public IActionResult Inserir([FromBody] Liderancas model)
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
        /// Rota que permite apagar uma liderança em específico -
        /// Campos obrigatórios: liderancasId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "2")]
        [HttpDelete("liderancas/{liderancaId}")]
        public IActionResult Deletar([FromRoute] int liderancaId)
        {
            _service.Deletar(liderancaId);
            return StatusCode(200);
        }

        /// <summary>
        /// Rota que permite atualizar uma liderança em específico -
        /// Campos obrigatórios: liderancasId, funcionarioId e descricaoEquipe
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "1,2")]
        [HttpPut("liderancas")]
        public IActionResult Atualizar([FromBody] Liderancas model)
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
