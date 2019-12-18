namespace Rise.Core.Communication.Messages
{
    public abstract class CommandResult
    {
        public bool Success { get; }

        protected CommandResult(bool success)
        {
            Success = success;
        }
    }
}