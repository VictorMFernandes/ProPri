using Microsoft.AspNetCore.Identity;
using Rise.Core.Communication.Messages;
using Rise.Core.Constants;
using Rise.Core.Domain;
using Rise.Core.Domain.ValueObjects;
using Rise.Core.Extensions;
using Rise.Core.Helpers;
using Rise.Core.Validation;
using Rise.ImageUpload.Api.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rise.Users.Domain
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
        public Image Image { get; private set; }

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
            Validator.NotEqual(RegistrationDate, DateTime.MinValue, nameof(RegistrationDate), "Min DateTime Value");

            Validator.IsNotNull(UserRoles.FirstOrDefault(), nameof(Role));
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

        public void ClearEvents()
        {
            _notifications?.Clear();
        }

        #endregion

        public static User CreateAdministrator(string name, string email, Role role)
        {
            if (!role.IsAdmin) return null;

            var administrator = new User(name, email, true, null, role)
            {
                IsAdministrator = true,
                EmailConfirmed = true
            };

            return administrator;
        }

        public User CreateUser(string name, string email, bool active, DateTime? birthday, Role role)
        {
            if (role == null) return null;

            if (!HasRole(ConstData.RoleFd)) return null;

            if (role.IsManager && !HasRole(ConstData.RoleManager))
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

        public void UpdateUserImage(User user, Image image)
        {
            var oldImage = user.Image;

            user.Image = image;

            if (oldImage != null)
                AddEvent(new ImageUpdatedEvent(user.Id, oldImage.PublicId));
        }

        public void UpdateLastActiveDate()
        {
            LastActiveDate = DateTime.Now;
        }

        public bool HasRole(string role)
        {
            return IsAdministrator || UserRoles.Single().Role.HasAuthorization(role);
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