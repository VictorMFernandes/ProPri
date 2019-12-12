using Microsoft.AspNetCore.Identity;
using ProPri.Core.Domain;
using ProPri.Core.Domain.ValueObjects;
using ProPri.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using ProPri.Core.Constants;

namespace ProPri.Users.Domain
{
    public sealed class User : IdentityUser<Guid>, IAggregateRoot
    {
        #region Properties

        public PersonName Name { get; private set; }
        public DateTime RegistrationDate { get; }
        public DateTime LastActiveDate { get; private set; }
        public bool Active { get; set; }
        public DateTime? Birthday { get; private set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public Role Role => UserRoles.Single().Role;

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
            Active = true;

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

        public bool UpdateUser(User user, PersonName name, string email, DateTime? birthday, bool active, Role role)
        {
            user.Name = name;
            user.Email = email;
            user.Birthday = birthday;

            if (!UpdateUserActive(user, active))
                return false;

            if (!UpdateUserRole(user, role))
                return false;

            Validate();

            return true;
        }

        private bool UpdateUserActive(User user, bool active)
        {
            if (user.HasRole(ConstData.RoleManager) && !HasRole(ConstData.RoleManager))
                return false;

            user.Active = active;
            return true;
        }

        private bool UpdateUserRole(User user, Role role)
        {
            if (user.HasRole(ConstData.RoleManager) && !HasRole(ConstData.RoleManager))
                return false;

            if (role.Name == ConstData.RoleManager && !HasRole(ConstData.RoleManager))
                return false;

            if (role.Name == user.Role.Name)
                return true;

            user.UserRoles.Clear();
            user.UserRoles.Add(new UserRole(role));

            return true;
        }

        public void UpdateLastActiveDate()
        {
            LastActiveDate = DateTime.Now;
        }

        public bool HasRole(string role)
        {
            return UserRoles.Single().Role.Name == role;
        }

        public bool HasClaim(string claim)
        {
            return UserRoles.Single().Role.RoleClaims.Any(rc => rc.ClaimValue == claim);
        }
    }
}