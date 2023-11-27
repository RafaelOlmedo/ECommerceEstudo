using Flunt.Notifications;

namespace ECommerce.Domain.Entities.Base
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        public Guid Id { get; protected set; }

        public bool Invalido => !IsValid;
        public abstract void RealizaValidacoes();
        protected Guid GeraNovoId() =>
            new();
    }
}
