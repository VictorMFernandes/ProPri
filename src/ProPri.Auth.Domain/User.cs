using Microsoft.AspNetCore.Identity;
using ProPri.Core.Domain;
using ProPri.Core.Domain.ValueObjects;
using ProPri.Core.Validation;
using System;
using System.Collections.Generic;

namespace ProPri.Users.Domain
{
    public sealed class User : IdentityUser, IAggregateRoot
    {
        #region Properties

        public PersonName Name { get; private set; }
        public DateTime RegistrationDate { get; }

        public ICollection<IdentityUserRole<string>> UserRoles { get; set; }

        #endregion

        #region Constructors

        private User()
        {

        }

        public User(PersonName name, string email)
        {
            Name = name;
            Email = email;
            UserName = email;
            RegistrationDate = DateTime.Now;

            Validate();
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
            Validator.IsNotNull(RegistrationDate, nameof(RegistrationDate));
        }

        #endregion
    }
}