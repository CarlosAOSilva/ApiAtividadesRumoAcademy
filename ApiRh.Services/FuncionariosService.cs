using ApiRh.Domain.Models;
using ApiRh.Domain.Validacao;
using ApiRh.Repositories;
using System.Text.RegularExpressions;

namespace ApiRh.Services
{
    public class FuncionariosService
    {
        private readonly FuncionariosRepositories _repositorio;
        public FuncionariosService(FuncionariosRepositories repositories)
        {
            _repositorio = repositories;
        }

        public List<Funcionarios> ListarFuncionarios(string? nomeFuncionario)
        {
            try
            {

                _repositorio.AbrirConexao();
                return _repositorio.ListarFuncionarios(nomeFuncionario);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public Funcionarios Obter(int idFuncionario)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.SeExiste(idFuncionario);
                return _repositorio.Obter(idFuncionario);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Atualizar(Funcionarios model)
        {
            try
            {
                ValidarModelFuncionarios(model, true);
                _repositorio.AbrirConexao();
                _repositorio.Atualizar(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Deletar(int idFuncionario)
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Deletar(idFuncionario);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Inserir(Funcionarios model)
        {
            try
            {
                ValidarModelFuncionarios(model);
                _repositorio.AbrirConexao();
                _repositorio.InserirFuncionarios(model);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        private static void ValidarModelFuncionarios(Funcionarios model, bool isUpdate = false)
        {
           
            if (model == null)
                throw new InvalidOperationException("O Json está mal formatado");
            if (!isUpdate)
            {
                if (string.IsNullOrWhiteSpace(model.cpf))
                    throw new InvalidOperationException("O CPF é Obrigatório.");

                if (!Utilities.ValidarCpf(model.cpf))
                    throw new InvalidOperationException("CPF Inválido.");
            }

            if (string.IsNullOrWhiteSpace(model.nomeFuncionario))
                throw new InvalidOperationException("O Nome é Obrigatório.");

            if (model.nomeFuncionario.Trim().Length < 3 || model.nomeFuncionario.Trim().Length > 255)
                throw new InvalidOperationException("O Nome precisa ter entre 3 a 255 caracteres.");

            if (!Utilities.IsValidEmail(model.emailFuncionario))
                throw new InvalidOperationException("Email Inválido.");

            if (string.IsNullOrWhiteSpace(model.emailFuncionario))
                throw new InvalidOperationException("O Email é Obrigatório.");

            if (model.emailFuncionario.Trim().Length < 8 || model.emailFuncionario.Trim().Length > 255)
                throw new InvalidOperationException("O Nome precisa ter entre 8 a 255 caracteres.");

            if (ObterIdade(model.dataNascimento) < 18)
                throw new InvalidOperationException("Somente maiores de 18 anos podem ser Cadastrados.");

            if (model.telefoneFuncionario is not null
                &&
                (model.telefoneFuncionario.Trim().Length < 10
                || model.telefoneFuncionario.Trim().Length > 15
                || model.telefoneFuncionario.Trim().Length != Utilities.RemoverMascaraTelefone(model.telefoneFuncionario).Length)
                )
                throw new InvalidOperationException("O mínimo que o telefone pode ter são 10 a 15 digitos, e não pode conter mascaras.");
        }
        private static int ObterIdade(DateTime DataNascimento)
        {
            var today = DateTime.Today;
            var age = today.Year - DataNascimento.Year;
            if (DataNascimento > today.AddYears(-age)) age--;
            return age;
        }
      
    }
}

