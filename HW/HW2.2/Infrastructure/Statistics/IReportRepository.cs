using Domain.Event;

namespace Infrastructure.Statistics;

public interface IReportRepository {
  public Task<string> GetFeedingStatistic();
  public Task<string> GetMovingStatistic();
  
  public void AddInformationToFeedingStatistic(string text);
  public void AddInformationToMoveStatistic(string text);
  
  public void ClearFeedingStatistic();
}