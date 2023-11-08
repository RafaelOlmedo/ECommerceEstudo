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

        public Produto(string nome, string descricao, decimal preco)
        {
            Id = GeraNovoId();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
        }
        public override void RealizaValidacoes()
        {
            throw new NotImplementedException();
        }

        private Guid GeraNovoId() =>
            new();

        public void AdicionaDataDeCriacao() =>
            DataCriacao = DateTime.Now;
    }
}
