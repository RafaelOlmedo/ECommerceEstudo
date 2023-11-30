using ECommerce.Integracao.Domain.Interfaces;

namespace ECommerce.Integracao.Domain.Wrapper
{
    public class SystemIOWrapper : ISystemIOWrapper
    {
        public bool ArquivoExiste(string caminhoArquivo) =>
            File.Exists(caminhoArquivo);

        public void CriaPasta(string caminhoPasta)
        {
            Directory.CreateDirectory(caminhoPasta);
        }

        public string GravaArquivo(string conteudoArquivo, string caminhoCompleto)
        {
            using (var streamWritter = new StreamWriter(caminhoCompleto)) 
            {
                streamWritter.Write(conteudoArquivo);
                streamWritter.Close();
            }

            return caminhoCompleto;
        }

        public bool PastaExiste(string caminhoPasta) =>
            Path.Exists(caminhoPasta);        
    }
}
