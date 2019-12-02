using System;

namespace ProPri.Comum.Comunicacao.Mensagens
{
    public abstract class Mensagem
    {
        public string Tipo { get; protected set; }
        public Guid AgregacaoId { get; protected set; }

        protected Mensagem()
        {
            Tipo = GetType().Name;
        }
    }
}
