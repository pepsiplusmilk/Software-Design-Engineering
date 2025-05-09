using Domain.Event;

namespace Application.Event;

public class DomainEventService : IDomainEventService {
  public void RaiseEvent(IDomainEvent domainEvent) {
    OnDomainEvent?.Invoke(domainEvent);
  }

  public event Action<IDomainEvent>? OnDomainEvent;
}