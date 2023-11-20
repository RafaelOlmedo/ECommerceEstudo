using ECommerce.Domain.Constants;
using ECommerce.Domain.Entities.Base;
using Flunt.Validations;

namespace ECommerce.Domain.Entities
{
    public class Categoria : BaseEntity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public Categoria(string nome, string descricao)
        {
            Id = GeraNovoId();
            Nome = nome;
            Descricao = descricao;
        }
        public override void RealizaValidacoes()
        {
            AddNotifications(
                 new Contract<Produto>()
                    .Requires()
                    .IsNotNullOrEmpty(Nome, nameof(Nome), $"O campo '{nameof(Nome)}' deve ser preenchido.")
                    .IsNotNullOrEmpty(Descricao, nameof(Descricao), $"O campo '{nameof(Descricao)}' deve ser preenchido.")
                    .IsLowerThan(Nome.Length, CategoriaTamanhoColunas.Nome, nameof(Nome), $"O campo '{nameof(Nome)}' deve possuir no máximo {CategoriaTamanhoColunas.Nome} caracteres.")
                    .IsLowerThan(Descricao.Length, CategoriaTamanhoColunas.Descricao, nameof(Descricao), $"O campo '{nameof(Descricao)}' deve possuir no máximo {CategoriaTamanhoColunas.Descricao} caracteres.")
                );
        }

        public void AtribuiId(Guid id) =>
            Id = id;

        public void AtribuiNome(string nome) =>
            Nome = nome;

        public void AtribuiDescricao(string descricao) =>
            Descricao = descricao;

        public void AdicionaDataCriacao() => 
            DataCriacao = DateTime.Now;   
    }
}
