using ImportFile.SharedKernel.Messaging;
using System.Collections.Generic;

namespace ImportFile.SharedKernel.Domain
{
    public abstract class Entity
    {
        public string Id { get; protected set; }

        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> GetEvents() => _events.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent eventItem)
        {
            _events.Add(eventItem);
        }

        internal void ClearDomainEvents()
        {
            _events?.Clear();
        }
    }
}