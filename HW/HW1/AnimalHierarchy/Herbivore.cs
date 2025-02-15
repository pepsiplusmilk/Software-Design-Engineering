namespace MoscowZooERP.AnimalHierarchy;

public class Herbivore : Animal {
  protected int _kindness;
  
  public int Kindness {
    get => _kindness;
    
    set {
      if (value < 0 || value > 10) {
        throw new ArgumentException("Доброта животного, оценивается по 10-бальной шкале.");
      }
      
      _kindness = value;
    }
  }
}

public abstract class HerbivoreEgg : AbstractEgg {
  public override Herbivore CreateAnimal() {
    return new Herbivore();
  }
}