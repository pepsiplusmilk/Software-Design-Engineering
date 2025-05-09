using Microsoft.Extensions.Hosting;
namespace Application.ZooStatistics;

public class ZooReporterInitService : IHostedService {
  private readonly ZooReporterService _zooReporterService;

  public ZooReporterInitService(ZooReporterService zooReporterService) {
    _zooReporterService = zooReporterService;
  }
  
  public Task StartAsync(CancellationToken cancellationToken) {
    _zooReporterService.Initialize();
    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken) {
    return Task.CompletedTask;
  }
}