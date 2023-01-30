namespace ApiRh.Domain.Models
{

    public enum EnumCargoUsuario
    {
        Gerente = 1,
        Rh = 2,
        Colaborador

    }
    public class Usuario
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public EnumCargoUsuario CargoUsuario { get; set; }
    }
}


