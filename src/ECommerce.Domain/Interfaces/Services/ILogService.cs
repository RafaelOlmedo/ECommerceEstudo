namespace ECommerce.Domain.Interfaces.Services
{
    public interface ILogService
    {
        void LogErro(string mensagem, bool inicioLog = false, bool fimLog = false);
        void LogErroFatal(string mensagem, bool inicioLog = false, bool fimLog = false);
        void LogInformacao(string mensagem, bool inicioLog = false, bool fimLog = false);
    }
}
