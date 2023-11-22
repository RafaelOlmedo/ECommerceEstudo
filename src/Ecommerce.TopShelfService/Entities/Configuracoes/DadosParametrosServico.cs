using ECommerce.Domain.Entities.Base;

namespace Ecommerce.TopShelfService.Entities.Configuracoes
{
    public class DadosParametrosServico : BaseEntity
    {
        public int ScheduleExportacaoProdutosEmMinutos { get; }
        public int ScheduleExportacaoCategoriasEmMinutos { get; }
        public int LimiteMemoria { get; }
        public string PastaExportacaoProdutos { get; }
        public string PastaExportacaoCategorias { get; }

        public DadosParametrosServico(string pastaExportacaoProdutos,
                                      string pastaExportacaoCategorias,
                                      int scheduleExportacaoProdutosEmMinutos = default,
                                      int scheduleExportacaoCategoriasEmMinutos = default,
                                      int limiteMemoria = default)
        {
            ScheduleExportacaoProdutosEmMinutos = scheduleExportacaoProdutosEmMinutos;
            ScheduleExportacaoCategoriasEmMinutos = scheduleExportacaoCategoriasEmMinutos;
            LimiteMemoria = limiteMemoria;
            PastaExportacaoProdutos = pastaExportacaoProdutos;
            PastaExportacaoCategorias = pastaExportacaoCategorias;
        }

        public override void RealizaValidacoes()
        {
            throw new NotImplementedException();
        }
    }
}
