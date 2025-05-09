namespace Application.ReportSaving;

public interface IReportSaverService {
  public Task<bool> SaveMovementsReport();
  public Task<bool> SaveFeedingsReport();
}