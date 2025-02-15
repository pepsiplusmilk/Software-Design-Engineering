namespace MoscowZooERP.AnimalHierarchy;

public class Tiger : Predator {
  public override string Behave() {
    return "Тигр рычит на вас";
  }
}

public sealed class TigerEgg : PredatorEgg {
  public override Tiger CreateAnimal() {
    return new Tiger();
  }
}