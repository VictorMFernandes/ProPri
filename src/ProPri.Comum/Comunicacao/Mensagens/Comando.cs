using FluentValidation.Results;
using MediatR;
using System;

namespace ProPri.Comum.Comunicacao.Mensagens
{
    public abstract class Comando : Mensagem, IRequest<bool>
    {
        public DateTime Data { get; }
        public ValidationResult ValidacaoResultado { get; set; }

        protected Comando()
        {
            Data = DateTime.Now;
        }

        public abstract bool EhValido();
    }
}
