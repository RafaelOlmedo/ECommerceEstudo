using Ecommerce.TopShelfService.AppSettings;
using Ecommerce.TopShelfService.ViewModels;
using ECommerce.Integracao.Domain.Entities;
using Newtonsoft.Json;

namespace Ecommerce.TopShelfService.Controllers
{
    public static class ConfiguracoesServicoController
    {
        public static DadosConfiguracaoServico ObtemConfiguracaoServico()
        {
            var dadosConfiguracaoServico = new DadosConfiguracaoServico(caminhoArquivoConfiguracaoServico: AppParams.CaminhoArquivoConfiguracaoServico);

            try
            {
                dadosConfiguracaoServico.ValidaSeExisteArquivo();

                if(dadosConfiguracaoServico.Invalido)
                {
                    // log
                    return dadosConfiguracaoServico;
                }

                dadosConfiguracaoServico.ValidaSeArquivoFoiPreenchido();

                if (dadosConfiguracaoServico.Invalido)
                {
                    // log
                    return dadosConfiguracaoServico;
                }

                string configJson = File.ReadAllText(AppParams.CaminhoArquivoConfiguracaoServico);

                var configuracaoServico = new ConfiguracaoServicoViewModel();
                configuracaoServico = JsonConvert.DeserializeObject<ConfiguracaoServicoViewModel>(configJson);

                dadosConfiguracaoServico = DadosConfiguracaoServicoMapping(configuracaoServico);

                dadosConfiguracaoServico.RealizaValidacoes();

                if(dadosConfiguracaoServico.Invalido)
                {
                    // Log
                    return dadosConfiguracaoServico;
                }

                AlteraArquivoConfiguracaoComSenhaCriptografada(configuracaoServico);
            }
            catch (Exception ex)
            {
                string mensagemErro = $"[CONFIG] Falha ao buscar dados de configuração. Retorno: {ex.Message}";
                dadosConfiguracaoServico.AddNotification(string.Empty, mensagemErro);
                //Log.Logger.Error(mensagemErro);
            }

            return dadosConfiguracaoServico;
        }

        private static DadosConfiguracaoServico DadosConfiguracaoServicoMapping(ConfiguracaoServicoViewModel configuracaoServicoViewModel)
        {
            return new DadosConfiguracaoServico(
                servidor: new DadosServidor(
                    nome: configuracaoServicoViewModel.Servidor.Nome,
                    baseDeDados: configuracaoServicoViewModel.Servidor.BaseDeDados,
                    usuario: configuracaoServicoViewModel.Servidor.Usuario,
                    senha: configuracaoServicoViewModel.Servidor.Senha,
                    senhaCriptografada: configuracaoServicoViewModel.Servidor.SenhaCriptografada),
                parametrosServico: new DadosParametrosServico(
                    scheduleExportacaoProdutosEmMinutos: configuracaoServicoViewModel.ParametrosServico.ScheduleExportacaoProdutosEmMinutos,
                    scheduleExportacaoCategoriasEmMinutos: configuracaoServicoViewModel.ParametrosServico.ScheduleExportacaoCategoriasEmMinutos,
                    limiteMemoria: configuracaoServicoViewModel.ParametrosServico.LimiteMemoria,
                    pastaExportacaoProdutos: configuracaoServicoViewModel.ParametrosServico.PastaExportacaoProdutos,
                    pastaExportacaoCategorias: configuracaoServicoViewModel.ParametrosServico.PastaExportacaoCategorias)
                );
        }

        private static void AlteraArquivoConfiguracaoComSenhaCriptografada(ConfiguracaoServicoViewModel configuracaoServico)
        {
            var servidor = configuracaoServico.Servidor;

            if (!string.IsNullOrEmpty(servidor.Senha))
            {
                servidor.SenhaCriptografada = servidor.Senha;
                servidor.Senha = string.Empty;
            }
            
            var configuracaoJson = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            string serializedJson = JsonConvert.SerializeObject(configuracaoServico, configuracaoJson);

            File.WriteAllText(AppParams.CaminhoArquivoConfiguracaoServico, serializedJson);

            if (string.IsNullOrEmpty(servidor.SenhaCriptografada))
            {
                string mensagemErro = $"[CONFIG] Falha ao buscar dados de configuração. Erro ao criptografar as senhas.";
                //Log.Logger.Error(mensagemErro);
            }
        }
    }
}
