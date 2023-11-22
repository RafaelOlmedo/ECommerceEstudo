using Ecommerce.TopShelfService.Entities;
using Ecommerce.TopShelfService.Schedules;
using Topshelf;

namespace Ecommerce.TopShelfService.AppSettings
{
    public static class TopShelfConfiguracoes
    {
        public static void Init(InformacoesServico informacoesServico) 
        {
			try
			{
                var servico = HostFactory.Run(configuracoes =>
                {
                    configuracoes.Service<AgendamentoJobsIntegracao>(servico =>
                    {
                        servico.ConstructUsing(construtor => new AgendamentoJobsIntegracao());
                        servico.WhenStarted(servicoQuartz => servicoQuartz.IniciaAgendamento(informacoesServico));
                        servico.WhenStopped(servicoQuartz => servicoQuartz.PararAgendamento());
                    });                    
                });
			}
			finally
            {
                // TODO: Tratar logs.
            }
        }
    }
}
