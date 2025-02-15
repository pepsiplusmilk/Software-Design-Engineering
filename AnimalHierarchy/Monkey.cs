namespace MoscowZooERP.AnimalHierarchy;

public class Monkey : Herbivore {
  public override string Behave() {
    return "Обезъяна кидает в вас банан!";
  }
}

public sealed class MonkeyEgg : HerbivoreEgg {
  public override Monkey CreateAnimal() {
    return new Monkey();
  }
}