using MoscowZooERP.AnimalHierarchy;
using Moq;
using MoscowZooERP.ThingsHierarchy;

namespace MoscowZooERP.Tests;

public class MoscowZooUnitTest {
  [Fact]
  public void AdoptionNullity() {
    Animal? animal = null;
    Mock<VeterinaryClinic> mockVeterinaryClinic = new();
    MoscowZoo zoo = new MoscowZoo(mockVeterinaryClinic.Object);

    Assert.Throws<ArgumentNullException>(
      () => zoo.AdoptAnimal(animal));
  }

  [Fact]
  public void AdoptionIllness() {
    VeterinaryClinic veterinaryClinic = new VeterinaryClinic();
    MoscowZoo zoo = new(veterinaryClinic);
    Animal animal = new Animal();

    animal.Illness = true;
    
    Assert.False(zoo.AdoptAnimal(animal));
  }

  [Fact]
  public void HerbAdoptionTest() {
    VeterinaryClinic veterinaryClinic = new VeterinaryClinic();
    MoscowZoo zoo = new(veterinaryClinic);
    Animal animal = new Rabbit();
    
    animal.Illness = false;
    var result = zoo.AdoptAnimal(animal);
    
    Assert.True(result);
    Assert.True(zoo.GetHerbivoresList().Contains(animal));
    Assert.False(zoo.GetPredatorList().Contains(animal));
  }

  [Fact]
  public void ThingNullityAddTest() {
    VeterinaryClinic veterinaryClinic = new VeterinaryClinic();
    MoscowZoo zoo = new(veterinaryClinic);
    Thing? thing = null;
    
    Assert.Throws<ArgumentNullException>(() => zoo.Add(thing));
  }
  
  [Fact]
  public void ThingDuplicationAddTest() {
    VeterinaryClinic veterinaryClinic = new VeterinaryClinic();
    MoscowZoo zoo = new(veterinaryClinic);
    Thing? thing = new Thing();
    Thing? thing2 = new Thing();

    thing2.Id = thing.Id = 2;
    zoo.Add(thing);
    
    Assert.Throws<ArgumentException>(() => zoo.Add(thing2));
  }
}
