namespace ApiRh.Domain.Models
{
    public class Pontos
    {
        public int pontoId { get; set; }

        public DateTime dataHorarioPonto { get; set; }

        public int funcionarioId { get; set; }

        public string justificativa { get; set; }
    }
}
