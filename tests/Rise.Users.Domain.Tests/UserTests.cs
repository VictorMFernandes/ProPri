using Rise.Core.Constants;
using Rise.Core.Extensions;
using Xunit;

namespace Rise.Users.Domain.Tests
{
    public class UserTests
    {
        #region Constructors

        [Fact(DisplayName = "Construct Must Initialize All Collections")]
        [Trait("Category", "User Tests")]
        public void OnConstruct_InitializeCollections()
        {
            // Act
            var roleAdm = new Role(ConstData.RoleAdministrator);
            var user = User.CreateAdministrator("User SurName", "email@email.com", roleAdm);

            // Assert
            foreach (var prop in user.GetCollections())
            {
                Assert.NotNull(prop.GetValue(user));
            }
        }

        #endregion

        [Fact(DisplayName = "Create User Return User")]
        [Trait("Category", "User Tests")]
        public void CreateUser_ReturnUser()
        {
            // Arrange
            var roleAdm = new Role(ConstData.RoleAdministrator);
            var performingUser = User.CreateAdministrator("User SurName", "email@email.com", roleAdm);
            var role = new Role(ConstData.RoleFd);

            // Act
            var createdUser = performingUser.CreateUser("Newuser Surname", "user@email.com", true, null, role);

            // Assert
            Assert.NotNull(createdUser);
        }

        [Fact(DisplayName = "Create User Without Role Return Null")]
        [Trait("Category", "User Tests")]
        public void CreateUser_RoleNull_ReturnNull()
        {
            // Arrange
            var roleAdm = new Role(ConstData.RoleAdministrator);
            var performingUser = User.CreateAdministrator("User SurName", "email@email.com", roleAdm);

            // Act
            var createdUser = performingUser.CreateUser("Newuser Surname", "user@email.com", true, null, null);

            // Assert
            Assert.Null(createdUser);
        }
    }
}