using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UserModel usuario);
        void RemoverSessaoDoUsuario();
        UserModel BuscarSessaoDoUsuario();
    }
}