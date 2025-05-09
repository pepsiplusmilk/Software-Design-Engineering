namespace Infrastructure.Animal;

using Domain.Animal;

public interface IAnimalRepository {
  public Task<IEnumerable<Animal>> GetListOfAnimals();
  public Task<Animal?> GetAnimalById(Guid id);
  
  public Task RegisterNewAnimal(Animal animal);
  public Task<bool> UnregisterAnimal(Guid animalId);

  public Task<bool> ChangeAnimalHealthStatus(Guid animalId, HealthState newHealthState);

  public Task<int> GetRepositorySize();
  public Task<IEnumerable<Animal>> GetAnimalsWithHealthStatus(HealthState fixedState);
}