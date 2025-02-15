using MoscowZooERP.AnimalHierarchy;

namespace MoscowZooERP.Tests;

public class AnimalFabricMethodTest {
  [Fact]
  public void HierarchyTest() {
    MonkeyEgg eggPeace = new MonkeyEgg();
    TigerEgg eggFight = new TigerEgg();
    Animal animal = eggPeace.CreateAnimal();
    Animal otherAnimal = eggFight.CreateAnimal();

    Assert.False(animal is Predator);
    Assert.True(otherAnimal is Predator);
    Assert.False(otherAnimal is Wolf);
    Assert.False(animal is Rabbit);
  }
}
