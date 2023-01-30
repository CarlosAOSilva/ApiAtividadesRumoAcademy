using System.Data.SqlClient;
using ApiRh.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;

namespace ApiRh.Repositories
{
    public class EquipesRepositories : Context
    {
        public EquipesRepositories(IConfiguration configuration) : base(configuration)
        {
        }

        public void InserirCargos(Equipes register)
        {
            string comandoSql = @"INSERT INTO Equipes
                                    (LiderancaId,
                                      FuncionarioId) 
                                        VALUES
                                    (@LiderancaId,
                                     @FuncionarioId);";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@EquipeId", register.equipesId);
                cmd.Parameters.AddWithValue("@LiderancaId", register.liderancaId);
                cmd.Parameters.AddWithValue("@FuncionarioId", register.funcionarioId);

                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(Equipes register)
        {
            string comandoSql = @"UPDATE Equipes
                                    SET
                                     LiderancaId = @LiderancaId, FuncionarioId = @FuncionarioId 
                                    WHERE EquipeId = @EquipeId;";


            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@EquipeId", register.equipesId);
                cmd.Parameters.AddWithValue("@LiderancaId", register.liderancaId);
                cmd.Parameters.AddWithValue("@FuncionarioId", register.funcionarioId);

                cmd.ExecuteNonQuery();

                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para este Identificador {register.equipesId}");
            }
        }
        public bool SeExiste(int equipeId)
        {
            string comandoSql = @"SELECT COUNT (EquipeId) FROM Equipes WHERE EquipeId = @EquipeId";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@EquipeId", equipeId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public Equipes Obter(int equipeId)
        {
            string comandoSql = @"SELECT EquipeId, LiderancaId, FuncionarioId 
                                     FROM Equipes WHERE EquipeId = @EquipeId";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@EquipeId", equipeId);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        var register = new Equipes();

                        register.equipesId = (int)rdr["EquipeId"];
                        register.liderancaId = (int)rdr["LiderancaId"];
                        register.funcionarioId = (int)rdr["FuncionarioId"];

                        return register;
                    }
                    else
                        return null;
                }
            }
        }
        public List<Equipes> ListarEquipes(int equipeId)
        {
            var registers = new List<Equipes>(equipeId);

            string comandoSql = @"Select EquipeId, LiderancaId, FuncionarioId From Equipes";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var register = new Equipes();

                    register.equipesId = (int)rdr["EquipeId"];
                    register.liderancaId = (int)rdr["LiderancaId"];
                    register.funcionarioId = (int)rdr["FuncionarioId"];

                    registers.Add(register);
                }
                return registers;
            }
        }
        public List<Equipes> ListarEquipes(int? equipeId)
        {
            string comandoSql = @"Select EquipeId, LiderancaId, FuncionarioId From Equipes";

            if (equipeId != null)
                comandoSql += " WHERE equipeId LIKE @equipeId";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                if (equipeId != null)
                    cmd.Parameters.AddWithValue("@EquipeId", "%" + equipeId + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var registers = new List<Equipes>();
                    while (rdr.Read())
                    {
                        var register = new Equipes();
                        register.equipesId = (int)rdr["EquipeId"];
                        register.liderancaId = (int)rdr["LiderancaId"];
                        register.funcionarioId = (int)rdr["FuncionarioId"];

                        registers.Add(register);
                    }
                    return registers;
                }
            }
        }
        public void Deletar(int equipeId)
        {
            string comandoSql = @"DELETE FROM Equipes WHERE EquipeId = @EquipeId;";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@EquipeId", equipeId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para este Identificador {equipeId}");
                else
                    cmd.ExecuteNonQuery();
            }
        }
    }
}
