﻿using Microsoft.AspNetCore.Mvc;
using ProPri.WebApp.Mvc.Views.Auth.ViewModels;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginVm)
        {
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }

        public IActionResult RecoverPassword(LoginViewModel loginVm)
        {
            return View(loginVm);
        }

        [HttpPost]
        public IActionResult RecoverPassword(string email)
        {
            return View();
        }
    }
}