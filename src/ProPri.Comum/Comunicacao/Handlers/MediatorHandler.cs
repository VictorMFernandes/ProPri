using MediatR;
using ProPri.Comum.Comunicacao.Mensagens;
using ProPri.Comum.Comunicacao.Mensagens.Comuns.Notificacoes;
using System.Threading.Tasks;

namespace ProPri.Comum.Comunicacao.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> EnviarComando<T>(T comando) where T : Comando
        {
            return await _mediator.Send(comando);
        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : DominioNotificacao
        {
            await _mediator.Publish(notificacao);
        }
    }
}