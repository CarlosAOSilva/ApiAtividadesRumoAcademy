using ApiRh.Domain.Models;
using ApiRh.Repositories;
using System.Text.RegularExpressions;

namespace ApiRh.Services
{
    public class LiderancasService
    {
        private readonly LiderancasRepositories _repositorio;
        public LiderancasService(LiderancasRepositories repositories)
        {
            _repositorio = repositories;
        }

        public List<Liderancas> ListarLiderancas(string? descricaoEquipe)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarLiderancas(descricaoEquipe);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public Liderancas Obter(int liderancaId)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.SeExiste(liderancaId);
                return _repositorio.Obter(liderancaId);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Atualizar(Liderancas model)
        {
            try
            {
                ValidarModelLiderancas(model);
                _repositorio.AbrirConexao();
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(int liderancaId)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Deletar(liderancaId);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Liderancas model)
        {
            try
            {
                ValidarModelLiderancas(model);
                _repositorio.AbrirConexao();
                _repositorio.InserirLiderancas(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        private static void ValidarModelLiderancas(Liderancas model)
        {
            if (model is null)
                throw new InvalidOperationException("O json está mal formatado, ou foi enviado vazio.");

            if (string.IsNullOrWhiteSpace(model.descricaoEquipe))
                throw new InvalidOperationException("O nome da equipe é obrigatório.");

            if (model.descricaoEquipe.Trim().Length < 3 || model.descricaoEquipe.Trim().Length > 255)
                throw new InvalidOperationException("O nome da equipe precisa ter entre 3 a 255 caracteres.");
        }

    }
}
