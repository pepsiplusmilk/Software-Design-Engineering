using Domain.Enclosure;
using Infrastructure.Enclosure;

namespace Application.EnclosureServices;

public class EnclosureRegisterService : IEnclosureRegisterService {
  private readonly IEnclosureRepository _enclosureRepository;
  private readonly CageFactory _cageFactory = new();
  private readonly EnclosureFactory _enclosureFactory = new();
  private readonly AquariumFactory _aquariumFactory = new();
  private readonly TerrariumFactory _terrariumFactory = new();

  public EnclosureRegisterService(IEnclosureRepository enclosureRepository) {
    _enclosureRepository = enclosureRepository;
  }
  
  public Task<bool> AddEnclosure(int maxAnimalsCount, EnclosureTypes enclosureType) {
    AbstractEnclosure? newEnclosure = enclosureType switch {
      EnclosureTypes.Cage => _cageFactory.CreateEnclosure(maxAnimalsCount),
      EnclosureTypes.Enclosure => _enclosureFactory.CreateEnclosure(maxAnimalsCount),
      EnclosureTypes.Aquarium => _aquariumFactory.CreateEnclosure(maxAnimalsCount),
      EnclosureTypes.Terrarium => _terrariumFactory.CreateEnclosure(maxAnimalsCount),
      _ => null
    };

    // If creating new exemplar isn't sucsessfull
    if (newEnclosure is null) {
      return Task.FromResult(false);
    }

    _enclosureRepository.RegisterNewEnclosure(newEnclosure);

    return Task.FromResult(true);
  }

  public Task<bool> DeleteEnclosure(Guid enclosureId) {
    // If id is invalid
    if (!IsEnclosureExist(enclosureId).Result) {
      return Task.FromResult(false);
    }
    
    var enclosure = _enclosureRepository.GetEnclosureById(enclosureId).Result;
    
    // If enclosure contains an animals it can't be destroyed
    if (enclosure!.CurrentAnimalsInside != 0) {
      return Task.FromResult(false);
    }

    _enclosureRepository.UnregisterEnclosure(enclosureId);
    return Task.FromResult(true);
  }

  public Task<AbstractEnclosure?> GetEnclosureById(Guid enclosureId) {
    return Task.FromResult(_enclosureRepository.GetEnclosureById(enclosureId).Result);
  }

  public Task<IEnumerable<AbstractEnclosure>> GetAllEnclosures() {
    return Task.FromResult(_enclosureRepository.GetListOfEnclosures().Result);
  }

  public Task<bool> IsEnclosureExist(Guid enclosureId) {
    return Task.FromResult(_enclosureRepository.IsEnclosureExists(enclosureId).Result);
  }
}