namespace ECommerce.Integracao.Domain.Logs.Base
{
    public abstract class BaseLog
    {
        protected BaseLog(string tag, string subPasta, string nomeArquivo, string tipoArquivo)
        {
            Tag = tag;
            SubPasta = subPasta;
            NomeArquivo = nomeArquivo;
            TipoArquivo = tipoArquivo;
        }

        public string Tag { get; private set; }
        public string SubPasta { get; private set; }
        public string NomeArquivo { get; private set; }
        public string TipoArquivo { get; private set; }

        public void LogErro(string mensagem, bool inicioLog = false, bool fimLog = false)
        {
            if (inicioLog)
                Serilog.Log.Logger.Error($"{{{nameof(Tag)}}} =============================================================================================", Tag);

            Serilog.Log.Logger.Error($"{{{nameof(Tag)}}} {mensagem}", Tag);

            if (fimLog)
                Serilog.Log.Logger.Error($"{{{nameof(Tag)}}} =============================================================================================", Tag);
        }

        public void LogInformacao(string mensagem, bool inicioLog = false, bool fimLog = false)
        {
            if (inicioLog)
                Serilog.Log.Logger.Information($"{{{nameof(Tag)}}} =============================================================================================", Tag);

            Serilog.Log.Logger.Information($"{{{nameof(Tag)}}} {mensagem}", Tag);

            if (fimLog)
                Serilog.Log.Logger.Information($"{{{nameof(Tag)}}} =============================================================================================", Tag);
        }
    }
}
