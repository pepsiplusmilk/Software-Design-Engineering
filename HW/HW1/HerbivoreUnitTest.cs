using MoscowZooERP.AnimalHierarchy;

namespace MoscowZooERP.Tests;

public class HerbivoreUnitTest {
  [Fact]
  public void NegativeKindnessTest() {
    Herbivore herbivore = new Herbivore();

    Assert.Throws<ArgumentException>(() => herbivore.Kindness = -5);
  }
  
  [Fact]
  public void PositiveKindnessTest() {
    Herbivore herbivore = new Herbivore();

    Assert.Throws<ArgumentException>(() => herbivore.Kindness = 100);
  }
}