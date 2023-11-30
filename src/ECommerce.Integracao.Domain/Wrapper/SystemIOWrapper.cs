using ECommerce.Integracao.Domain.Interfaces;

namespace ECommerce.Integracao.Domain.Wrapper
{
    public class SystemIOWrapper : ISystemIOWrapper
    {
        public bool ArquivoExiste(string caminhoArquivo) =>
            File.Exists(caminhoArquivo);

        public void CriaPasta(string caminhoPasta) =>
            Directory.CreateDirectory(caminhoPasta);        

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

        public string CasoNaoExistaCriaPastaComNomeDiaMesAno(string caminhoPasta)
        {
            string dataAtualEmString = DateTime.Now.ToString("ddMMyyyy");
            string caminhoComPastaENomeArquivo = ConcatenarCaminho(caminhoPasta, dataAtualEmString);

            if (PastaExiste(caminhoComPastaENomeArquivo))
                return caminhoComPastaENomeArquivo;

            CriaPasta(caminhoComPastaENomeArquivo);

            return caminhoComPastaENomeArquivo;

        }

        public int RecuperaQuantidadeDeArquivosEmUmaPasta(string caminhoPasta)
        {
            if(!PastaExiste(caminhoPasta)) 
                return 0;

            int quantidadeArquivosNaPasta = Directory.GetFiles(caminhoPasta).Length;

            return quantidadeArquivosNaPasta;
        }

        public string ConcatenarCaminho(string caminho1, string caminho2) =>
            Path.Combine(caminho1, caminho2) ?? string.Empty;
    }
}
