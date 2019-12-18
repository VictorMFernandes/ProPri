using System;

namespace Rise.Core.Helpers
{
    public static class StringHelper
    {
        public static string RandomPassword(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";

            var stringChars = new char[length];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
            {
                if (i % 2 == 0)
                    stringChars[i] = chars[random.Next(chars.Length)];
                else
                    stringChars[i] = numbers[random.Next(numbers.Length)];
            }

            return new string(stringChars);
        }
    }
}