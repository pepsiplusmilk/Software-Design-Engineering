namespace MoscowZooDDD.Infrastructure.Repo;
using MoscowZooDDD.Entities.Animal;

public interface IAnimalRepository {
  public Animal GetAnimal(int animalId);
  public int GetNextFreeAnimalId();
  public void RegisterNewAnimal(Animal animal);
  public void DischargeAnimal(int animalId);
}