using ECommerce.Domain.Entities;

namespace ECommerce.Integracao.Domain.DTOs
{
    public class ExportacaoCategoriaDto
    {
        public ExportacaoCategoriaDto(Guid id, string nome, string descricao, bool ativo, DateTime dataCriacao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            DataCriacao = dataCriacao;
        }

        public Guid Id { get; }
        public string Nome { get; }
        public string Descricao { get; }
        public bool Ativo { get; }
        public DateTime DataCriacao { get; }

        public static implicit operator ExportacaoCategoriaDto(Categoria categoria) =>
            new ExportacaoCategoriaDto(id: categoria.Id, nome: categoria.Nome, descricao: categoria.Descricao, ativo: categoria.Ativo, dataCriacao: categoria.DataCriacao);

        public static List<ExportacaoCategoriaDto> ConverteListaDeCategoriaEmListaDeExportacaoCategoriaDto(List<Categoria> categorias) =>
            categorias.Select(categoria => (ExportacaoCategoriaDto)categoria).ToList();

    }
}
