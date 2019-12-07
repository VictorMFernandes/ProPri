using ProPri.Core.Constants;
using ProPri.Core.Validation;

namespace ProPri.Core.Domain.ValueObjects
{
    public sealed class Address : ValueObject
    {
        #region Properties

        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        #endregion

        #region Constructors

        public Address()
        {
            Validate();
        }

        #endregion

        #region ValueObject Methods

        public override string ToString()
        {
            return $"{Street} {District} {Number}";
        }

        protected override void Validate()
        {
            Validator.IsNotNullOrEmpty(ZipCode, nameof(ZipCode));
            Validator.MaximumLength(ZipCode, ConstSizes.AddressZipCodeMax, nameof(ZipCode));

            Validator.IsNotNullOrEmpty(Street, nameof(Street));
            Validator.MaximumLength(Street, ConstSizes.AddressStreetMax, nameof(Street));

            Validator.IsNotNullOrEmpty(Number, nameof(Number));
            Validator.MaximumLength(Number, ConstSizes.AddressNumberMax, nameof(Number));

            Validator.IsNotNullOrEmpty(Complement, nameof(Complement));
            Validator.MaximumLength(Complement, ConstSizes.AddressComplementMax, nameof(Complement));

            Validator.IsNotNullOrEmpty(District, nameof(District));
            Validator.MaximumLength(District, ConstSizes.AddressDistrictMax, nameof(District));

            Validator.IsNotNullOrEmpty(City, nameof(City));
            Validator.MaximumLength(City, ConstSizes.AddressCityMax, nameof(City));

            Validator.IsNotNullOrEmpty(State, nameof(State));
            Validator.MaximumLength(State, ConstSizes.AddressStateMax, nameof(State));
        }

        #endregion
    }
}