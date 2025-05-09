using System.Text;
using Infrastructure.Statistics;

namespace Application.ReportSaving;

public class ReportSaverService : IReportSaverService {
  private readonly IReportRepository _reportRepository;
  private readonly string _reportPath = Path.Combine(Environment.CurrentDirectory, "Reports", "Report");
  private static int _reportsSaved = 0;

  public ReportSaverService(IReportRepository reportRepository) {
    _reportRepository = reportRepository;
    Directory.CreateDirectory(Path.GetDirectoryName(_reportPath)!);
  }

  private bool WriteFileOnADisk(string text, string path) {
    Console.WriteLine(path);

    try {
      using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8)) {
        writer.Write(text);
      }
    }
    catch (Exception) {
      return false;
    }
    
    return true;
  }
  
  public Task<bool> SaveMovementsReport() {
    //Console.WriteLine($"{_reportsSaved}");
    var text = _reportRepository.GetMovingStatistic().Result;
    var filePath = _reportPath + $"{_reportsSaved++}_movements.csv";
    //Console.WriteLine($"{_reportsSaved}");
    return Task.FromResult(WriteFileOnADisk(text, filePath));
  }

  public Task<bool> SaveFeedingsReport() {
    var text = _reportRepository.GetFeedingStatistic().Result;
    var filePath = _reportPath + $"{_reportsSaved++}_feedings.csv";
    return Task.FromResult(WriteFileOnADisk(text, filePath));
  }
}