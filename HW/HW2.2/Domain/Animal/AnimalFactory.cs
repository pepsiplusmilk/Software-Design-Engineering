using Domain.Enclosure;

namespace Domain.Animal;

public abstract class AnimalFactory {
  public abstract Animal? Create(Guid enclosureId, string nickName, DateOnly birthDate, string
    preferredFood, AnimalGender gender, string animalSpecie, EnclosureTypes preferredEnclosureType);
}

public class PredatorFactory : AnimalFactory {
  public override Animal? Create(Guid enclosureId, string nickName, DateOnly birthDate,
    string preferredFood, AnimalGender gender, string animalSpecie, EnclosureTypes preferredEnclosureType) {
    if (animalSpecie == string.Empty) {
      return null;
    }

    if (!Enum.IsDefined(typeof(AnimalGender), gender) || 
        !Enum.IsDefined(typeof(EnclosureTypes), preferredEnclosureType)) {
      return null;
    }

    if (birthDate >= DateOnly.FromDateTime(DateTime.Now)) {
      return null;
    }
    
    AnimalBio newAnimalBiography = new AnimalBio(nickName, birthDate, preferredFood, gender);
    AnimalState newAnimalState = new AnimalState(animalSpecie);
    
    return new Predator(enclosureId, newAnimalBiography, newAnimalState, preferredEnclosureType, HealthState.Healthy);
  }
}

public class HerbivoreFactory : AnimalFactory {
  public override Animal? Create(Guid enclosureId, string nickName, DateOnly birthDate, 
    string preferredFood, AnimalGender gender, string animalSpecie, EnclosureTypes preferredEnclosureType) {
    if (animalSpecie == string.Empty) {
      return null;
    }

    if (!Enum.IsDefined(typeof(AnimalGender), gender) || 
        !Enum.IsDefined(typeof(EnclosureTypes), preferredEnclosureType)) {
      return null;
    }

    if (birthDate >= DateOnly.FromDateTime(DateTime.Now)) {
      return null;
    }
    
    AnimalBio newAnimalBiography = new AnimalBio(nickName, birthDate, preferredFood, gender);
    AnimalState newAnimalState = new AnimalState(animalSpecie);
    
    return new Herbivore(enclosureId, newAnimalBiography, newAnimalState, preferredEnclosureType, HealthState.Healthy);
  }
}