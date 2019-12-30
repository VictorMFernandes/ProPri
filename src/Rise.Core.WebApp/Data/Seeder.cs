using Rise.Users.Data.Seeding;
using System.Threading.Tasks;

namespace Rise.Core.WebApp.Data
{
    public class Seeder
    {
        private readonly UsersSeeder _userSeeder;

        public Seeder(UsersSeeder userSeeder)
        {
            _userSeeder = userSeeder;
        }

        public async Task Seed()
        {
            await _userSeeder.Seed();
        }
    }
}