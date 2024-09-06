using N5Domain.Entities;

namespace N5Domain.Events
{
    public class PermissionCreatedEvent : IDomainEvent
    {
        public Permission Permission { get; }

        public PermissionCreatedEvent(Permission permission)
        {
            Permission = permission;
        }
    }
}