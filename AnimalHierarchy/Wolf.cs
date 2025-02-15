namespace MoscowZooERP.AnimalHierarchy;

public class Wolf : Predator {
  public override string Behave() {
    return "Волк лает";
  }
}

public sealed class WolfEgg : PredatorEgg {
  public override Wolf CreateAnimal() {
    return new Wolf();
  }
}