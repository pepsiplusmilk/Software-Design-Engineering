namespace MoscowZooERP;
using AnimalHierarchy;

public class VeterinaryClinic : AnimalKeeper {
  public Animal? CheckIllnessStatus(Animal animal) {
    if (animal.Illness) {
      if (animal is Herbivore) {
        HerbivoreAnimals.Add(animal as Herbivore);
      }

      if (animal is Predator) {
        PredatorAnimals.Add(animal as Predator);
      }

      return null;
    }
    
    return animal;
  }
}