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
    public class EquipesController : ControllerBase
    {
        private readonly EquipesService _service;
        public EquipesController(EquipesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Rota que permite listar as equipes registradas - 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "2")]
        [HttpGet("equipes")]
        public IActionResult Listar([FromQuery] int equipeId)
        {
            return StatusCode(200, _service.ListarEquipes(equipeId));
        }

        /// <summary>
        /// Rota que permite listar uma equipe em específico - 
        /// Campos obrigatórios: equipeId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "1,2")]
        [HttpGet("equipes/{equipeId}")]
        public IActionResult Obter([FromRoute] int equipeId)
        {
            return StatusCode(200, _service.Obter(equipeId));
        }

        /// <summary>
        /// Rota para realizar cadastros de equipes - 
        /// Campos obrigatórios: liderancaId e funcionarioId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos cadastrados</response>
        [Authorize(Roles = "1,2")]
        [HttpPost("equipes")]
        public IActionResult Inserir([FromBody] Equipes model)
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
        /// Rota para apagar uma equipe em específico - 
        /// Campos obrigatórios: equipeId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna o elemento encontrado via ID</response>
        [Authorize(Roles = "2")]
        [HttpDelete("equipes/{equipeId}")]
        public IActionResult Deletar([FromRoute] int equipeId)
        {
            _service.Deletar(equipeId);
            return StatusCode(200);
        }

        /// <summary>
        /// Rota para atualizar dados de uma equipe - 
        /// Campos obrigatórios: equipeId, liderancaId e funcionarioId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna o elemento encontrado via ID</response>
        [Authorize(Roles = "1,2")]
        [HttpPut("equipes")]
        public IActionResult Atualizar([FromBody] Equipes model)
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
