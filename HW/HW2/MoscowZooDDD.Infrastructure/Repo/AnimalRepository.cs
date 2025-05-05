using MoscowZooDDD.Entities.Animal;

namespace MoscowZooDDD.Infrastructure.Repo;

public class AnimalRepository : IAnimalRepository {
  private readonly Dictionary<int, Animal> _animals = new();
  private readonly SortedSet<int> _animalsMexCounter = new();

  public Animal GetAnimal(int animalId) {
    if (animalId < 0 || animalId >= _animals.Count || _animalsMexCounter.Contains(animalId)) {
      throw new ArgumentOutOfRangeException(nameof(animalId), "Животное с таким уникальным номером не найдено. " +
                                                              $"Попробуйте ввести номер от 0 до {_animals.Count - 1}");
    }
    
    return _animals[animalId];
  }

  public void RegisterNewAnimal(Animal animal) {
    if (animal is null) {
      throw new ArgumentNullException(nameof(animal), "Для постановки на учет, животное должно существовать");
    }

    if (!_animals.TryAdd(animal.Id, animal)) {
      throw new ArgumentException($"Индекс {animal.Id} не является уникальным", nameof(animal));
    }
    
    _animalsMexCounter.Remove(animal.Id);
  }

  public void DischargeAnimal(int animalId) {
    if (!_animals.ContainsKey(animalId)) {
      throw new ArgumentException("Животного с таким уникальным номером не существует", nameof(animalId));
    }
    
    _animals.Remove(animalId);
    _animalsMexCounter.Add(animalId);
  }

  /// <summary>
  /// Returns next free id number that can be used to mark animal
  /// Be aware of its also pops it out from list of used id's(if it was in it)
  /// </summary>
  /// <returns> int - new id </returns>
  public int GetNextFreeAnimalId() {
    if (_animalsMexCounter.Count == 0) {
      return _animals.Count;
    }
    
    int result = _animalsMexCounter.Min();
    _animalsMexCounter.Remove(result);
    
    return result;
  }
}