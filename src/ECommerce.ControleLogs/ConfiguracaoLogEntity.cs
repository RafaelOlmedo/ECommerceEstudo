namespace ECommerce.ControleLogs
{
    public abstract class ConfiguracaoLogEntity
    {
        protected ConfiguracaoLogEntity(string tag, string subPasta, string nomeArquivo, string tipoArquivo)
        {
            Tag = tag;
            SubPasta = subPasta;
            NomeArquivo = nomeArquivo;
            TipoArquivo = tipoArquivo;
        }

        public string Tag { get; set; }
        public string SubPasta { get; set; }
        public string NomeArquivo { get; set; }
        public string TipoArquivo { get; set; }
    }
}
