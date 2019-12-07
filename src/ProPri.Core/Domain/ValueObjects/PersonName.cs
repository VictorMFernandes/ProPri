using ProPri.Core.Constants;
using ProPri.Core.Validation;

namespace ProPri.Core.Domain.ValueObjects
{
    public sealed class PersonName : ValueObject
    {
        #region Properties

        public string FirstName { get; }
        public string Surname { get; }

        #endregion

        #region Constructors

        public PersonName(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;

            Validate();
        }

        #endregion

        #region ValueObject Methods

        public override string ToString()
        {
            return $"{FirstName} {Surname}";
        }

        protected override void Validate()
        {
            Validator.MaximumLength(FirstName, ConstSizes.PersonFirstNameMax, nameof(FirstName));
            Validator.MinimumLength(FirstName, ConstSizes.PersonFirstNameMin, nameof(FirstName));

            Validator.MaximumLength(Surname, ConstSizes.PersonSurnameMax, nameof(Surname));
            Validator.MinimumLength(Surname, ConstSizes.PersonSurnameMin, nameof(Surname));
        }

        #endregion
    }
}