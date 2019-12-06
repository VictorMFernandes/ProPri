using System;

namespace ProPri.Core.Communication.Mensagens
{
    public abstract class Message
    {
        public string Type { get; protected set; }
        public Guid AgregacaoId { get; protected set; }

        protected Message()
        {
            Type = GetType().Name;
        }
    }
}
