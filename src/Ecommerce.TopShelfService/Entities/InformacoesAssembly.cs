using System.Diagnostics;
using System.Reflection;

namespace Ecommerce.TopShelfService.Entities
{
    public class InformacoesAssembly
    {
        public InformacoesAssembly()
        {
            ProductName = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).ProductName;
            FileDescription = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).FileDescription;
            Comments = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).Comments;
            CompanyName = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName;
            ProductVersion = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).ProductVersion;
        }

        private string ProductName { get; set; }
        private string FileDescription { get; set; }
        private string Comments { get; set; }
        private string CompanyName { get; set; }
        private string ProductVersion { get; set; }

        public InformacoesServico GetCurrentServiceInfo()
        {
            return new InformacoesServico
            {
                NomeServico = ProductName,
                NomeExibicaoServico = FileDescription,
                DescricaoServico = $"{CompanyName} - {Comments}",
                VersaoServico = $"v{ProductVersion}"
            };
        }
    }
}
