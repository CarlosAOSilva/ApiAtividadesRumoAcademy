using ApiRh.Domain.Models;
using ApiRh.Repositories;
using System.Text.RegularExpressions;

namespace ApiRh.Services
{
    public class PontosService
    {
        private readonly PontosRepositories _repositorio;
        public PontosService(PontosRepositories repositories)
        {
            _repositorio = repositories;
        }

        public List<Pontos> ListarPontos(int idPonto)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarPontos(idPonto);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public Pontos Obter(int pontoId)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.SeExiste(pontoId);
                return _repositorio.Obter(pontoId);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Atualizar(Pontos model)
        {
            try
            {
                ValidarModelPontos(model);
                _repositorio.AbrirConexao();
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(int pontoId)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Deletar(pontoId);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Pontos model)
        {
            try
            {
                ValidarModelPontos(model);
                _repositorio.AbrirConexao();
                _repositorio.InserirCargos(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        private static void ValidarModelPontos(Pontos model)
        {
            if (model is null)
                throw new InvalidOperationException("O json está mal formatado, ou foi enviado vazio.");

            if (string.IsNullOrWhiteSpace(model.justificativa))
                throw new InvalidOperationException("A justificativa é obrigatória.");

            if (model.justificativa.Trim().Length < 3 || model.justificativa.Trim().Length > 255)
                throw new InvalidOperationException("A justificativa precisa ter entre 3 a 255 caracteres.");
        }

    }
}
