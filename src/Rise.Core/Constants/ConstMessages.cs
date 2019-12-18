namespace Rise.Core.Constants
{
    public static class ConstMessages
    {
        public static string ErrorMaxLength(string propertyName, int maxLength)
        {
            return $"Maximum length for {propertyName} is {maxLength}";
        }

        public static string ErrorMinLength(string propertyName, int minLength)
        {
            return $"Minimum length for {propertyName} is {minLength}";
        }

        public static string ErrorNull(string propertyName)
        {
            return $"{propertyName} can't be null";
        }

        public static string ErrorNullOrEmpty(string propertyName)
        {
            return $"{propertyName} can't be neither empty nor null";
        }

        public static string ErrorInvalid(string propertyName)
        {
            return $"{propertyName} is invalid";
        }

        public static string ErrorNotEqual(string propertyNameA, string propertyNameB)
        {
            return $"{propertyNameA} and {propertyNameB} must be equal";
        }
    }
}