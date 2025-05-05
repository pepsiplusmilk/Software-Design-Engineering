namespace MoscowZooDDD.UseCases.MaintainingAnimals;
using MoscowZooDDD.Entities.Animal;

public interface IAnimalService {
  public void TransferAnimal(int animalId, int destinationEnclosureId);
  public void SetHealthStatus(int animalId, AnimalHealthState assignedState);
  
  public void AddAnimal(Animal animal);
  public void RemoveAnimal(int animalId);
}