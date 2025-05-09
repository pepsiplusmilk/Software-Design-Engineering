using Domain.Animal;
using Infrastructure.Animal;

namespace Application.AnimalServices;

public class AnimalHealthMonitoringService : IAnimalHealthMonitoringService{
  private readonly IAnimalRepository _animalRepository;

  public AnimalHealthMonitoringService(IAnimalRepository animalRepository) {
    _animalRepository = animalRepository;
  }
  
  public Task<bool> ChangeStatus(Guid animalId, HealthState newHealthState) {
    return Task.FromResult(_animalRepository.ChangeAnimalHealthStatus(animalId, newHealthState).Result);
  }
}