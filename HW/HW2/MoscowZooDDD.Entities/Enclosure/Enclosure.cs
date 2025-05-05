namespace MoscowZooDDD.Entities.Enclosure;

public enum InhabitantsType {
  Herbivores = 1,
  Neutral = 0,
  Predatory = -1
}

public class Enclosure {
  public int Id { get; }
  
  public int CurrentAnimalCount { get; private set; }
  public InhabitantsType CurrentInhabitantType { get; private set; }
  
  public EnclosureСharacteristics Characteristics { get; }

  public Enclosure(int id, EnclosureСharacteristics characteristics) {
    if (id < 0) {
      throw new ArgumentException("Инвентарный номер вольера, должен быть положительным целым числом", nameof(id));
    }

    CurrentInhabitantType = InhabitantsType.Neutral;
    CurrentAnimalCount = 0;
    
    Id = id;
    Characteristics = characteristics;
  }

  public void IncrementCurrentAnimalCount() {
    ++CurrentAnimalCount;
  }

  public void DecrementCurrentAnimalCount() {
    --CurrentAnimalCount;
  }

  public void ChangeCellInhabitantType(InhabitantsType newType) {
    CurrentInhabitantType = newType;
  }

  public override string ToString() {
    return $"{Id};{CurrentAnimalCount};{Characteristics.MaxAnimalCount};{Characteristics.Type}" + Environment.NewLine;
  }
}