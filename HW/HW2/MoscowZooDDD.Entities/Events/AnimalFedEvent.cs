namespace MoscowZooDDD.Entities.Events;

public class AnimalFedEvent : IDomainEvent {
  public DateTime HappenedOn { get; }
}