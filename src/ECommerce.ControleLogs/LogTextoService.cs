using ECommerce.Domain.Interfaces.Services;
using Serilog;

namespace ECommerce.ControleLogs
{
    public class LogTextoService : ILogService
    {
        string Tag { get; set; }
        string SubPasta { get; set; }
        string NomeArquivo { get; set; }
        string TipoArquivo { get; set; }

        public LogTextoService
        (
            string tag,
            string subPasta,
            string nomeArquivo,
            string tipoArquivo
        )
        {
            Tag = tag;
            SubPasta = subPasta;
            NomeArquivo = nomeArquivo;
            TipoArquivo = tipoArquivo;
        }  

        string ILogService.Tag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string ILogService.SubPasta { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string ILogService.NomeArquivo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string ILogService.TipoArquivo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void LogErro(string mensagem, bool inicioLog = false, bool fimLog = false)
        {
            if (inicioLog)
                Log.Logger.Error($"{{{nameof(Tag)}}} =============================================================================================", Tag);

            Log.Logger.Error($"{{{nameof(Tag)}}} {mensagem}", Tag);

            if (fimLog)
                Log.Logger.Error($"{{{nameof(Tag)}}} =============================================================================================", Tag);
        }

        public void LogErroFatal(string mensagem, bool inicioLog = false, bool fimLog = false)
        {
            if (inicioLog)
                Log.Logger.Error($"{{{nameof(Tag)}}} =============================================================================================", Tag);

            Log.Logger.Error($"{{{nameof(Tag)}}} {mensagem}", Tag);

            if (fimLog)
                Log.Logger.Error($"{{{nameof(Tag)}}} =============================================================================================", Tag);
        }

        public void LogInformacao(string mensagem, bool inicioLog = false, bool fimLog = false)
        {
            if (inicioLog)
                Log.Logger.Information($"{{{nameof(Tag)}}} =============================================================================================", Tag);

            Log.Logger.Information($"{{{nameof(Tag)}}} {mensagem}", Tag);

            if (fimLog)
                Log.Logger.Information($"{{{nameof(Tag)}}} =============================================================================================", Tag);
        }
    }
}
