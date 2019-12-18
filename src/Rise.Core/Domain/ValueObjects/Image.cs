using Rise.Core.Constants;
using Rise.Core.Validation;

namespace Rise.Core.Domain.ValueObjects
{
    public sealed class Image : ValueObject
    {
        #region Properties

        public string Url { get; }
        public string PublicId { get; }

        #endregion

        #region Constructors

        public Image(string url, string publicId)
        {
            Url = url;
            PublicId = publicId;
            Validate();
        }

        #endregion

        #region ValueObject Methods

        public override string ToString()
        {
            return Url;
        }

        protected override void Validate()
        {
            Validator.MaximumLength(Url, ConstSizes.ImageUrlMax, nameof(Url));
            Validator.MaximumLength(PublicId, ConstSizes.ImagePublicIdMax, nameof(PublicId));
        }

        #endregion
    }
}