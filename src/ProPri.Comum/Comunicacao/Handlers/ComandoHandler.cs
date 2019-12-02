using ProPri.Comum.Comunicacao.Mensagens;
using ProPri.Comum.Comunicacao.Mensagens.Comuns.Notificacoes;

namespace ProPri.Comum.Comunicacao.Handlers
{
    public class ComandoHandler
    {
        private readonly IMediatorHandler _mediatorHandler;

        protected ComandoHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected bool ValidarComando(Comando comando)
        {
            if (comando.EhValido()) return true;

            foreach (var error in comando.ValidacaoResultado.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DominioNotificacao(comando.Tipo, error.ErrorMessage));
            }

            return false;
        }
    }
}