using MoscowZooDDD.Entities.Enclosure;

namespace MoscowZooDDD.UseCases.MaintainingEnclosures;
using MoscowZooDDD.Entities.Animal;

public interface IEnclosureService {
  public void DecreaseAnimalPopulation(int enclosureId);
  public void IncreaseAnimalPopulation(int enclosureId, Animal withAnimal);
  public void AddEnclosure(Enclosure enclosure);
  public void RemoveEnclosure(int enclosureId);
  //public int TryFindFreeEnclosureId(Animal forAnimal);
}