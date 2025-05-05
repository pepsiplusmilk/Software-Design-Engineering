using MoscowZooDDD.Entities.Animal;

namespace MoscowZooDDD.Infrastructure.Repo;
using MoscowZooDDD.Entities.Enclosure;

public class EnclosureRepository : IEnclosureRepository {
  private readonly Dictionary<int, Enclosure> _enclosures = new();
  private readonly SortedSet<int> _enclosuresMexCounter = new();

  public Enclosure GetEnclosure(int enclosureId) {
    if (enclosureId < 0 || enclosureId >= _enclosures.Count || _enclosuresMexCounter.Contains(enclosureId)) {
      throw new ArgumentOutOfRangeException(nameof(enclosureId),
        "Вольер с таким уникальным номером не найден. " + $"Попробуйте ввести номер от 0 до {_enclosures.Count - 1}");
    }
    
    return _enclosures[enclosureId];
  }

  public void AddEnclosure(Enclosure enclosure) {
    if (enclosure is null) {
      throw new ArgumentNullException(nameof(enclosure), "Добавляемый вольер должен существовать");
    }

    if (!_enclosures.TryAdd(enclosure.Id, enclosure)) {
      throw new ArgumentException($"Индекс {enclosure.Id} не является уникальным", nameof(enclosure));
    }
  }

  public void RemoveEnclosure(int enclosureId) {
    if (!_enclosures.ContainsKey(enclosureId)) {
      throw new ArgumentException("Вольера с таким уникальным номером не существует", nameof(enclosureId));
    }
    
    _enclosures.Remove(enclosureId);
    _enclosuresMexCounter.Add(enclosureId);
  }

  /// <summary>
  /// Returns next free id number that can be used to mark enclosure
  /// Be aware of its also pops it out from list of used id's(if it was in it)
  /// </summary>
  /// <returns> int - new id </returns>
  public int GetNextFreeEnclosureId() {
    if (_enclosuresMexCounter.Count == 0) {
      return _enclosures.Count;
    }
    
    int id = _enclosuresMexCounter.Min();
    _enclosuresMexCounter.Remove(id);
    
    return id;
  }
  
  public void UpdateStatus(int enclosureId, InhabitantsType newType) {
    if (!_enclosures.ContainsKey(enclosureId)) {
      throw new ArgumentException("Вольера с таким уникальным номером не существует", nameof(enclosureId));
    }
    
    _enclosures[enclosureId].ChangeCellInhabitantType(newType);
  }
}