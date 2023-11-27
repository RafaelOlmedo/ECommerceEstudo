using ECommerce.Domain.Entities.Base;
using Flunt.Validations;

namespace Ecommerce.TopShelfService.Entities.Configuracoes
{
    public class DadosConfiguracaoServico : BaseEntity
    {
        public const string LogConfiguracao = "[CONFIG]";
        public DadosServidor Servidor { get; private set; }
        public DadosParametrosServico ParametrosServico { get; private set; }
        public string CaminhoArquivoConfiguracaoServico { get; private set; }

        public DadosConfiguracaoServico(DadosServidor servidor = default,
                                        DadosParametrosServico parametrosServico = default,
                                        string caminhoArquivoConfiguracaoServico = "")
        {
            Servidor = servidor;
            ParametrosServico = parametrosServico;
            CaminhoArquivoConfiguracaoServico = caminhoArquivoConfiguracaoServico;
        }

        public bool ValidaSeExisteArquivo()
        {
            if (Invalido) return false;

            if (!File.Exists(CaminhoArquivoConfiguracaoServico))
            {
                var mensagemErro = $"{LogConfiguracao} Falha ao buscar dados de configuração. Arquivo de conexão inexistente.";
                AddNotification(string.Empty, mensagemErro);
                return false;
            }

            return true;
        }

        public bool ValidaSeArquivoFoiPreenchido()
        {
            if (Invalido) return false;

            string configJson = File.ReadAllText(CaminhoArquivoConfiguracaoServico);

            if (string.IsNullOrEmpty(configJson))
            {
                var mensagemErro = $"{LogConfiguracao} Falha ao buscar dados de configuração. Arquivo de conexão vazio.";
                AddNotification(string.Empty, mensagemErro);
                return false;
            }
            return true;
        }

        private void ValidaSeDadosServidorFoiPreenchido()
        {
            if (Invalido) return;

            Servidor.RealizaValidacoes();
            AddNotifications(Servidor.Notifications);
        }

        private void ValidaSeParametroFoiPreenchido()
        {
            if (Invalido) return;

            ParametrosServico.RealizaValidacoes();
            AddNotifications(ParametrosServico.Notifications);
        }

        private void ValidaSeExisteDadosServidor()
        {
            if (Invalido) return;

            if (Servidor is null)
                AddNotification(nameof(Servidor), "Não foi possível recuperar as informações de conexão do SBO.");
        }

        public override void RealizaValidacoes()
        {
            if (Invalido) return;

            ValidaSeDadosServidorFoiPreenchido();
            ValidaSeParametroFoiPreenchido();
            ValidaSeExisteDadosServidor();
        }
    }
}
