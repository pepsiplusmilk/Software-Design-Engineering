using Domain.Enclosure;
using Domain.Animal;
namespace Application.EnclosureServices;

public interface IEnclosureRegisterService {
  public Task<bool> AddEnclosure(int maxAnimalsCount, EnclosureTypes enclosureType);
  public Task<bool> DeleteEnclosure(Guid enclosureId);
  
  public Task<AbstractEnclosure?> GetEnclosureById(Guid enclosureId);
  public Task<IEnumerable<AbstractEnclosure>> GetAllEnclosures();
  
  public Task<bool> IsEnclosureExist(Guid enclosureId);
}