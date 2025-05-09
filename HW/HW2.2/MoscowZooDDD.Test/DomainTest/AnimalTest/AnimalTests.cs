using Domain.Animal;
using Domain.Enclosure;
using NSubstitute;

namespace MoscowZooDDD.Test.DomainTest.AnimalTest;
using Domain.Animal;

public class AnimalTests {
  private static DateOnly date = 
    new DateOnly(2025, 5, 12);
  private static AnimalBio fullBio = 
    new AnimalBio("nick", date,
    "food", AnimalGender.Unspecified);
  private static AnimalState animalState = 
    new AnimalState("some specie");
  
  [Fact]
  public void CheckAnimalState() {
    Assert.Equal("some specie", animalState.Specie);
  }

  [Fact]
  public void CheckAnimalType() {
   
    
    AnimalBio partBio = new AnimalBio("", 
      date, "",
      AnimalGender.Female);
    
    Assert.Equal(partBio.BirthDate, fullBio.BirthDate);
    Assert.Equal(AnimalGender.Female, partBio.Gender);
    Assert.Equal( AnimalGender.Unspecified, fullBio.Gender);
    
    Assert.Equal("-", partBio.PreferredFood);
    Assert.Equal("-", partBio.Nickname);
    
    Assert.Equal("nick", fullBio.Nickname);
    Assert.Equal("food", fullBio.PreferredFood);
  }

  [Theory]
  [InlineData(EnclosureTypes.Enclosure)]
  [InlineData(EnclosureTypes.Cage)]
  [InlineData(EnclosureTypes.Aquarium)]
  [InlineData(EnclosureTypes.Terrarium)]
  public void CheckAnimaEnclosureTypes(EnclosureTypes type) {
    Animal animal = new Animal(
      Guid.NewGuid(), fullBio, animalState, type, 0);
    
    Assert.Equal(type, animal.RequiredEnclosureType);
  }

  [Fact]
  public void CheckAnimalToString() {
    Guid id = Guid.NewGuid();
    Animal animal = new Animal(id
      , fullBio, animalState
      , 0, 0);
    
    Assert.Equal($"{animal.Id};{id};nick;{Enum.GetName(AnimalGender.Unspecified)};{date};food;" +
                 $"{Enum.GetName(HealthState.Healthy)};some specie;",
      animal.ToString());
  }

  [Fact]
  public void CheckPredator() {
    Guid id = Guid.NewGuid();
    Predator animal = new Predator(id
      , fullBio, animalState
      , 0, 0);
    
    Assert.Equal( $"{animal.Id};{id};nick;{Enum.GetName(AnimalGender.Unspecified)};{date};food;" +
                  $"{Enum.GetName(HealthState.Healthy)};some specie;Хищник" + 
                  Environment.NewLine, animal.ToString());
  }
  
  [Fact]
  public void CheckHerbivore() {
    Guid id = Guid.NewGuid();
    Herbivore animal = new Herbivore(id
      , fullBio, animalState
      , 0, 0);
    
    Assert.Equal( $"{animal.Id};{id};nick;{Enum.GetName(AnimalGender.Unspecified)};{date};food;" +
                  $"{Enum.GetName(HealthState.Healthy)};some specie;Травоядное" + 
                  Environment.NewLine, animal.ToString());
  }

  [Fact]
  public void CheckHerbivoreFactory() {
    HerbivoreFactory animalFactory = new HerbivoreFactory();
    Guid id = Guid.NewGuid();
    
    var an1 = animalFactory.Create(id, fullBio.Nickname, date, fullBio.PreferredFood,
      fullBio.Gender, animalState.Specie, EnclosureTypes.Cage);
    var an2 = animalFactory.Create(id, fullBio.Nickname, date, fullBio.PreferredFood,
      fullBio.Gender, "", EnclosureTypes.Cage);
    var an3 = animalFactory.Create(id, fullBio.Nickname, date, fullBio.PreferredFood,
      fullBio.Gender, animalState.Specie, (EnclosureTypes)Enum.Parse(typeof(EnclosureTypes), "100"));
    var an4 =  animalFactory.Create(id, fullBio.Nickname, date, fullBio.PreferredFood,
      fullBio.Gender, animalState.Specie, EnclosureTypes.Cage);
  }
}