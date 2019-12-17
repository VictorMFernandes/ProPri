using Microsoft.AspNetCore.Identity;
using ProPri.Core.Communication.Messages;
using ProPri.Core.Constants;
using ProPri.Core.Domain;
using ProPri.Core.Extensions;
using ProPri.Core.Helpers;
using ProPri.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProPri.Users.Domain
{
    public sealed class User : IdentityUser<Guid>, IAggregateRoot, IEventContainer
    {
        #region Properties

        private string _name;
        public string Name
        {
            get => _name;
            private set
            {
                _name = value;
                NormalizedName = _name.ToNeutral();
            }
        }
        public string NormalizedName { get; private set; }
        public DateTime RegistrationDate { get; }
        public DateTime LastActiveDate { get; private set; }
        public bool Active { get; set; }
        public DateTime? Birthday { get; private set; }
        public bool IsAdministrator { get; private set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public Role Role => UserRoles.Single().Role;

        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        #endregion

        #region Constructors

        private User() { }

        private User(string name, string email, bool active, DateTime? birthday, Role role)
        {
            InitializeCollections();

            Name = name;
            Email = email;
            UserName = Email;
            Active = active;
            Birthday = birthday;
            UserRoles.Add(new UserRole(role));
            RegistrationDate = DateTime.Now;
            IsAdministrator = false;

            Validate();
        }

        // TODO remover esse construtor e criar uma factory para dar o seed quando for para produção
        public User(string name, string email, bool isAdministrator)
        {
            InitializeCollections();

            Name = name;
            Email = email;
            UserName = email;
            RegistrationDate = DateTime.Now;
            Active = true;
            EmailConfirmed = true;
            IsAdministrator = isAdministrator;

            Validate();
        }

        #endregion

        #region Entity Methods

        private void InitializeCollections()
        {
            UserRoles = new List<UserRole>();
            _notifications = new List<Event>();
        }

        public override string ToString()
        {
            return Name;
        }

        private void Validate()
        {
            Validator.IsNotNull(RegistrationDate, nameof(RegistrationDate));
        }

        #endregion

        #region EventContainer Methods

        public void AddEvent(Event eventItem)
        {
            _notifications ??= new List<Event>();
            _notifications.Add(eventItem);
        }

        public void RemoveEvent(Event eventItem)
        {
            _notifications?.Remove(eventItem);
        }

        public void CleanEvents()
        {
            _notifications?.Clear();
        }

        #endregion

        public User CreateUser(string name, string email, bool active, DateTime? birthday, Role role)
        {
            if (role.Name == ConstData.RoleManager && !HasRole(ConstData.RoleManager))
                return null;

            var user = new User(name, email, active, birthday, role);

            user.Validate();

            return user;
        }

        public bool UpdateUser(User user, string name, string email, DateTime? birthday, bool active, Role role)
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
            return IsAdministrator || UserRoles.Single().Role.Name == role;
        }

        public bool HasClaim(string claim)
        {
            return IsAdministrator || UserRoles.Single().Role.RoleClaims.Any(rc => rc.ClaimValue == claim);
        }

        public string GenerateTempPassword()
        {
            return StringHelper.RandomPassword(ConstSizes.UserPasswordMin);
        }
    }
}