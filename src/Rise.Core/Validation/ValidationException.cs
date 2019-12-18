using System;

namespace Rise.Core.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}