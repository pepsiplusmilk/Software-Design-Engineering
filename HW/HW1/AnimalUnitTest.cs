using Xunit.Sdk;

namespace MoscowZooERP.Tests;

using MoscowZooERP;
using MoscowZooERP.AnimalHierarchy;

public class AnimalUnitTest {
  [Fact]
  public void RequiersFoodAmountTest() {
    Animal animal = new Animal();

    Assert.Throws<ArgumentException>(() => animal.RequiersFoodAmount = -5);
  }

  [Fact]
  public void NicknameNullityTest() {
    Animal animal = new Animal();

    animal.Nickname = null;
    
    Assert.True(animal.Nickname == "");
  }

  [Fact]
  public void NicknameEmptyTest() {
    Animal animal = new Animal();
    
    animal.Nickname = "";
    
    Assert.True(animal.Nickname == "");
  }
}