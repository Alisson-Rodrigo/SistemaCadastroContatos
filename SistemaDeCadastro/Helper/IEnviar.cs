namespace SistemaDeCadastro.Helper
{
    public interface IEnviar
    {
        public bool EnviarEmail(string email, string assunto, string mensagem);
        
    }
}