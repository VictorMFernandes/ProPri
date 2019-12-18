using Rise.Core.Constants;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rise.WebApp.Mvc.Views.Auth.ViewModels
{
    public class NewPasswordViewModel
    {
        public Guid UserId { get; set; }

        [DisplayName("Current Password")]
        [Required]
        [StringLength(ConstSizes.UserPasswordMax, MinimumLength = ConstSizes.UserPasswordMin)]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(ConstSizes.UserPasswordMax, MinimumLength = ConstSizes.UserPasswordMin)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}