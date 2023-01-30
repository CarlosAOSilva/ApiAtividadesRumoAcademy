using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ApiRh.Domain.Models;

namespace ApiRh.Repositories
{
    public class PontosRepositories : Context
    {
        public PontosRepositories(IConfiguration configuration) : base(configuration)
        {
        }

        public void InserirCargos(Pontos register)
        {
            string comandoSql = @"INSERT INTO Ponto
                                    (DataHorarioPonto, Justificativa, FuncionarioId) 
                                        VALUES
                                    (@DataHorarioPonto, @Justificativa, @FuncionarioId);";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@PontoId", register.pontoId);
                cmd.Parameters.AddWithValue("@DataHorarioPonto", register.dataHorarioPonto);
                cmd.Parameters.AddWithValue("@Justificativa", register.justificativa);
                cmd.Parameters.AddWithValue("@FuncionarioId", register.funcionarioId);

                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(Pontos register)
        {
            string comandoSql = @"UPDATE Ponto
                                    SET
                                     DataHorarioPonto = @DataHorarioPonto, Justificativa = @Justificativa, FuncionarioId = @FuncionarioId
                                    WHERE PontoId = @PontoId;";


            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@PontoId", register.pontoId);
                cmd.Parameters.AddWithValue("@DataHorarioPonto", register.dataHorarioPonto);
                cmd.Parameters.AddWithValue("@Justificativa", register.justificativa);
                cmd.Parameters.AddWithValue("@FuncionarioId", register.funcionarioId);

                cmd.ExecuteNonQuery();

                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para este Identificador {register.pontoId}");
            }
        }
        public bool SeExiste(int pontoId)
        {
            string comandoSql = @"SELECT COUNT (PontoId) FROM Ponto WHERE PontoId = @PontoId";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@PontoId", pontoId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public Pontos Obter(int pontoId)
        {
            string comandoSql = @"SELECT PontoId, DataHorarioPonto, Justificativa, FuncionarioId
                                     FROM Ponto WHERE PontoId = @PontoId";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@PontoId", pontoId);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        var register = new Pontos();

                        register.pontoId = Convert.ToInt32(rdr["PontoId"]);
                        register.dataHorarioPonto = (DateTime)rdr["DataHorarioPonto"];
                        register.justificativa = Convert.ToString(rdr["Justificativa"]);
                        register.funcionarioId = (int)rdr["FuncionarioId"];

                        return register;
                    }
                    else
                        return null;
                }
            }
        }
        public List<Pontos> ListarPontos(int? pontoId)
        {
            string comandoSql = @"Select PontoId, DataHorarioPonto, Justificativa, FuncionarioId From Ponto";

            if (pontoId != null)
                comandoSql += " WHERE pontoId LIKE @pontoId";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                if (pontoId != null)
                    cmd.Parameters.AddWithValue("@PontoId", "%" + pontoId + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var registers = new List<Pontos>();
                    while (rdr.Read())
                    {
                        var register = new Pontos();
                        register.pontoId = Convert.ToInt32(rdr["PontoId"]);
                        register.dataHorarioPonto = (DateTime)rdr["DataHorarioPonto"];
                        register.justificativa = Convert.ToString(rdr["Justificativa"]);
                        register.funcionarioId = (int)rdr["FuncionarioId"];

                        registers.Add(register);
                    }
                    return registers;
                }
            }
        }
        public void Deletar(int pontoId)
        {
            string comandoSql = @"DELETE FROM Ponto WHERE PontoId = @PontoId;";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@PontoId", pontoId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para este Identificador {pontoId}");
                else
                    cmd.ExecuteNonQuery();
            }
        }
    }
}
