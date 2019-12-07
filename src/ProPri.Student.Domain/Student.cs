using ProPri.Core.Domain;
using ProPri.Core.Domain.ValueObjects;

namespace ProPri.Students.Domain
{

    public sealed class Student : Entity
    {
        #region Properties

        public PersonName Name { get; private set; }
        public Email Email { get; private set; }
        public Credentials Credentials { get; private set; }
        public Image Image { get; private set; }
        public Address Address { get; private set; }

        #endregion

        #region Constructors

        private Student()
        {
            
        }

        public Student(PersonName name, Email email, Credentials credentials)
        {
            Name = name;
            Email = email;
            Credentials = credentials;

            Validate();
        }

        #endregion

        #region Entity Methods

        protected override void InitializeCollections()
        {
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        protected override void Validate()
        {
        }

        #endregion
    }
}