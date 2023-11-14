using ECommerce.Domain.Entities.Base;

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
            throw new NotImplementedException();
        }

        public void AdicionaDataCriacao() => DataCriacao = DateTime.Now;   
    }
}
