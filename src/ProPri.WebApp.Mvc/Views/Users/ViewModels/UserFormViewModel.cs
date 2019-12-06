using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProPri.WebApp.Mvc.Views.Users.ViewModels
{
    public class UserFormViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Role")]
        public Guid RoleId { get; set; }

        public IEnumerable<RoleIndexViewModel> Roles { get; set; }
    }
}