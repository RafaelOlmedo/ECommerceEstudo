namespace ECommerce.Integracao.Domain.Interfaces
{
    public interface ISystemIOWrapper
    {
        bool ArquivoExiste(string caminhoArquivo);
        bool PastaExiste(string caminhoPasta);
        void CriaPasta(string caminhoPasta);
        string GravaArquivo(string conteudoArquivo, string caminhoCompleto);
        string CasoNaoExistaCriaPastaComNomeDiaMesAno(string caminhoPasta);
        int RecuperaQuantidadeDeArquivosEmUmaPasta (string caminhoPasta);
        string ConcatenarCaminho(string caminho1, string  caminho2);
    }
}
