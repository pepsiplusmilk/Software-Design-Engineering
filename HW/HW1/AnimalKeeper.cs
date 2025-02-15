using MoscowZooERP.AnimalHierarchy;

namespace MoscowZooERP;

public class AnimalKeeper {
  protected List<Herbivore> HerbivoreAnimals = new List<Herbivore>();
  protected List<Predator> PredatorAnimals = new List<Predator>();

  public IReadOnlyCollection<Herbivore> GetHerbivoresList() {
    return HerbivoreAnimals.AsReadOnly();
  }

  public IReadOnlyCollection<Predator> GetPredatorList() {
    return PredatorAnimals.AsReadOnly();
  }
  
  public int GetAnimalCount() => HerbivoreAnimals.Count + PredatorAnimals.Count;

  public int GetDailyFeedAmount() {
    int total = 0;
    
    foreach (var herbivore in HerbivoreAnimals) {
      total += herbivore.RequiersFoodAmount;
    }

    foreach (var predator in PredatorAnimals) {
      total += predator.RequiersFoodAmount;
    }
    
    return total;
  }
}