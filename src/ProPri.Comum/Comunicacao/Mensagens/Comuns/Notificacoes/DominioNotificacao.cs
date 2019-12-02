using System;

namespace ProPri.Comum.Comunicacao.Mensagens.Comuns.Notificacoes
{
    public class DominioNotificacao
    {
        public Guid Id { get; private set; }
        public DateTime Data { get; private set; }
        public string Chave { get; private set; }
        public string Valor { get; private set; }

        public DominioNotificacao(string chave, string valor)
        {
            Data = DateTime.Now;
            Id = Guid.NewGuid();
            Chave = chave;
            Valor = valor;
        }
    }
}