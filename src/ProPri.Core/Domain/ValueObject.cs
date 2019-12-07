using FluentValidation.Results;

namespace ProPri.Core.Domain
{
    public abstract class ValueObject
    {
        protected ValidationResult ValidationResult { get; set; }
        protected abstract void Validate();
        public abstract override string ToString();
    }
}