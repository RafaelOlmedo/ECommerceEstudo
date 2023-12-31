﻿using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Integracao.Domain.DTOs;
using ECommerce.Integracao.Domain.Entities;
using ECommerce.Integracao.Domain.Interfaces;
using ECommerce.Integracao.Domain.Interfaces.Services;
using ECommerce.Integracao.Domain.Logs;
using Flunt.Notifications;
using Newtonsoft.Json;

namespace ECommerce.Integracao.Domain.Services
{
    public class ExportacaoCategoriasService : IExportacaoCategoriasService
    {
        private readonly DadosConfiguracaoServico _configuracaoServico;
        private readonly ExportacaoCategoriasLog _exportacaoCategoriasLog;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ISystemIOWrapper _systemIOWrapper;

        public ExportacaoCategoriasService(ExportacaoCategoriasLog exportacaoCategoriasLog,
                                           DadosConfiguracaoServico configuracaoServico,
                                           ICategoriaRepository categoriaRepository,
                                           ISystemIOWrapper systemIOWrapper)
        {
            _exportacaoCategoriasLog = exportacaoCategoriasLog;
            _configuracaoServico = configuracaoServico;
            _categoriaRepository = categoriaRepository;
            _systemIOWrapper = systemIOWrapper;
        }

        public bool RealizaExportacaoCategoriasCadastradosEmArquivoJson()
        {
            var notificacoes = RealizaValidacoesReferenteAoCaminhoConfiguradoParaExportacao();

            if (notificacoes.Any())
            {
                foreach (var notificacao in notificacoes)
                    _exportacaoCategoriasLog.LogErro(notificacao.Message);

                return false;
            }

            var categorias = _categoriaRepository.RecuperaTodos();

            var exportacaoCategoriasDto =
                ExportacaoCategoriaDto.ConverteListaDeCategoriaEmListaDeExportacaoCategoriaDto(categorias.ToList());


            if (!categorias.Any())
            {
                _exportacaoCategoriasLog.LogInformacao($"Não existem categorias para serem exportadas.");
                return false;
            }

            string categoriasJson = ConverteExportacaoCategoriaDtoEmJson(exportacaoCategoriasDto);

            string caminhoCompletoComNomeArquivoSeraCriado = RealizaTratamentosParaControleDePastaENomeArquivoERetornaCaminhoCompletoArquivo();
            _systemIOWrapper.GravaArquivo(categoriasJson, caminhoCompletoComNomeArquivoSeraCriado);

            _exportacaoCategoriasLog.LogInformacao($"Realizada a exportação do arquivo com {exportacaoCategoriasDto.Count} categorias. Arquivo exportado: {caminhoCompletoComNomeArquivoSeraCriado}.");

            return true;
        }

        private List<Notification> RealizaValidacoesReferenteAoCaminhoConfiguradoParaExportacao()
        {
            var notificacoes = new List<Notification>();

            string caminhoConfiguradoExportacaoCategorias =
                _configuracaoServico.ParametrosServico.PastaExportacaoCategorias;

            if (!_systemIOWrapper.PastaExiste(caminhoConfiguradoExportacaoCategorias))
                notificacoes.Add(new Notification(string.Empty, $"O caminho '{caminhoConfiguradoExportacaoCategorias}' configurado para a exportação de categorias, não existe"));

            return notificacoes;
        }

        private string RealizaTratamentosParaControleDePastaENomeArquivoERetornaCaminhoCompletoArquivo()
        {
            string caminhoConfiguradoExportacaoCategorias =
               _configuracaoServico.ParametrosServico.PastaExportacaoCategorias;

            string caminhoComPastaDoDia = _systemIOWrapper.CasoNaoExistaCriaPastaComNomeDiaMesAno(caminhoConfiguradoExportacaoCategorias);

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

        private string ConverteExportacaoCategoriaDtoEmJson(List<ExportacaoCategoriaDto> exportacaoCategoriaDtos) =>
            JsonConvert.SerializeObject(exportacaoCategoriaDtos);

    }
}
