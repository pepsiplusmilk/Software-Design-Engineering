namespace MoscowZooERP.AnimalHierarchy;

public class Rabbit : Herbivore {
  public override string Behave() {
    return "Кролик бегает по траве";
  }
}

public sealed class RabbitEgg : HerbivoreEgg {
  public override Rabbit CreateAnimal() {
    return new Rabbit();
  }
}