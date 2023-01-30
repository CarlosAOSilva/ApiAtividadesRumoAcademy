using ApiRh.Domain.Models;
using ApiRh.Repositories;
using System.Text.RegularExpressions;

namespace ApiRh.Services
{
    public class CargosService
    {
        private readonly CargosRepositories _repositorio;
        public CargosService(CargosRepositories repositories)
        {
            _repositorio = repositories; 
        }

        public List<Cargos> ListarCargos(string? descricao)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarCargos(descricao);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public Cargos Obter(int cargoId)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.SeExiste(cargoId);
                return _repositorio.Obter(cargoId);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Atualizar(Cargos model)
        {
            try
            {
                ValidarModelCargos(model);
                _repositorio.AbrirConexao();
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(int cargoId)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Deletar(cargoId);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Cargos model)
        {
            try
            {
                ValidarModelCargos(model);
                _repositorio.AbrirConexao();
                _repositorio.InserirCargos(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        private static void ValidarModelCargos(Cargos model)
        {
            if (model is null)
                throw new InvalidOperationException("O json está mal formatado, ou foi enviado vazio.");

            if (string.IsNullOrWhiteSpace(model.nomeCargo))
                throw new InvalidOperationException("O nome do cargo é obrigatório.");

            if (model.nomeCargo.Trim().Length < 3 || model.nomeCargo.Trim().Length > 255)
                throw new InvalidOperationException("O nome do cargo precisa ter entre 3 a 255 caracteres.");
        }

    }
}


