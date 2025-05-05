namespace MoscowZooDDD.Entities.Report;

public interface IReport {
  public void AddInformationToReport(string textInfo);
  public void BuildReport(string placementPath);
}