using Domain.Feeding;

namespace Application.FeedingService;

public interface IAnimalFeedingService {
  public Task<bool> AddFeedingTask(Guid animalId, string foodDescription, string optimalTimeW);
  public Task<bool> RemoveFeedingTask(Guid feedingId);
  
  public Task<Feeding?> GetTaskById(Guid feedingId);
  public Task<IEnumerable<Feeding>> GetFeedingTimeTable();
  
  public Task<bool> CompleteFeedingTask(Guid feedingId);
  public Task<bool> ChangeTaskOptimalTime(Guid feedingId, string newOptimalTime);
  public Task<bool> SetNewFeedingDay();
}