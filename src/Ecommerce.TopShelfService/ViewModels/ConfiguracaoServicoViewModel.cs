using System.Runtime.Serialization;

namespace Ecommerce.TopShelfService.ViewModels
{
    public class ConfiguracaoServicoViewModel
    {
        [DataMember]
        public ServidorViewModel Servidor { get; set; }

        [DataMember]
        public ParametrosServicoViewModel ParametrosServico { get; set; }

        public const string LogConfiguracao = "[CONFIG]";
    }
}
