﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ProPri.Core.Constants;

namespace ProPri.WebApp.Mvc.Views.Auth.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("E-mail")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(ConstSizes.UserPasswordMax, MinimumLength = ConstSizes.UserPasswordMin)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
