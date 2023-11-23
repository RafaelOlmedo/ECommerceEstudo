using ECommerce.Domain.Interfaces.Services;
using Serilog;

namespace ECommerce.ControleLogs
{
    public class LogTextoService : ILogService
    {
        private string
            _tag, _subPasta, _nomeArquivo, _tipoArquivo;

        public LogTextoService
        (
            string tag,
            string subPasta,
            string nomeArquivo,
            string tipoArquivo
        )
        {
            _tag = tag;
            _subPasta = subPasta;
            _nomeArquivo = nomeArquivo;
            _tipoArquivo = tipoArquivo;
        }

        public string Tag => _tag;
        public string SubPasta => _subPasta;
        public string NomeArquivo => _nomeArquivo;
        public string TipoArquivo => _tipoArquivo;

        public void LogErro(string mensagem, bool inicioLog = false, bool fimLog = false)
        {
            if (inicioLog)
                Log.Logger.Error($"{{{nameof(Tag)}}} =============================================================================================", _tag);

            Log.Logger.Error($"{{{nameof(Tag)}}} {mensagem}", _tag);

            if (fimLog)
                Log.Logger.Error($"{{{nameof(Tag)}}} =============================================================================================", _tag);
        }

        public void LogErroFatal(string mensagem, bool inicioLog = false, bool fimLog = false)
        {
            if (inicioLog)
                Log.Logger.Error($"{{{nameof(Tag)}}} =============================================================================================", _tag);

            Log.Logger.Error($"{{{nameof(Tag)}}} {mensagem}", _tag);

            if (fimLog)
                Log.Logger.Error($"{{{nameof(Tag)}}} =============================================================================================", _tag);
        }

        public void LogInformacao(string mensagem, bool inicioLog = false, bool fimLog = false)
        {
            if (inicioLog)
                Log.Logger.Information($"{{{nameof(Tag)}}} =============================================================================================", _tag);

            Log.Logger.Information($"{{{nameof(Tag)}}} {mensagem}", _tag);

            if (fimLog)
                Log.Logger.Information($"{{{nameof(Tag)}}} =============================================================================================", _tag);
        }
    }
}
