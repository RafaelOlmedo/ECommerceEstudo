using ECommerce.Domain.Entities.Base;
using Flunt.Validations;

namespace Ecommerce.TopShelfService.Entities.Configuracoes
{
    public class DadosServidor : BaseEntity
    {
        public DadosServidor(
            string nome,
            string baseDeDados,
            string usuario,
            string senha,
            string senhaCriptografada
            )
        {
            Nome = nome;
            BaseDeDados = baseDeDados;
            Usuario = usuario;
            Senha = senha;
            SenhaCriptografada = senhaCriptografada;
        }

        public string Nome { get; }
        public string BaseDeDados { get; set; }
        public string Usuario { get; }
        public string Senha { get; }
        public string SenhaCriptografada { get; }

        public string RecuperaSenha() =>
            string.IsNullOrEmpty(Senha) ? SenhaCriptografada : Senha;        

        public override void RealizaValidacoes()
        {
            bool senhaNaoFoiPreenchida = string.IsNullOrEmpty(Senha) && string.IsNullOrEmpty(SenhaCriptografada);

            AddNotifications(new Contract<DadosServidor>()
                .Requires()
                .IsNotNullOrEmpty(Nome, nameof(Nome), "O campo 'Nome' do arquivo de configuração não está preenchido.")
                .IsNotNullOrEmpty(BaseDeDados, nameof(BaseDeDados), "O campo 'Driver' do arquivo de configuração não está preenchido.")
                .IsNotNullOrEmpty(Usuario, nameof(Usuario), "O campo 'Usuario' do arquivo de configuração não está preenchido.")
                .IsFalse(senhaNaoFoiPreenchida, "", "O campo 'Senha' do arquivo de configuração não está preenchido."));
        }
    }
}
