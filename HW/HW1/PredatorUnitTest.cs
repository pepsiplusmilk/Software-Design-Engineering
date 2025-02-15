using MoscowZooERP.AnimalHierarchy;

namespace MoscowZooERP.Tests;

public class PredatorUnitTest {
  [Fact]
  public void NegativeAgressionTest() {
    Predator predator = new Predator();

    Assert.Throws<ArgumentException>(() => predator.Agression = -5);
  }
  
  [Fact]
  public void PositiveKindnessTest() {
    Predator predator = new Predator();

    Assert.Throws<ArgumentException>(() => predator.Agression = 100);
  }
}