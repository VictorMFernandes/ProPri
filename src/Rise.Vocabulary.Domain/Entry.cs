using System;
using Rise.Core.Domain;
using Rise.Core.Domain.ValueObjects;
using Rise.Core.Validation;

namespace Rise.Vocabulary.Domain
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

        public Entry(string english, string portuguese, Image image, Guid classificationId)
        {
            English = english;
            Portuguese = portuguese;
            Image = image;
            ClassificationId = classificationId;

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
            Validator.IsNotNullOrEmpty(English, nameof(English));
        }

        #endregion
    }
}