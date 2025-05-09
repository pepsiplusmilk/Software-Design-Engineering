using Domain.Event;

namespace Application.Event;

public interface IDomainEventService {
  public void RaiseEvent(IDomainEvent domainEvent);
  public event Action<IDomainEvent> OnDomainEvent;
}