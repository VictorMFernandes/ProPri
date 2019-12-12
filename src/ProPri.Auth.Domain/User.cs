using Microsoft.AspNetCore.Identity;
using ProPri.Core.Constants;
using ProPri.Core.Domain;
using ProPri.Core.Domain.ValueObjects;
using ProPri.Core.Helpers;
using ProPri.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

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

        private User() { }

        private User(PersonName name, string email, DateTime? birthday, Role role)
        {
            InitializeCollections();

            Name = name;
            Email = email;
            UserName = Email;
            Birthday = birthday;
            UserRoles.Add(new UserRole(role));
            RegistrationDate = DateTime.Now;

            Validate();
        }

        // TODO remover esse construtor e criar uma factory para dar o seed quando for para produção
        public User(PersonName name, string email)
        {
            InitializeCollections();

            Name = name;
            Email = email;
            UserName = email;
            RegistrationDate = DateTime.Now;
            Active = true;
            EmailConfirmed = true;

            Validate();
        }

        #endregion

        #region Entity Methods

        private void InitializeCollections()
        {
            UserRoles = new List<UserRole>();
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

        public User CreateUser(PersonName name, string email, DateTime? birthday, Role role)
        {
            if (role.Name == ConstData.RoleManager && !HasRole(ConstData.RoleManager))
                return null;

            var user = new User(name, email, birthday, role);

            user.Validate();

            return user;
        }

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

        public string GenerateTempPassword()
        {
            return StringHelper.RandomString(ConstSizes.UserPasswordMin);
        }
    }
}