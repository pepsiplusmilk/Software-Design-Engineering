namespace MoscowZooDDD.Entities.Animal;
//using MoscowZooDDD.Entities;

public record AnimalBiologicalDescription {
  public string Specie { get;}
  public Enclosure.EnclosureType EnclosureType { get; }

  public AnimalBiologicalDescription(string specie, Enclosure.EnclosureType enclosureType) {
    if (string.IsNullOrEmpty(specie)) {
      throw new ArgumentException("Вид животного не может быть задан пустой строкой", nameof(specie));
    }

    if (!Enum.IsDefined(typeof(Enclosure.EnclosureType), enclosureType)) {
      throw new ArgumentException("Такой тип клеток не поддерживается в базе данных", nameof(enclosureType));
    }
    
    Specie = specie;
    EnclosureType = enclosureType;
  }

  public override string ToString() {
    return Specie;
  }
}