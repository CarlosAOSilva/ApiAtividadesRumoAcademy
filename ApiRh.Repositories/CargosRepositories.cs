using System.Data.SqlClient;
using ApiRh.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace ApiRh.Repositories
{
    public class CargosRepositories : Context
    {
        public CargosRepositories(IConfiguration configuration) : base(configuration)
        {
        }

        public void InserirCargos(Cargos register)
        {
            string comandoSql = @"INSERT INTO Cargos
                                    (Descricao) 
                                        VALUES
                                    (@Descricao);";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@CargoId", register.cargoId);
                cmd.Parameters.AddWithValue("@Descricao", register.nomeCargo);

                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(Cargos register)
        {
            string comandoSql = @"UPDATE Cargos
                                    SET
                                     Descricao = @Descricao 
                                    WHERE CargoId = @CargoId;";


            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@CargoId", register.cargoId);
                cmd.Parameters.AddWithValue("@Descricao", register.nomeCargo);

                cmd.ExecuteNonQuery();

                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para este Identificador {register.cargoId}");
            }
        }
        public bool SeExiste(int idCargo)
        {
            string comandoSql = @"SELECT COUNT (CargoId) FROM Cargos WHERE CargoId = @CargoId";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@CargoId", idCargo);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public Cargos Obter(int idCargo)
        {
            string comandoSql = @"SELECT CargoId, Descricao
                                     FROM Cargos WHERE CargoId = @CargoId";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@CargoId", idCargo);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        var register = new Cargos();

                        register.cargoId = (int)rdr["CargoId"];
                        register.nomeCargo = Convert.ToString(rdr["Descricao"]);

                        return register;
                    }
                    else
                        return null;
                }
            }
        }
        public List<Cargos> ListarCargos(string? descricao)
        {
            string comandoSql = @"Select CargoId, Descricao From Cargos";

            if (!string.IsNullOrWhiteSpace(descricao))
                comandoSql += " WHERE descricao LIKE @descricao";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                if (!string.IsNullOrWhiteSpace(descricao))
                    cmd.Parameters.AddWithValue("@Descricao", "%" + descricao + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var registers = new List<Cargos>();
                    while (rdr.Read())
                    {
                        var register = new Cargos();
                        register.cargoId = (int)rdr["CargoId"];
                        register.nomeCargo = Convert.ToString(rdr["Descricao"]);

                        registers.Add(register);
                    }
                    return registers;
                }
            }
        }
        public void Deletar(int idCargo)
        {
            string comandoSql = @"DELETE FROM Cargos WHERE CargoId = @CargoId;";

            using (var cmd = new SqlCommand(comandoSql, _conexao))
            {
                cmd.Parameters.AddWithValue("@CargoId", idCargo);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para este Identificador {idCargo}");
                else
                    cmd.ExecuteNonQuery();
            }
        }
    }
}



