﻿using Ecommerce.TopShelfService.Logs.Base;

namespace Ecommerce.TopShelfService.Logs
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
