namespace MoscowZooERP.AnimalHierarchy;

public class Predator : Animal {
  protected int _agression;

  public int Agression {
    get => _agression;

    set {
      if (value < 0 || value > 10) {
        throw new ArgumentException("Агрессивность животного оценивается по 10-бальной шкале.");
      }
      
      _agression = -value;
    }
  }
}

public abstract class PredatorEgg : AbstractEgg {
  public override Predator CreateAnimal() {
    return new Predator();
  }
}