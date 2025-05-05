using System.Text;

namespace MoscowZooDDD.Entities.Report;

public class ReportCSV : IReport {
  private int Id { get; }
  private string TypeSuffix { get; }

  private StringBuilder _reportContent = new StringBuilder();

  public ReportCSV(int id, string typeSuffix) {
    if (id < 0) {
      throw new ArgumentException("Индекс отчетов должен быть неотрицательным числом", nameof(id));
    }

    if (typeSuffix is null) {
      typeSuffix = string.Empty;
    }
    
    Id = id;
    TypeSuffix = typeSuffix;
  }
  
  public void AddInformationToReport(string textInfo) {
    if (textInfo is null) {
      throw new ArgumentNullException(nameof(textInfo), "Текст невозможно добавить к отчету, так как его не " +
                                                        "существует");
    }
    
    _reportContent.Append(textInfo);
  }

  public void BuildReport(string placementPath) {
    if (placementPath is null) {
      throw new ArgumentNullException(nameof(placementPath), "Невозможно сохранить отчет, по несуществующему пути" +
                                                             " к папке");
    }
    
    var finalPath = placementPath + Path.DirectorySeparatorChar + $"{Id}_{TypeSuffix}.csv";
    Directory.CreateDirectory(Path.GetDirectoryName(finalPath)!);

    using StreamWriter writer = new StreamWriter(finalPath, false, Encoding.UTF8);
    writer.Write(_reportContent.ToString());
  }
}