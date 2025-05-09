using Application.Event;
using Domain.Event;
using Domain.Feeding;
using Infrastructure.Animal;
using Infrastructure.FeedingTable;

namespace Application.FeedingService;

public class AnimalFeedingService : IAnimalFeedingService {
  private readonly IFeedingRepository _feedingRepository;
  private readonly IAnimalRepository _animalRepository;
  private readonly FeedingFactory _feedingFactory = new();
  private readonly IDomainEventService _domainEventService;

  public AnimalFeedingService(IFeedingRepository feedingRepository, IAnimalRepository animalRepository,
    IDomainEventService domainEventService) {
    _feedingRepository = feedingRepository;
    _animalRepository = animalRepository;
    _domainEventService = domainEventService;
  }
  
  public Task<bool> AddFeedingTask(Guid animalId, string foodDescription, string optimalTimeW) {
    Feeding? newFeeding = null;
    if (!TimeOnly.TryParse(optimalTimeW, out TimeOnly optimalTime)) {
      return Task.FromResult(false);
    }

    // If there is no animal with such id
    if (_animalRepository.GetAnimalById(animalId).Result == null) {
      return Task.FromResult(false);
    }

    newFeeding = _feedingFactory.Create(animalId, foodDescription,optimalTime);
    _feedingRepository.AddFeeding(newFeeding!);
    
    return Task.FromResult(true);
  }

  public Task<bool> RemoveFeedingTask(Guid feedingId) {
    return Task.FromResult(_feedingRepository.DeleteFeeding(feedingId).Result);
  }

  public Task<Feeding?> GetTaskById(Guid feedingId) {
    return Task.FromResult(_feedingRepository.GetFeedingById(feedingId).Result);
  }

  public Task<IEnumerable<Feeding>> GetFeedingTimeTable() {
    return Task.FromResult(_feedingRepository.GetAllFeedings().Result);
  }

  public Task<bool> CompleteFeedingTask(Guid feedingId) {
    var result = _feedingRepository.CompleteFeeding(feedingId);

    // If we have troubles on completing feeding
    if (!result.Result) {
      return Task.FromResult(false);
    }
    
    // Raising event that notifies report system
    _domainEventService.RaiseEvent(new AnimalFedEvent(DateTime.Now, feedingId));
    return Task.FromResult(true);
  }

  public Task<bool> ChangeTaskOptimalTime(Guid feedingId, string newOptimalTime) {
    if (!TimeOnly.TryParse(newOptimalTime, out TimeOnly optimalTime)) {
      return Task.FromResult(false);
    }
    
    return Task.FromResult(_feedingRepository.ChangeFeedingTime(feedingId, optimalTime).Result);
  }

  public Task<bool> SetNewFeedingDay() {
    var res = _feedingRepository.ResetDay();

    // If we have troubles on completing feeding
    if (!res.Result) {
      return Task.FromResult(false);
    }
    
    // Raising event that notifies report system
    _domainEventService.RaiseEvent(new NewDayStartedEvent(DateTime.Now));
    return Task.FromResult(true);
  }
}