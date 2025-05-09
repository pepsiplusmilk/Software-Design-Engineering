using Domain.Animal;
using Domain.Enclosure;
using Domain.Event;

namespace Application.ZooStatistics;

public interface IZooReporterService {
  public Task<int> GetSizeOfAnimalsRepository();
  public Task<int> GetSizeOfEnclosuresRepository();
  public Task<IEnumerable<AbstractEnclosure>> GetListOfFreeEnclosures();
  public Task<IEnumerable<Animal>> GetListOfAnimalsWithHealthStatus(HealthState thisHealthState);
  public void HandleDomainEvent(IDomainEvent domainEvent);
  public void Initialize();
}