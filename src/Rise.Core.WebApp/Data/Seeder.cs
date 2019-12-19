using Rise.Users.Data;

namespace Rise.Core.WebApp.Data
{
    public class Seeder
    {
        private readonly UsersSeeder _userSeeder;

        public Seeder(UsersSeeder userSeeder)
        {
            _userSeeder = userSeeder;
        }

        public void Seed()
        {
            _userSeeder.Seed();
        }
    }
}