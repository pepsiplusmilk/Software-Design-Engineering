namespace Domain.Animal;
using Enclosure;

public enum HealthState {
  Healthy = 0,
  Ill = 1,
  Healing = 2,
  Dead = -1,
}

public class Animal {
  // Associated id's
  public Guid Id { get; set; }
  public Guid EnclosureId { get; set; }
  
  public AnimalBio Bio { get; set; }
  public AnimalState State { get; set; }
  public HealthState Health { get; set; }
  public EnclosureTypes RequiredEnclosureType { get; }
  
  public Animal(Guid enclosureId, AnimalBio bio, AnimalState state, EnclosureTypes enclosureType, HealthState health) {
    Id = Guid.NewGuid(); // Generating for new instance of Animal new id
    
    Bio = bio;
    State = state;
    RequiredEnclosureType = enclosureType;
    EnclosureId = enclosureId;
    Health = health;
  }

  public override string ToString() {
    return $"{Id};{EnclosureId};{Bio.Nickname};{Enum.GetName(Bio.Gender)};{Bio.BirthDate};" +
           $"{Bio.PreferredFood};{Enum.GetName(Health)};{State.Specie};";
  }
}

public sealed class Predator : Animal {
  public Predator(Guid enclosureId, AnimalBio bio, AnimalState state, EnclosureTypes enclosureType, HealthState health) 
  : base(enclosureId, bio, state, enclosureType, health) {
    
  }

  public override string ToString() {
    return base.ToString() + $"Хищник" + Environment.NewLine;
  }
}

public sealed class Herbivore : Animal {
  public Herbivore(Guid enclosureId, AnimalBio bio, AnimalState state, EnclosureTypes enclosureType, HealthState health) 
    : base(enclosureId, bio, state, enclosureType, health){
    
  }

  public override string ToString() {
    return base.ToString() + $"Травоядное" + Environment.NewLine;
  }
}