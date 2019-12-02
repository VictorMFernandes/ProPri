using ProPri.Comum.Comunicacao.Mensagens;
using ProPri.Comum.Comunicacao.Mensagens.Comuns.Notificacoes;
using System.Threading.Tasks;

namespace ProPri.Comum.Comunicacao.Handlers
{
    public interface IMediatorHandler
    {
        Task<bool> EnviarComando<T>(T comando) where T : Comando;
        Task PublicarNotificacao<T>(T notificacao) where T : DominioNotificacao;
    }
}