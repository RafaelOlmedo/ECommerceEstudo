using ECommerce.Domain.Interfaces.Services;

namespace ECommerce.ControleLogs
{
    public class LogServiceFactory : ILogServiceFactory
    {
        private readonly ConfiguracaoLogEntity _configuracaoLogEntity;        

        public ILogService CriaLogCategorias()
        {
            return new LogTextoService(_configuracaoLogEntity.Tag, _configuracaoLogEntity.SubPasta, _configuracaoLogEntity.NomeArquivo, _configuracaoLogEntity.TipoArquivo);
        }

        public ILogService CriaLogProdutos()
        {
            return new LogTextoService(_configuracaoLogEntity.Tag, _configuracaoLogEntity.SubPasta, _configuracaoLogEntity.NomeArquivo, _configuracaoLogEntity.TipoArquivo);
        }
    }
}
