namespace MoscowZooERP.AnimalHierarchy;

/// <summary>
/// Абстрактная фабрика животных
/// </summary>
public abstract class AbstractEgg {
  public virtual Animal CreateAnimal() {
    return new Animal();
  }
}