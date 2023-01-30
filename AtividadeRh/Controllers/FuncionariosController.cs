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
    public class FuncionariosController : ControllerBase
    {
        private readonly FuncionariosService _service;
        public FuncionariosController(FuncionariosService service)
        {
            _service = service;
        }

        /// <summary>
        /// Rota que permite listar os funcionários registrados - Você pode buscar um funcionario em específico
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna o elemento encontrado via ID</response>
        [Authorize(Roles = "2")]
        [HttpGet("funcionarios")]
        public IActionResult Listar([FromQuery] string? nomeFuncionario)
        {
            return StatusCode(200, _service.ListarFuncionarios(nomeFuncionario));
        }

        /// <summary>
        /// Rota que permite listar um funcionário em específico -
        /// Campos obrigatórios: funcionarioId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna o elemento encontrado via ID</response>
        [Authorize(Roles = "2")]
        [HttpGet("funcionarios/{idFuncionario}")]
        public IActionResult Obter([FromRoute] int idFuncionario)
        {
            return StatusCode(200, _service.Obter(idFuncionario));
        }

        /// <summary>
        /// Rota para realizar registros de funcionários - 
        /// Campos obrigatórios: nomeFuncionario, cpf, dataNascimento, dataAdmissao, telefoneFuncionario, emailFuncionario, cargoId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna o elemento encontrado via ID</response>
        [Authorize(Roles = "2")]
        [HttpPost("funcionarios")]
        public IActionResult Inserir([FromBody] Funcionarios model)
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
        /// Rota que permite apagar o registro de um funcionário em específico -
        /// Campos obrigatórios: funcionarioId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna o elemento encontrado via ID</response>
        [Authorize(Roles = "2")]
        [HttpDelete("funcionarios/{idFuncionario}")]
        public IActionResult Deletar([FromRoute] int idFuncionario)
        {
            _service.Deletar(idFuncionario);
            return StatusCode(200);
        }


        /// <summary>
        /// Rota que permite atualizar o registro de um funcionário em específico -
        /// Campos obrigatórios: funcionarioId, nomeFuncionario, cpf, dataNascimento, dataAdmissao, telefoneFuncionario, emailFuncionario, cargoId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna o elemento encontrado via ID</response>
        [Authorize(Roles = "2")]
        [HttpPut("funcionarios")]
        public IActionResult Atualizar([FromBody] Funcionarios model)
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





