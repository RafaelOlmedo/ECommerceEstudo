using ECommerce.Integracao.Domain.Logs.Base;

namespace ECommerce.Integracao.Domain.Logs
{
    public class ExportacaoCategoriasLog : BaseLog
    {
        public readonly static string
           Tag = "[EXPORTAÇÃO CATEGORIAS]",
           SubPasta = "ExportacaoCategorias",
           NomeArquivo = "log_EXCAT_",
           TipoArquivo = "txt";

        public ExportacaoCategoriasLog() :
            base(Tag, SubPasta, NomeArquivo, TipoArquivo)
        {
        }
    }
}
