namespace MoscowZooDDD.Entities.Events;

public class AnimalMovedEvent : IDomainEvent {
  public DateTime HappenedOn { get; }
}