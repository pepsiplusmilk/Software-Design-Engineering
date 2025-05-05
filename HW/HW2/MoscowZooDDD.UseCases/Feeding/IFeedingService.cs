using MoscowZooDDD.Entities.FeedSchedule;

namespace MoscowZooDDD.UseCases.Feeding;

public interface IFeedingService {
  public void AddNewFeedingTime(Schedule schedule);
  public void ClearFeeding(int animalId, Schedule replacement);
  public void CompleteFirstUnfinishedFeeding(int animalId);
  public string GetFeedingAccomplishedReport(int animalId);
  public void StartNewDay(int animalId);
}