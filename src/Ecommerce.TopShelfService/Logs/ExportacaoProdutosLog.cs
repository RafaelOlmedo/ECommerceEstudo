﻿using ECommerce.ControleLogs;
namespace Ecommerce.TopShelfService.Logs
{
    public class ExportacaoProdutosLog : ConfiguracaoLogEntity
    {
        public ExportacaoProdutosLog() : 
            base(tag: Tag, subPasta: SubPasta, nomeArquivo: NomeArquivo, tipoArquivo: TipoArquivo)
        {
            
        }

        public readonly static string
           Tag = "[EXPORTAÇÃO PRODUTOS]",
           SubPasta = "ExportacaoProdutos",
           NomeArquivo = "log_EXPROD_",
           TipoArquivo = "txt";
    }
}
