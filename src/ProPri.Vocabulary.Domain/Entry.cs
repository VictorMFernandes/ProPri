using System;
using ProPri.Core.Domain;
using ProPri.Core.Domain.ValueObjects;

namespace ProPri.Vocabulary.Domain
{

    public sealed class Entry : Entity, IAggregateRoot
    {
        #region Properties

        public string English { get; private set; }
        public string Portuguese { get; private set; }
        public Image Image { get; private set; }
        public Guid ClassificationId { get; private set; }

        public Classification Classification { get; private set; }

        #endregion

        #region Constructors

        public Entry()
        {
            Validate();
        }

        #endregion

        #region Entity Methods

        protected override void InitializeCollections()
        {
        }

        public override string ToString()
        {
            return English;
        }

        protected override void Validate()
        {
        }

        #endregion
    }
}