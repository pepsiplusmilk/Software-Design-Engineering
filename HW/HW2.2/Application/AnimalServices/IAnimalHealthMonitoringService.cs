using Domain.Animal;

namespace Application.AnimalServices;

public interface IAnimalHealthMonitoringService {
  public Task<bool> ChangeStatus(Guid animalId, HealthState newHealthState);
}