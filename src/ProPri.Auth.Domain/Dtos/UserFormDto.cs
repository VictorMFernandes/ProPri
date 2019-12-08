using System;

namespace ProPri.Users.Domain.Dtos
{
    public class UserFormDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
    }
}