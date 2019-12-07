﻿using ProPri.Users.Data;

namespace ProPri.Core.WebApp.Data
{
    public class Seeder
    {
        private readonly UsersSeeder _authSeeder;

        public Seeder(UsersSeeder authSeeder)
        {
            _authSeeder = authSeeder;
        }

        public void Seed()
        {
            _authSeeder.Seed();
        }
    }
}