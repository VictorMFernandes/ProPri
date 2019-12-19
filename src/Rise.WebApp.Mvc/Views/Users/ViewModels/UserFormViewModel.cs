using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Rise.WebApp.Mvc.Views.Users.ViewModels
{
    public class UserFormViewModel
    {
        public Guid LoggedUserId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastActiveDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Active { get; set; }
        public DateTime? Birthday { get; set; }
        public IFormFile ImageUpload { get; set; }
        public string Image { get; set; }

        [DisplayName("Role")]
        public Guid RoleId { get; set; }

        public IEnumerable<RoleIndexViewModel> Roles { get; set; }
    }
}