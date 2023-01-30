using ApiRh.Domain.Models;
using ApiRh.Repositories;
using System.Text.RegularExpressions;

namespace ApiRh.Services
{

    public class EquipesService
    {
        private readonly EquipesRepositories _repositorio;
        public EquipesService(EquipesRepositories repositories)
        {
            _repositorio = repositories;
        }

        public List<Equipes> ListarEquipes(int equipesId)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarEquipes(equipesId);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public Equipes Obter(int equipesId)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.SeExiste(equipesId);
                return _repositorio.Obter(equipesId);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Atualizar(Equipes model)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(int equipesId)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Deletar(equipesId);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Equipes model)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.InserirCargos(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

    }
}
