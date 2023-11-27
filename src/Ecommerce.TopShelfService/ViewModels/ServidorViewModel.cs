using Ecommerce.TopShelfService.Extensions;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Ecommerce.TopShelfService.ViewModels
{
    [DataContract]
    public class ServidorViewModel
    {
        private const string ChaveCriptografia = "#my*S3cr3t";

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string BaseDeDados { get; set; }

        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public string Senha { get; set; }

        [DataMember]
        [JsonConverter(typeof(CriptografaJson), ChaveCriptografia)]
        public string SenhaCriptografada { get; set; }
    }
}
