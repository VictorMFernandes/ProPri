﻿using System;

namespace ProPri.Users.Domain.Dtos
{
    public class UserFormDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime LastActiveDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Guid RoleId { get; set; }
    }
}