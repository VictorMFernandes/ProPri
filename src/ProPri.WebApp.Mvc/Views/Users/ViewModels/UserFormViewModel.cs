using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProPri.WebApp.Mvc.Views.Users.ViewModels
{
    public class UserFormViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime LastActiveDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Active { get; set; }

        [DisplayName("Role")]
        public Guid RoleId { get; set; }

        public IEnumerable<RoleIndexViewModel> Roles { get; set; }
    }
}