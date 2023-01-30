using System.Data.SqlClient;
using ApiRh.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;

namespace ApiRh.Repositories
{
    public class LiderancasRepositories : Context
    {
        public LiderancasRepositories(IConfiguration configuration) : base(configuration)
        {
        }

        public void InserirLiderancas(Liderancas register)
        {
            string comandoSql = @"INSERT INTO Liderancas
                                    (FuncionarioId,
                                    DescricaoEquipe) 
                                        VALUES
                                    (@FuncionarioId,
                                    @DescricaoEquipe);";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", register.liderancaId);
                cmd.Parameters.AddWithValue("@FuncionarioId", register.funcionarioId);
                cmd.Parameters.AddWithValue("@DescricaoEquipe", register.descricaoEquipe);

                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(Liderancas register)
        {
            string comandoSql = @"UPDATE Liderancas
                                    SET FuncionarioId = @FuncionarioId,
                                     DescricaoEquipe = @DescricaoEquipe
                                    WHERE LiderancaId = @LiderancaId;";


            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", register.liderancaId);
                cmd.Parameters.AddWithValue("@FuncionarioId", register.funcionarioId);
                cmd.Parameters.AddWithValue("@DescricaoEquipe", register.descricaoEquipe);

                cmd.ExecuteNonQuery();

                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para este Identificador {register.liderancaId}");
            }
        }
        public bool SeExiste(int liderancasId)
        {
            string comandoSql = @"SELECT COUNT (LiderancaId) FROM Liderancas WHERE LiderancaId = @LiderancaId";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", liderancasId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public Liderancas Obter(int liderancasId)
        {
            string comandoSql = @"SELECT LiderancaId, FuncionarioId, DescricaoEquipe 
                                     FROM Liderancas WHERE LiderancaId = @LiderancaId";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", liderancasId);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        var register = new Liderancas();

                        register.liderancaId = (int)rdr["LiderancaId"];
                        register.funcionarioId = (int)rdr["FuncionarioId"];
                        register.descricaoEquipe = Convert.ToString(rdr["DescricaoEquipe"]);

                        return register;
                    }
                    else
                        return null;
                }
            }
        }
        public List<Liderancas> ListarLiderancas(string? descricaoEquipe)
        {
            string comandoSql = @"Select LiderancaId, FuncionarioId, DescricaoEquipe From Liderancas";

            if (!string.IsNullOrWhiteSpace(descricaoEquipe))
                comandoSql += " WHERE descricaoEquipe LIKE @descricaoEquipe";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                if (!string.IsNullOrWhiteSpace(descricaoEquipe))
                    cmd.Parameters.AddWithValue("@DescricaoEquipe", "%" + descricaoEquipe + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var registers = new List<Liderancas>();
                    while (rdr.Read())
                    {
                        var register = new Liderancas();
                        register.liderancaId = (int)rdr["LiderancaId"];
                        register.funcionarioId = (int)rdr["FuncionarioId"];
                        register.descricaoEquipe = Convert.ToString(rdr["DescricaoEquipe"]);

                        registers.Add(register);
                    }
                    return registers;
                }
            }
        }
        public void Deletar(int liderancasId)
        {
            string comandoSql = @"DELETE FROM Liderancas WHERE LiderancaId = @LiderancaId"";";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", liderancasId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para este Identificador {liderancasId}");
                else
                    cmd.ExecuteNonQuery();
            }
        }
    }
}



