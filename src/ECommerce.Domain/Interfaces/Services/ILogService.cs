namespace ECommerce.Domain.Interfaces.Services
{
    public interface ILogService
    {
        string Tag { get; set; }
        string SubPasta { get; set; }
        string NomeArquivo { get; set; }
        string TipoArquivo { get; set; }

        void LogErro(string mensagem, bool inicioLog = false, bool fimLog = false);
        void LogErroFatal(string mensagem, bool inicioLog = false, bool fimLog = false);
        void LogInformacao(string mensagem, bool inicioLog = false, bool fimLog = false);
    }
}
