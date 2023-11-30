using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Integracao.Domain.DTOs;
using ECommerce.Integracao.Domain.Entities;
using ECommerce.Integracao.Domain.Interfaces;
using ECommerce.Integracao.Domain.Interfaces.Services;
using ECommerce.Integracao.Domain.Logs;
using Flunt.Notifications;
using Newtonsoft.Json;

namespace ECommerce.Integracao.Domain.Services
{
    public class ExportacaoProdutosService : IExportacaoProdutosService
    {
        private readonly DadosConfiguracaoServico _configuracaoServico;
        private readonly ExportacaoProdutosLog _exportacaoProdutosLog;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ISystemIOWrapper _systemIOWrapper;

        public ExportacaoProdutosService(ExportacaoProdutosLog exportacaoProdutosLog,
                                           DadosConfiguracaoServico configuracaoServico,
                                           IProdutoRepository produtoRepository,
                                           ISystemIOWrapper systemIOWrapper)
        {
            _exportacaoProdutosLog = exportacaoProdutosLog;
            _configuracaoServico = configuracaoServico;
            _produtoRepository = produtoRepository;
            _systemIOWrapper = systemIOWrapper;
        }

        public bool RealizaExportacaoProdutosCadastradosEmArquivoJson()
        {
            var notificacoes = RealizaValidacoesReferenteAoCaminhoConfiguradoParaExportacao();

            if (notificacoes.Any())
            {
                foreach (var notificacao in notificacoes)
                    _exportacaoProdutosLog.LogErro(notificacao.Message);

                return false;
            }

            var produtos = _produtoRepository.RecuperaTodos();            

            var exportacaoProdutosDto =
                ExportacaoProdutoDto.ConverteListaDeProdutosEmListaDeExportacaoProdutosDto(produtos.ToList());


            if (!produtos.Any())
            {
                _exportacaoProdutosLog.LogInformacao($"Não existem produtos para serem exportadas.");
                return false;
            }

            string produtosJson = ConverteExportacaoProdutoDtoEmJson(exportacaoProdutosDto);

            string caminhoCompletoComNomeArquivoSeraCriado = RealizaTratamentosParaControleDePastaENomeArquivoERetornaCaminhoCompletoArquivo();
            _systemIOWrapper.GravaArquivo(produtosJson, caminhoCompletoComNomeArquivoSeraCriado);

            _exportacaoProdutosLog.LogInformacao($"Realizada a exportação do arquivo com {exportacaoProdutosDto.Count} produtos. Arquivo exportado: {caminhoCompletoComNomeArquivoSeraCriado}.");

            return true;
        }

        private List<Notification> RealizaValidacoesReferenteAoCaminhoConfiguradoParaExportacao()
        {
            var notificacoes = new List<Notification>();

            string caminhoConfiguradoExportacaoProdutos =
                _configuracaoServico.ParametrosServico.PastaExportacaoProdutos;

            if (!_systemIOWrapper.PastaExiste(caminhoConfiguradoExportacaoProdutos))
                notificacoes.Add(new Notification(string.Empty, $"O caminho '{caminhoConfiguradoExportacaoProdutos}' configurado para a exportação de produtos, não existe"));

            return notificacoes;
        }

        private string RealizaTratamentosParaControleDePastaENomeArquivoERetornaCaminhoCompletoArquivo()
        {
            string caminhoConfiguradoExportacaoProdutos =
               _configuracaoServico.ParametrosServico.PastaExportacaoProdutos;

            string caminhoComPastaDoDia = _systemIOWrapper.CasoNaoExistaCriaPastaComNomeDiaMesAno(caminhoConfiguradoExportacaoProdutos);

            string nomeArquivoQueSeraExportado = GeraNomeArquivoQueSeraExportado(caminhoComPastaDoDia);

            return _systemIOWrapper.ConcatenarCaminho(caminhoComPastaDoDia, nomeArquivoQueSeraExportado);
        }

        private string GeraNomeArquivoQueSeraExportado(string caminhoExportacao)
        {
            int quantidadeArquivosNaPastaDeExportacaoParaDiaAtual = _systemIOWrapper.RecuperaQuantidadeDeArquivosEmUmaPasta(caminhoExportacao);
            int sequencialNomeArquivo = ++quantidadeArquivosNaPastaDeExportacaoParaDiaAtual;

            string dataAtualEmString = DateTime.Now.ToString("ddMMyyyy");
            string nomeArquivoQueSeraExportado = $"{dataAtualEmString}_{sequencialNomeArquivo}.json";

            return nomeArquivoQueSeraExportado;
        }

        private string ConverteExportacaoProdutoDtoEmJson(List<ExportacaoProdutoDto> exportacaoProdutoDtos) =>
            JsonConvert.SerializeObject(exportacaoProdutoDtos);
    }
}
