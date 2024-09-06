using N5Domain.Events;

namespace N5Domain.Base
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();

        protected AggregateRoot(int id) : base(id)
        {
            Id = id;
        }

        public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

        public void AddEvent(IDomainEvent ev)
        {
            _events.Add(ev);
        }
    }
}