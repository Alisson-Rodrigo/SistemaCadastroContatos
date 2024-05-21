using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace SistemaDeCadastro.Helper
{
    public class Enviar : IEnviar
    {

        private readonly IConfiguration _configuration;

        public Enviar(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool EnviarEmail(string email, string assunto, string mensagem) {
            try
            {
                string host = _configuration.GetValue<string>("SMTP:Host");
                int porta = _configuration.GetValue<int>("SMTP:Porta");
                string usuario = _configuration.GetValue<string>("SMTP:UserName");
                string senha = _configuration.GetValue<string>("SMTP:Senha");
                string displayName = _configuration.GetValue<string>("SMTP:DisplayName");

                MailMessage mail = new MailMessage(){
                    From = new MailAddress(usuario, displayName),
                    Subject = assunto,
                    Body = mensagem,
                    IsBodyHtml = true
                };
                mail.To.Add(email);
                mail.Priority = MailPriority.High;

                using(SmtpClient smtp = new SmtpClient(host, porta)){
                    smtp.Credentials = new System.Net.NetworkCredential(usuario, senha);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    return true;
                }

            }
            catch (System.Exception)
            {
                return false;
            }

        }
        
    }
}