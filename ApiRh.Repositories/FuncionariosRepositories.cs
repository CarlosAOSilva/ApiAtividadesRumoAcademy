
using System.Data.SqlClient;
using ApiRh.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace ApiRh.Repositories
{
    public class FuncionariosRepositories : Context
    {
        public FuncionariosRepositories(IConfiguration configuration) : base(configuration)
        {
        }

        public void InserirFuncionarios(Funcionarios register)
        {
            string comandoSql = @"INSERT INTO Funcionarios
                                    (NomeDoFuncionario, 
                                     Cpf, 
                                     NascimentoFuncionario, 
                                     CelularFuncionario,
                                     EmailFuncionario,
                                     DataDeAdmissao,
                                     CargoId) 
                                        VALUES
                                    (@NomeDoFuncionario, 
                                     @Cpf, 
                                     @NascimentoFuncionario, 
                                     @CelularFuncionario,
                                     @EmailFuncionario,
                                     @DataDeAdmissao,
                                     @CargoId);";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", register.funcionarioId);
                cmd.Parameters.AddWithValue("@NomeDoFuncionario", register.nomeFuncionario);
                cmd.Parameters.AddWithValue("@Cpf", register.cpf);
                cmd.Parameters.AddWithValue("@NascimentoFuncionario", register.dataNascimento);
                cmd.Parameters.AddWithValue("@CelularFuncionario", register.telefoneFuncionario);
                cmd.Parameters.AddWithValue("@EmailFuncionario", register.emailFuncionario);
                cmd.Parameters.AddWithValue("@DataDeAdmissao", register.dataAdmissao);
                cmd.Parameters.AddWithValue("@CargoId", register.cargoId);

                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(Funcionarios register)
        {
            string comandoSql = @"UPDATE Funcionarios
                                    SET
                                     NomeDoFuncionario = @NomeFuncionario, 
                                     Cpf = @Cpf, 
                                     NascimentoFuncionario = @NascimentoFuncionario, 
                                     CelularFuncionario = @CelularFuncionario,
                                     EmailFuncionario = @EmailFuncionario,
                                     DataDeAdmissao = @DataDeAdmissao,
                                     CargoId = @CargoId
                                    WHERE FuncionarioId = @FuncionarioId;";


            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", register.funcionarioId);
                cmd.Parameters.AddWithValue("@NomeDoFuncionario", register.nomeFuncionario);
                cmd.Parameters.AddWithValue("@Cpf", register.cpf);
                cmd.Parameters.AddWithValue("@NascimentoFuncionario", register.dataNascimento);
                cmd.Parameters.AddWithValue("@CelularFuncionario", register.telefoneFuncionario);
                cmd.Parameters.AddWithValue("@EmailFuncionario", register.emailFuncionario);
                cmd.Parameters.AddWithValue("@DataDeAdmissao", register.dataAdmissao);
                cmd.Parameters.AddWithValue("@CargoId", register.cargoId);

                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para este Identificador {register.funcionarioId}");
            }
        }
        public bool SeExiste(int idFuncionario)
        {
            string comandoSql = @"SELECT COUNT (FuncionarioId) as total FROM Funcionarios WHERE FuncionarioId = @FuncionarioId";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", idFuncionario);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public Funcionarios? Obter(int idFuncionarid)
        {
            string comandoSql = @"SELECT FuncionarioId, 
                                     NomeDoFuncionario, 
                                     Cpf, 
                                     NascimentoFuncionario, 
                                     CelularFuncionario,
                                     EmailFuncionario,
                                     DataDeAdmissao,
                                     CargoId FROM Funcionarios WHERE FuncionarioId = @FuncionarioId";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", idFuncionarid);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        var register = new Funcionarios();

                        register.funcionarioId = (int)rdr["FuncionarioId"];
                        register.nomeFuncionario = Convert.ToString(rdr["NomeDoFuncionario"]);
                        register.cpf = Convert.ToString(rdr["Cpf"]);
                        register.telefoneFuncionario = Convert.ToString(rdr["CelularFuncionario"]);
                        register.emailFuncionario = Convert.ToString(rdr["EmailFuncionario"]);
                        register.dataNascimento = Convert.ToDateTime(rdr["NascimentoFuncionario"]);
                        register.dataAdmissao = Convert.ToDateTime(rdr["DataDeAdmissao"]);
                        register.cargoId = (int)rdr["CargoId"];
                        return register;
                    }
                    else
                        return null;
                }
            }
        }
        public List<Funcionarios> ListarFuncionarios(string? nomeFuncionario)
        {
            string comandoSql = @"SELECT FuncionarioId, 
                                     NomeDoFuncionario, 
                                     Cpf, 
                                     NascimentoFuncionario, 
                                     CelularFuncionario,
                                     EmailFuncionario,
                                     DataDeAdmissao,
                                     CargoId FROM Funcionarios";

            if (!string.IsNullOrWhiteSpace(nomeFuncionario))
                comandoSql += " WHERE nomeDoFuncionario LIKE @NomeDoFuncionario";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                if (!string.IsNullOrWhiteSpace(nomeFuncionario))
                    cmd.Parameters.AddWithValue("@NomeDoFuncionario", "%" + nomeFuncionario + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var registers = new List<Funcionarios>();
                    while (rdr.Read())
                    {
                        var register = new Funcionarios();
                        register.funcionarioId = (int)rdr["FuncionarioId"];
                        register.nomeFuncionario = Convert.ToString(rdr["NomeDoFuncionario"]);
                        register.cpf = Convert.ToString(rdr["Cpf"]);
                        register.telefoneFuncionario = Convert.ToString(rdr["CelularFuncionario"]);
                        register.emailFuncionario = Convert.ToString(rdr["EmailFuncionario"]);
                        register.dataNascimento = Convert.ToDateTime(rdr["NascimentoFuncionario"]);
                        register.dataAdmissao = Convert.ToDateTime(rdr["DataDeAdmissao"]);
                        register.cargoId = (int)rdr["CargoId"];

                        registers.Add(register);
                    }
                    return registers;
                }

            }
        }
            public void Deletar(int FuncionarioId)
        {
            string comandoSql = @"DELETE FROM Funcionarios WHERE FuncionarioId = @FuncionarioId;";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", FuncionarioId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para este Identificador {FuncionarioId}");
                else
                    cmd.ExecuteNonQuery();
            }
        }
    }
}



