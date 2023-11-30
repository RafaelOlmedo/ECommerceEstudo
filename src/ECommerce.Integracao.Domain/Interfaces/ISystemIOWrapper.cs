namespace ECommerce.Integracao.Domain.Interfaces
{
    public interface ISystemIOWrapper
    {
        bool ArquivoExiste(string caminhoArquivo);
        bool PastaExiste(string caminhoPasta);
        void CriaPasta(string caminhoPasta);
        string GravaArquivo(string conteudoArquivo, string caminhoCompleto);
    }
}
