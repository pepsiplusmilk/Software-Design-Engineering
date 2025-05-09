namespace Domain.Event;

public record AnimalMovedEvent(DateTime OccurredOn, Guid PreviousEnclosureId, Guid NowUsedEnclosureId, 
  Guid AnimalId) : IDomainEvent;