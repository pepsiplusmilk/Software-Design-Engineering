namespace Infrastructure.Animal;

using Domain.Animal;

public class AnimalRepository : IAnimalRepository {
  private readonly Dictionary<Guid, Animal> _animals = new();

  public Task<IEnumerable<Animal>> GetListOfAnimals() {
    return Task.FromResult(_animals.Values.AsEnumerable());
  }

  public Task<Animal?> GetAnimalById(Guid id) {
    Animal? res = null;
    _animals.TryGetValue(id, out res);
    return Task.FromResult(res);
  }

  public Task RegisterNewAnimal(Animal animal) {
    _animals[animal.Id] = animal;
    return Task.CompletedTask;
  }

  public Task<bool> IsAnimalExist(Guid id) {
    return Task.FromResult(_animals.ContainsKey(id));
  }

  public Task<bool> UnregisterAnimal(Guid animalId) {
    if (!IsAnimalExist(animalId).Result) {
      return Task.FromResult(false);
    }
    
    return Task.FromResult(_animals.Remove(animalId));
  }

  public Task<bool> ChangeAnimalHealthStatus(Guid animalId, HealthState newHealthStatus) {
    if (!IsAnimalExist(animalId).Result) {
      return Task.FromResult(false);
    }
    
    _animals[animalId].Health = newHealthStatus;
    
    return Task.FromResult(true);
  }

  public Task<int> GetRepositorySize() {
    return Task.FromResult(_animals.Count);
  }

  public Task<IEnumerable<Animal>> GetAnimalsWithHealthStatus(HealthState fixedState) {
    return Task.FromResult(from animal in _animals.Values
                           where animal.Health == fixedState
                           select animal);
  }
}