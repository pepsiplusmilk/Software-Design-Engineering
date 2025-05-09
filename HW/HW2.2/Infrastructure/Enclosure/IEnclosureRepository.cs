using Domain.Animal;

namespace Infrastructure.Enclosure;

using Domain.Enclosure;

public interface IEnclosureRepository {
  public Task<IEnumerable<AbstractEnclosure>> GetListOfEnclosures();
  public Task<AbstractEnclosure?> GetEnclosureById(Guid id);
  
  public Task<IEnumerable<AbstractEnclosure>> FilterEnclosuresByType(System.Type enclosureType);
  public Task<IEnumerable<AbstractEnclosure>> GetFreeEnclosures();
  public Task<int> GetRepositorySize();
  
  public Task RegisterNewEnclosure(AbstractEnclosure enclosure);
  public Task<bool> UnregisterEnclosure(Guid enclosureId);
  
  public Task<bool> IsEnclosureExists(Guid enclosureId);

  public Task<bool> IncreaseAnimalsCount(Guid enclosureId);
  public Task<bool> DecreaseAnimalsCount(Guid enclosureId);

  public Task<bool> ChangeEnclosureAnimalType(AnimalTypes newAnimalType, Guid enclosureId);
}