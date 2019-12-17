using System;

namespace ProPri.Users.Domain.Dtos
{
    public class UserIndexDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }

        public UserIndexDto(Guid id, string name, string role, bool active)
        {
            Id = id;
            Name = name;
            Role = role;
            Active = active;
        }
    }
}