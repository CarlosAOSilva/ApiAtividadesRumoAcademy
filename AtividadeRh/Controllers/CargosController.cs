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
    public class CargosController : ControllerBase
    {
        private readonly CargosService _service;
        public CargosController(CargosService service)
        {
            _service = service;
        }

        /// <summary>
        /// Rota que permite listar os cargos registrados - Você pode buscar um cargo em específico
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "2")]
        [HttpGet("cargos")]
        public IActionResult Listar([FromQuery] string? descricao)
        {
            return StatusCode(200, _service.ListarCargos(descricao));
        }

        /// <summary>
        /// Rota que permite listar um cargos em específico -
        /// Campos obrigatórios: idCargos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "2")]
        [HttpGet("cargos/{idCargo}")]
        public IActionResult Obter([FromRoute] int idCargo)
        {
            return StatusCode(200, _service.Obter(idCargo));
        }

        /// <summary>
        /// Rota para realizar registros de cargos -
        /// Campos obrigatórios: nomeCargo
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "2")]
        [HttpPost("cargos")]
        public IActionResult Inserir([FromBody] Cargos model)
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
        /// Rota que permite apagar um cargo em específico -
        /// Campos obrigatórios: idCargos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "2")]
        [HttpDelete("cargos/{idCargo}")]
        public IActionResult Deletar([FromRoute] int idCargo)
        {
            _service.Deletar(idCargo);
            return StatusCode(200);
        }

        /// <summary>
        /// Rota que permite atualizar um cargo em específico -
        /// Campos obrigatórios: idCargos e nomeCargo
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna os elementos encontrados</response>
        [Authorize(Roles = "2")]
        [HttpPut("cargos")]
        public IActionResult Atualizar([FromBody] Cargos model)
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


