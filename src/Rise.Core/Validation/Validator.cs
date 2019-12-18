using System.Text.RegularExpressions;
using Rise.Core.Constants;

namespace Rise.Core.Validation
{
    public static class Validator
    {
        public static void MaximumLength(string property, int maxLength, string propertyName)
        {
            if (property == null)
                throw new ValidationException(ConstMessages.ErrorNull(propertyName));

            if (property.Length > maxLength)
                throw new ValidationException(ConstMessages.ErrorMaxLength(propertyName, maxLength));
        }

        public static void MinimumLength(string property, int minLength, string propertyName)
        {
            if (property == null)
                throw new ValidationException(ConstMessages.ErrorNull(propertyName));

            if (property.Length < minLength)
                throw new ValidationException(ConstMessages.ErrorMinLength(propertyName, minLength));
        }

        public static void IsEmail(string property, string propertyName)
        {
            if (property == null)
                throw new ValidationException(ConstMessages.ErrorNull(propertyName));
            const string emailPattern =
                "^(http:\\/\\/www\\.|https:\\/\\/www\\.|http:\\/\\/|https:\\/\\/)[a-z0-9]+([\\-\\.]{1}[a-z0-9]+)*\\.[a-z]{2,5}(:[0-9]{1,5})?(\\/.*)?$";

            if (!Regex.IsMatch(property, emailPattern))
                throw new ValidationException(ConstMessages.ErrorInvalid(propertyName));
        }

        public static void AreEqual(string propertyA, string propertyB, string propertyNameA, string propertyNameB)
        {
            if (propertyA != propertyB)
                throw new ValidationException(ConstMessages.ErrorNotEqual(propertyNameA, propertyNameB));
        }

        public static void IsNotNull(object property, string propertyName)
        {
            if (property == null)
            {
                throw new ValidationException(ConstMessages.ErrorNull(propertyName));
            }
        }

        public static void IsNotNullOrEmpty(string property, string propertyName)
        {
            if (string.IsNullOrEmpty(property))
            {
                throw new ValidationException(ConstMessages.ErrorNullOrEmpty(propertyName));
            }
        }
    }
}