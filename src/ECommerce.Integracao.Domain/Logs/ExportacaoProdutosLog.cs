using ECommerce.Integracao.Domain.Logs.Base;

namespace ECommerce.Integracao.Domain.Logs
{
    public class ExportacaoProdutosLog : BaseLog
    {
        public readonly static string
           Tag = "[EXPORTAÇÃO PRODUTOS]",
           SubPasta = "ExportacaoProdutos",
           NomeArquivo = "log_EXPROD_",
           TipoArquivo = "txt";

        public ExportacaoProdutosLog() :
            base(Tag, SubPasta, NomeArquivo, TipoArquivo)
        {
        }
    }
}
