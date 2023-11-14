using Flunt.Notifications;

namespace ECommerce.Domain.Entities.Base
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        public Guid Id { get; set; }

        public bool Invalid => !IsValid;
        public abstract void RealizaValidacoes();
        protected Guid GeraNovoId() =>
            new();
    }
}
