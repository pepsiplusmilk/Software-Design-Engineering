namespace MoscowZooDDD.Entities.Enclosure;

public class EnclosureСharacteristics {
  public EnclosureType Type { get; }
  public int MaxAnimalCount { get; }

  public EnclosureСharacteristics(EnclosureType type, int maxAnimalCount) {
    if (maxAnimalCount <= 0) {
      throw new ArgumentException("Максимальное число животных в вольере, должно быть" +
                                  " целым положительным числом", nameof(maxAnimalCount));
    }

    if (!Enum.IsDefined(typeof(EnclosureType), type)) {
      throw new ArgumentException("Недопустимое значение для типа вольера", nameof(type));
    }
    
    Type = type;
    MaxAnimalCount = maxAnimalCount;
  }
}