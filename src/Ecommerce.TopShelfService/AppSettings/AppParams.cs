namespace Ecommerce.TopShelfService.AppSettings
{
    public static class AppParams
    {
        private static readonly string DiretorioBase = AppDomain.CurrentDomain.BaseDirectory;
        public static string CaminhoPastaDeLog =>
            Path.Combine(DiretorioBase, "Logs");

        public static string CaminhoArquivoConfiguracaoServico =>
            Path.Combine(DiretorioBase, "Config", "config.json");
    }
}
