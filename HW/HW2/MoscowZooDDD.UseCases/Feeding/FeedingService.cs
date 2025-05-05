using MoscowZooDDD.Entities.FeedSchedule;
using MoscowZooDDD.Infrastructure.Repo;

namespace MoscowZooDDD.UseCases.Feeding;

public class FeedingService : IFeedingService {
  private readonly IAnimalRepository _animalRepository;

  public FeedingService(IAnimalRepository animalRepository) {
    _animalRepository = animalRepository;
  }

  public void AddNewFeedingTime(Schedule schedule) {
    var animal = _animalRepository.GetAnimal(schedule.AnimalId);
    animal.AddNewFeedingEvent(schedule);
  }

  public void ClearFeeding(int animalId, Schedule replacement) {
    var animal = _animalRepository.GetAnimal(animalId);
    animal.ClearFeedingTimeTable();
    animal.AddNewFeedingEvent(replacement);
  }

  public void CompleteFirstUnfinishedFeeding(int animalId) {
    var animal = _animalRepository.GetAnimal(animalId);
    animal.PerformFirstPossibleFeeding();
  }

  public string GetFeedingAccomplishedReport(int animalId) {
    return _animalRepository.GetAnimal(animalId).GetDailyFeedingReport();
  }

  public void StartNewDay(int animalId) {
    _animalRepository.GetAnimal(animalId).RebuildTimeTableForAnotherDay();
  }
}