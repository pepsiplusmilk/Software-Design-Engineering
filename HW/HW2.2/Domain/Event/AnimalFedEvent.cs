namespace Domain.Event;

public record AnimalFedEvent(DateTime OccurredOn, Guid FeedNoteId) : IDomainEvent;