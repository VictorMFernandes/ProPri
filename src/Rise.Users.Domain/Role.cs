using Microsoft.AspNetCore.Identity;
using Rise.Core.Constants;
using System;
using System.Collections.Generic;

namespace Rise.Users.Domain
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RoleClaim> RoleClaims { get; set; }

        public bool IsAdmin => Name == ConstData.RoleAdministrator;
        public bool IsManager => Name == ConstData.RoleManager || IsAdmin;
        public bool IsPed => Name == ConstData.RolePed || IsManager;
        public bool IsFd => Name == ConstData.RoleFd || IsPed;

        #region Constructors

        private Role() { }

        public Role(string name)
        {
            InitializeCollections();

            Name = name;

            Validate();
        }

        #endregion

        #region Entity Methods

        private void InitializeCollections()
        {
            UserRoles = new List<UserRole>();
            RoleClaims = new List<RoleClaim>();
        }

        public override string ToString()
        {
            return Name;
        }

        private void Validate() { }

        #endregion

        public bool HasAuthorization(string roleName)
        {
            return roleName switch
            {
                ConstData.RoleAdministrator => IsAdmin,
                ConstData.RoleManager => IsManager,
                ConstData.RolePed => IsPed,
                ConstData.RoleFd => IsFd,
                _ => false
            };
        }

        public void AddClaim(string claimType, string claimValue)
        {
            RoleClaims.Add(new RoleClaim(claimType, claimValue));
        }
    }
}