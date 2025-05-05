namespace MoscowZooDDD.Entities.Events;

public interface IDomainEvent {
  public DateTime HappenedOn { get; }
}