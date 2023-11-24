using ECommerce.ControleLogs;

namespace Ecommerce.TopShelfService.Logs
{
    public class ExportacaoCategoriasLog : ConfiguracaoLogEntity
    {
        public ExportacaoCategoriasLog() :
            base(tag: Tag, subPasta: SubPasta, nomeArquivo: NomeArquivo, tipoArquivo: TipoArquivo)
        {

        }

        public readonly static string
           Tag = "[EXPORTAÇÃO CATEGORIAS]",
           SubPasta = "ExportacaoCategorias",
           NomeArquivo = "log_EXCATE_",
           TipoArquivo = "txt";
    }
}
