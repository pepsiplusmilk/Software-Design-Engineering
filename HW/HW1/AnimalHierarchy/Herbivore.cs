namespace MoscowZooERP.AnimalHierarchy;

/// <summary>
/// Класс наследник Animal. Реализует класс травоядных животных
/// </summary>
public class Herbivore : Animal {
  protected int _kindness;
  
  // Свойство характеризующее доброту животного
  // Учитывается при формировании списков контактного зоопарка
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

// Фабрика травоядных
public abstract class HerbivoreEgg : AbstractEgg {
  public override Herbivore CreateAnimal() {
    return new Herbivore();
  }
}