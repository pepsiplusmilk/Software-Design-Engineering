namespace Infrastructure.FeedingTable;

public interface IFeedingRepository {
  
  public Task<IEnumerable<Domain.Feeding.Feeding>> GetAllFeedings();
  public Task<Domain.Feeding.Feeding?> GetFeedingById(Guid id);
  
  public Task AddFeeding(Domain.Feeding.Feeding feeding);
  public Task<bool> DeleteFeeding(Guid id);

  public Task<bool> CompleteFeeding(Guid feedingId);
  public Task<bool> ResetDay();

  public Task<bool> ChangeFeedingTime(Guid feedingId, TimeOnly newTime);
  public Task<bool> IsFeedingExists(Guid feedingId);
}