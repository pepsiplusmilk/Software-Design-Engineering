namespace MoscowZooERP;
using AnimalHierarchy;
using ThingsHierarchy;

public class MoscowZoo : AnimalKeeper, IAccounted {
  private VeterinaryClinic _clinic;
  private Dictionary<int, Thing> _things = new Dictionary<int, Thing>();

  public MoscowZoo(VeterinaryClinic newClinic) {
    _clinic = newClinic;
  }

  public bool AdoptAnimal(Animal? animal) {
    if (animal is null) {
      throw new ArgumentNullException("В перевозке не было обнаружено животных.");
    }
    
    var result = _clinic.CheckIllnessStatus(animal);

    if (result is null) {
      return false;
    }

    if (animal is Herbivore) {
      HerbivoreAnimals.Add(animal as Herbivore);
    } else {
      PredatorAnimals.Add(animal as Predator);
    }
    
    return true;
  }

  // Can be extendable to check if thing broken
  public bool Add(Thing? thing) {
    if (thing is null) {
      throw new ArgumentNullException("В грузовике не обнаружено вещей");
    }

    if (!_things.TryAdd(thing.Id, thing)) {
      throw new ArgumentException("Предмет с таким инвентарным номером уже существует.");
    }

    return true;
  }

  public Thing this[int index] => _things[index];
  
  public IReadOnlyDictionary<int, Thing> GetThingsList() => _things.AsReadOnly();
}