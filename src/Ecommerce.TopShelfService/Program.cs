using Ecommerce.TopShelfService.AppSettings;
using Ecommerce.TopShelfService.Entities;
class Program
{
    static void Main()
    {
        InformacoesAssembly informacoesAssembly = new InformacoesAssembly();
        TopShelfConfiguracoes.Init(informacoesAssembly.GetCurrentServiceInfo());
    }
}
