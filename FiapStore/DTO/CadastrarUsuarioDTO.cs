using FiapStore.Enums;

namespace FiapStore.DTO
{
    public class CadastrarUsuarioDTO
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; internal set; }
        public string Senha { get; internal set; }
        public TipoPermissao Permissao { get; internal set; }
    }
}
