using Microsoft.AspNetCore.Identity;
using ProPri.Core.Domain;
using ProPri.Core.Domain.ValueObjects;
using System;

namespace ProPri.Auth.Domain
{
    public sealed class User : IdentityUser, IAggregateRoot
    {
        #region Properties

        public PersonName Name { get; private set; }
        public DateTime RegistrationDate { get; }
        
        #endregion

        #region Constructors

        private User()
        {

        }

        public User(PersonName name, string email, string phone)
        {
            Name = name;
            Email = email;
            UserName = email;
            PhoneNumber = phone;
            RegistrationDate = DateTime.Now;

            Validate();
        }

        #endregion

        #region Entity Methods

        private void InitializeCollections()
        {
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        private void Validate()
        {
        }

        #endregion
    }
}