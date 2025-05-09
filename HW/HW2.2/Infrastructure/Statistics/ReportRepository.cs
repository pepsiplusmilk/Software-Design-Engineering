using System.Text;

namespace Infrastructure.Statistics;

public class ReportRepository : IReportRepository {
  private readonly StringBuilder _movementsReportBuilder = new();
  private readonly StringBuilder _feedingsReportBuilder = new();

  public ReportRepository() {
    // Adding headers in both reports
    _movementsReportBuilder.AppendLine("Уникальный номер животного;Уникальный номер предыдущего вольера;" +
                                       "Уникальный номер текущего вольера;Дата и время перемещения");
    _feedingsReportBuilder.AppendLine(
      "Уникальный номер события кормления;Кормление было выполнено;Ожидалось выполнение;Уникальный номер животного;" +
      "Описание еды которая была выдана;Описание предпочитаемой еды");
  }
  
  public Task<string> GetFeedingStatistic() {
    return Task.FromResult(_feedingsReportBuilder.ToString());
  }

  public Task<string> GetMovingStatistic() {
    return Task.FromResult(_movementsReportBuilder.ToString());
  }

  public void AddInformationToFeedingStatistic(string text) {
    _feedingsReportBuilder.Append(text);
    Console.WriteLine(text);
  }

  public void AddInformationToMoveStatistic(string text) {
    _movementsReportBuilder.Append(text);
    Console.WriteLine(text);
  }

  public void ClearFeedingStatistic() {
    _feedingsReportBuilder.Clear();
    _feedingsReportBuilder.AppendLine(
      "Уникальный номер события кормления;Кормление было выполнено;Ожидалось выполнение;Уникальный номер животного;" +
      "Описание еды которая была выдана;Описание предпочитаемой еды");
  }
}