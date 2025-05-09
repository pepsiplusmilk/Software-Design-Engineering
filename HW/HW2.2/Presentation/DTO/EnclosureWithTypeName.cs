using Domain.Animal;
using Domain.Enclosure;

namespace MoscowZooDDD.DTO;

public class EnclosureWithTypeName {
  public Guid Id { get; set; }
  public AnimalTypes CurrentAnimalType { get; set; }
  public int CurrentAnimalsInside { get; set; }
  public EnclosureState State {get; set;}
  public EnclosureTypes EnclosureType {get; set;}

  public static EnclosureWithTypeName Map(AbstractEnclosure enclosure) {
    EnclosureWithTypeName enclosureWithTypeName = new();
    
    enclosureWithTypeName.Id = enclosure.Id;
    enclosureWithTypeName.CurrentAnimalType = enclosure.CurrentAnimalType;
    enclosureWithTypeName.CurrentAnimalsInside = enclosure.CurrentAnimalsInside;
    enclosureWithTypeName.State = enclosure.State;
    enclosureWithTypeName.EnclosureType = Enum.Parse<EnclosureTypes>(enclosure.GetType().Name);
    
    return enclosureWithTypeName;
  }
}