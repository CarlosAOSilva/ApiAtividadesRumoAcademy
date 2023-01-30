namespace ApiRh.Domain.Models
{
    public class Funcionarios
    {
        public int funcionarioId { get; set; }
        public string nomeFuncionario { get; set; }
        public string cpf { get; set; }

        public DateTime dataNascimento { get; set; }

        public DateTime dataAdmissao { get; set; }

        public string telefoneFuncionario { get; set; }

        public string emailFuncionario { get; set; }

        public int cargoId { get; set; }

    }   
            
}
