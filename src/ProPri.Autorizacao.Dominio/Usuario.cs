using Microsoft.AspNetCore.Identity;
using ProPri.Comum.Dominio;

namespace ProPri.Autorizacao.Dominio
{
    public class Usuario : IdentityUser, IRaizAgregacao
    {
        public string Nome { get; private set; }

        public Usuario(string nome, string email, string telefone)
        {
            Nome = nome;
            Email = email;
            UserName = email;
            PhoneNumber = telefone;
        }
    }
}