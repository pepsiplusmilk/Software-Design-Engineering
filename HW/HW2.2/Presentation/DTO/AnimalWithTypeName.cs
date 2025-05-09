using Domain.Enclosure;

namespace MoscowZooDDD.DTO;

using Domain.Animal;

public class AnimalWithTypeName {
  public Guid Id { get; set; }
  public Guid EnclosureId { get; set; }
  public AnimalBio Bio { get; set; }
  public AnimalState State { get; set; }
  public EnclosureTypes RequiredEnclosureType { get; set; }
  public AnimalTypes AnimalType { get; set; }
  public HealthState Health { get; set; }
  public static AnimalWithTypeName Map(Animal animal) {
    AnimalWithTypeName animalWithTypeName = new();
    
    animalWithTypeName.Id = animal.Id;
    animalWithTypeName.EnclosureId = animal.EnclosureId;
    animalWithTypeName.Bio = animal.Bio;
    animalWithTypeName.State = animal.State;
    animalWithTypeName.RequiredEnclosureType = animal.RequiredEnclosureType;
    animalWithTypeName.AnimalType = Enum.Parse<AnimalTypes>(animal.GetType().Name);
    animalWithTypeName.Health = animal.Health;
    
    return animalWithTypeName;
  }
}