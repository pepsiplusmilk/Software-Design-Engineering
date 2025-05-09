namespace Domain.Event;

public record NewDayStartedEvent(DateTime OccurredOn) : IDomainEvent;