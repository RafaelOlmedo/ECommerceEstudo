using Flunt.Notifications;

namespace ECommerce.Domain.Entities.Base
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        public Guid Id { get; set; }

        public abstract void RealizaValidacoes();
    }
}
