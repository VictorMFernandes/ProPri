using Microsoft.IdentityModel.Tokens;
using Rise.Core.Constants;
using Rise.Core.Domain;
using Rise.Core.Validation;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rise.Students.Domain
{

    public sealed class Credentials : ValueObject
    {
        #region Properties

        public string Login { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; private set; }

        #endregion

        #region Constructors

        public Credentials(string login, string password)
        {
            ValidatePassword(password);

            Login = login;
            Password = EncryptPassword(password);

            Validate();
        }

        #endregion

        #region ValueObject Methods

        public override string ToString()
        {
            return Login;
        }

        protected override void Validate()
        {
            Validator.MaximumLength(Login, ConstSizes.StudentLoginMax, nameof(Login));
            Validator.MinimumLength(Login, ConstSizes.StudentLoginMin, nameof(Login));
        }

        #endregion

        private void ValidatePassword(string password)
        {
            Validator.MaximumLength(password, ConstSizes.StudentPasswordMax, nameof(Password));
            Validator.MinimumLength(password, ConstSizes.StudentPasswordMin, nameof(Password));
        }

        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            var passwordWithHash = (password + "|f168wa7-j8x44gj-6v18-qh89t7-n1b315fq75w23-11");
            using var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(passwordWithHash));
            var sbString = new StringBuilder();

            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }

        public static string GenerateToken(string userId, string login, string secretToken, double prescriptionDays)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, login),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretToken));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime
                    .Now
                    .AddDays(prescriptionDays),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}