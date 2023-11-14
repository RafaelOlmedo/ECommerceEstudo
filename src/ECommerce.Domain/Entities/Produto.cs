using ECommerce.Domain.Entities.Base;

namespace ECommerce.Domain.Entities
{
    public class Produto : BaseEntity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; }


        public Guid IdCategoria { get; private set; }
        public Categoria Categoria { get; private set; }


        public Produto(string nome, string descricao, decimal preco, Guid idCategoria)
        {
            Id = GeraNovoId();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            IdCategoria = idCategoria;
        }
        public override void RealizaValidacoes()
        {
            throw new NotImplementedException();
        }        

        public void AdicionaDataDeCriacao() =>
            DataCriacao = DateTime.Now;
    }
}
