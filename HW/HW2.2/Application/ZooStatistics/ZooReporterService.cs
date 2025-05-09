using Application.Event;
using Domain.Animal;
using Domain.Enclosure;
using Domain.Event;
using Infrastructure.Animal;
using Infrastructure.Enclosure;
using Infrastructure.FeedingTable;
using Infrastructure.Statistics;

namespace Application.ZooStatistics;

public class ZooReporterService : IZooReporterService {
  private readonly IAnimalRepository _animalRepository;
  private readonly IEnclosureRepository _enclosureRepository;
  private readonly IReportRepository _reportRepository;
  private readonly IFeedingRepository _feedingRepository;
  
  private readonly IDomainEventService _eventService;

  public ZooReporterService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository,
    IDomainEventService eventService, IReportRepository reportRepository, IFeedingRepository feedingRepository) {
    // Inits repo's and services
    _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
    _animalRepository = animalRepository;
    _enclosureRepository = enclosureRepository;
    _reportRepository = reportRepository;
    _feedingRepository = feedingRepository;
  }
  
  public Task<int> GetSizeOfAnimalsRepository() {
    return Task.FromResult(_animalRepository.GetRepositorySize().Result);
  }

  public Task<int> GetSizeOfEnclosuresRepository() {
    return Task.FromResult(_enclosureRepository.GetRepositorySize().Result);
  }

  public Task<IEnumerable<AbstractEnclosure>> GetListOfFreeEnclosures() {
    return Task.FromResult(_enclosureRepository.GetFreeEnclosures().Result);
  }

  public Task<IEnumerable<Animal>> GetListOfAnimalsWithHealthStatus(HealthState thisHealthState) {
    return Task.FromResult(_animalRepository.GetAnimalsWithHealthStatus(thisHealthState).Result);
  }

  public void HandleDomainEvent(IDomainEvent domainEvent) {
    string text;
    switch (domainEvent) {
      case AnimalMovedEvent animalMovedEvent:
        text = $"{animalMovedEvent.AnimalId};{animalMovedEvent.PreviousEnclosureId};" +
                   $"{animalMovedEvent.NowUsedEnclosureId};{animalMovedEvent.OccurredOn}" + Environment.NewLine;
        _reportRepository.AddInformationToMoveStatistic(text);
        break;
      case AnimalFedEvent animalFedEvent:
        var feedingId = animalFedEvent.FeedNoteId;
        var feedingNote = _feedingRepository.GetFeedingById(feedingId).Result!;
        var animal = _animalRepository.GetAnimalById(feedingNote.AnimalId).Result!;
        
        text = $"{animalFedEvent.FeedNoteId};{animalFedEvent.OccurredOn};{feedingNote.OptimalFeedingTime};" +
                   $"{feedingNote.AnimalId};{feedingNote.FoodDescription};{animal.Bio.PreferredFood}" + 
                   Environment.NewLine;
        _reportRepository.AddInformationToFeedingStatistic(text);
        break;
      case NewDayStartedEvent newDayStartedEvent:
        _reportRepository.ClearFeedingStatistic();
        break;
    }
  }

  public void Initialize() {
    _eventService.OnDomainEvent += HandleDomainEvent;
  }
}