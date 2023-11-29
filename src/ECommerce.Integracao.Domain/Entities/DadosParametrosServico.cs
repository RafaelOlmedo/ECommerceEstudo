using Flunt.Validations;

namespace ECommerce.Integracao.Domain.Entities
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
            AddNotifications(new Contract<DadosParametrosServico>()
               .Requires()
               .IsNotNullOrEmpty(PastaExportacaoProdutos, nameof(PastaExportacaoProdutos), $"O campo '{nameof(PastaExportacaoProdutos)}' do arquivo de configuração não está preenchido.")
               .IsNotNullOrEmpty(PastaExportacaoCategorias, nameof(PastaExportacaoCategorias), $"O campo '{nameof(PastaExportacaoCategorias)}' do arquivo de configuração não está preenchido.")
               .IsGreaterThan(ScheduleExportacaoProdutosEmMinutos, 0, $"O campo '{nameof(ScheduleExportacaoProdutosEmMinutos)}' do arquivo de configuração, deve possuir um valor maior ou igual à 0.")
               .IsGreaterThan(ScheduleExportacaoCategoriasEmMinutos, 0, $"O campo '{nameof(ScheduleExportacaoCategoriasEmMinutos)}' do arquivo de configuração, deve possuir um valor maior ou igual à 0.")
               .IsGreaterThan(LimiteMemoria, 0, $"O campo '{nameof(LimiteMemoria)}' do arquivo de configuração, deve possuir um valor maior ou igual à 0.")
               );
        }
    }
}
