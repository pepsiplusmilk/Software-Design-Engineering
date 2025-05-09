using Domain.Feeding;
using Infrastructure.FeedingTable;

namespace Infrastructure.Feeding;

public class FeedingRepository : IFeedingRepository {
  private readonly SortedDictionary<Guid, Domain.Feeding.Feeding> _feedings = new();
  
  public Task<IEnumerable<Domain.Feeding.Feeding>> GetAllFeedings() {
    return Task.FromResult(_feedings.Values.AsEnumerable());
  }

  public Task<Domain.Feeding.Feeding?> GetFeedingById(Guid id) {
    Domain.Feeding.Feeding? feeding;
    _feedings.TryGetValue(id, out feeding);
    return Task.FromResult(feeding);
  }

  public Task AddFeeding(Domain.Feeding.Feeding feeding) {
    _feedings[feeding.Id] = feeding;
    return Task.CompletedTask;
  }

  public Task<bool> DeleteFeeding(Guid id) {
    if (!IsFeedingExists(id).Result) {
      return Task.FromResult(false);
    }
    
    return Task.FromResult(_feedings.Remove(id));
  }

  public Task<bool> CompleteFeeding(Guid feedingId) {
    if (!IsFeedingExists(feedingId).Result) {
      return Task.FromResult(false);
    }

    if (_feedings[feedingId].IsCompleted) {
      return Task.FromResult(false);
    }
    
    _feedings[feedingId].IsCompleted = true;
    return Task.FromResult(true);
  }

  public Task<bool> ResetDay() {
    foreach (var feeding in _feedings.Values) {
      if (feeding.IsCompleted) {
        feeding.IsCompleted = false;
      }
    }
    
    return Task.FromResult(true);
  }

  public Task<bool> ChangeFeedingTime(Guid feedingId, TimeOnly newTime) {
    if (!IsFeedingExists(feedingId).Result) {
      return Task.FromResult(false);
    }
    
    _feedings[feedingId].OptimalFeedingTime = newTime;
    return Task.FromResult(true);
  }

  public Task<bool> IsFeedingExists(Guid feedingId) {
    return Task.FromResult(_feedings.ContainsKey(feedingId));
  }
}