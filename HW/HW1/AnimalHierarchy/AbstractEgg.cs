namespace MoscowZooERP.AnimalHierarchy;

public abstract class AbstractEgg {
  public virtual Animal CreateAnimal() {
    return new Animal();
  }
}