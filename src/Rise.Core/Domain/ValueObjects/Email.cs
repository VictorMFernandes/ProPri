using Rise.Core.Constants;
using Rise.Core.Validation;

namespace Rise.Core.Domain.ValueObjects
{

    public sealed class Email : ValueObject
    {
        #region Properties

        public string Address { get; }

        #endregion

        #region Constructors

        public Email(string address)
        {
            Address = address;

            Validate();
        }

        #endregion

        #region ValueObject Methods

        public override string ToString()
        {
            return Address;
        }

        protected override void Validate()
        {
            Validator.MaximumLength(Address, ConstSizes.EmailAddressMax, $"{nameof(Email)} {nameof(Address)}");
            Validator.IsEmail(Address, $"{nameof(Email)} {nameof(Address)}");
        }

        #endregion
    }
}