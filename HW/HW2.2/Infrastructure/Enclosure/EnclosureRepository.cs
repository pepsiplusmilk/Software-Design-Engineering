using Domain.Enclosure;
using Domain.Animal;

namespace Infrastructure.Enclosure;

public class EnclosureRepository : IEnclosureRepository {
  private readonly Dictionary<Guid, AbstractEnclosure> _enclosures = new();
  
  public Task<IEnumerable<AbstractEnclosure>> GetListOfEnclosures() {
    return Task.FromResult(_enclosures.Values.AsEnumerable());
  }

  public Task<AbstractEnclosure?> GetEnclosureById(Guid id) {
    if (!_enclosures.ContainsKey(id)) {
      return Task.FromResult<AbstractEnclosure?>(null);
    }
    
    return Task.FromResult(_enclosures?[id]);
  }

  public Task<IEnumerable<AbstractEnclosure>> FilterEnclosuresByType(System.Type enclosureType) {
    return Task.FromResult(from enclosure in _enclosures 
                           where enclosure.Value.GetType() == enclosureType
                             select enclosure.Value);
  }

  public Task<IEnumerable<AbstractEnclosure>> GetFreeEnclosures() {
    return Task.FromResult(from enclosure in _enclosures
                           where enclosure.Value.CurrentAnimalsInside < enclosure.Value.State.MaxAnimalsCount
                             select enclosure.Value);
  }

  public Task<int> GetRepositorySize() {
    return Task.FromResult(_enclosures.Count);
  }

  public Task RegisterNewEnclosure(AbstractEnclosure enclosure) {
    _enclosures[enclosure.Id] = enclosure;
    return Task.CompletedTask;
  }

  public Task<bool> UnregisterEnclosure(Guid enclosureId) {
    if (!IsEnclosureExists(enclosureId).Result) {
      return Task.FromResult(false);
    }
    
    _enclosures.Remove(enclosureId);
    return Task.FromResult(true);
  }

  public Task<bool> IsEnclosureExists(Guid enclosureId) {
    return Task.FromResult(_enclosures.ContainsKey(enclosureId));
  }

  /// <summary>
  /// This method updates count of currently suited animals "enclosureId" enclosure.
  /// </summary>
  /// <param name="enclosureId"> Id of enclosure </param>
  /// <returns> If enclosureId is correct and count isn't equal to maxSize then returned True otherwise False </returns>
  public Task<bool> IncreaseAnimalsCount(Guid enclosureId) {
    if (!IsEnclosureExists(enclosureId).Result) {
      Console.WriteLine($"{enclosureId}");
      return Task.FromResult(false);
    }

    if (_enclosures[enclosureId].CurrentAnimalsInside == _enclosures[enclosureId].State.MaxAnimalsCount) {
      return Task.FromResult(false);
    }
    
    ++_enclosures[enclosureId].CurrentAnimalsInside;
    return Task.FromResult(true);
  }

  /// <summary>
  /// This method updates count of currently suited animals "enclosureId" enclosure.
  /// </summary>
  /// <param name="enclosureId"> Id of enclosure </param>
  /// <returns> If enclosureId is correct and count isn't equal to zero then returned True otherwise False </returns>
  public Task<bool> DecreaseAnimalsCount(Guid enclosureId) {
    if (!IsEnclosureExists(enclosureId).Result) {
      return Task.FromResult(false);
    }

    // Counter is too low to decrement
    if (_enclosures[enclosureId].CurrentAnimalsInside == 0) {
      return Task.FromResult(false);
    }
    
    --_enclosures[enclosureId].CurrentAnimalsInside;
    return Task.FromResult(true);
  }

  public Task<bool> ChangeEnclosureAnimalType(AnimalTypes newAnimalType, Guid enclosureId) {
    if (!IsEnclosureExists(enclosureId).Result) {
      return Task.FromResult(false);
    }

    if (!Enum.IsDefined(typeof(AnimalTypes), newAnimalType)) {
      return Task.FromResult(false);
    }
    
    _enclosures[enclosureId].CurrentAnimalType = newAnimalType;
    
    return Task.FromResult(true);
  }
}