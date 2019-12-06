using Microsoft.AspNetCore.Identity;
using ProPri.Core.Domain;

namespace ProPri.Auth.Domain
{
    public class User : IdentityUser, IAggregateRoot
    {
        public string Nome { get; private set; }

        public User(string nome, string email, string telefone)
        {
            Nome = nome;
            Email = email;
            UserName = email;
            PhoneNumber = telefone;
        }
    }
}