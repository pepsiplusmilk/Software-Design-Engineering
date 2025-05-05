using MoscowZooDDD.Entities.Animal;
using MoscowZooDDD.Entities.Enclosure;

namespace MoscowZooDDD.UseCases.MaintainingEnclosures;
using MoscowZooDDD.Infrastructure.Repo;

public class EnclosureService : IEnclosureService {
  private readonly IEnclosureRepository _enclosureRepository;

  public EnclosureService(IEnclosureRepository enclosureRepository) {
    _enclosureRepository = enclosureRepository;
  }
  public void DecreaseAnimalPopulation(int enclosureId) {
    var enclosure = _enclosureRepository.GetEnclosure(enclosureId);

    if (enclosure.CurrentAnimalCount == 0) {
      throw new Exception("Вольер пуст, из него невозможно достать ни одного животного");
    }
    
    enclosure.DecrementCurrentAnimalCount();

    if (enclosure.CurrentAnimalCount == 0) {
      _enclosureRepository.UpdateStatus(enclosureId, InhabitantsType.Neutral);
    }
  }
  public void IncreaseAnimalPopulation(int enclosureId, Animal withAnimal) {
    var enclosure = _enclosureRepository.GetEnclosure(enclosureId);

    if (enclosure.CurrentAnimalCount == enclosure.Characteristics.MaxAnimalCount) {
      throw new Exception("Вольер заполнен, в него невозможно добавить ни одного животного");
    }

    if (enclosure.Characteristics.Type != withAnimal.Description.EnclosureType) {
      throw new Exception("Вольер не подходит по своему устройству для данного животного");
    }

    if ((withAnimal is Predator && enclosure.CurrentInhabitantType == InhabitantsType.Herbivores) ||
        (withAnimal is Herbivore && enclosure.CurrentInhabitantType == InhabitantsType.Predatory)) {
      throw new Exception("Вольер заполнен животными которым требуется другая поведенческая среда");
    }
    
    if (enclosure.CurrentInhabitantType == InhabitantsType.Neutral) {
      _enclosureRepository.UpdateStatus(enclosureId,
        withAnimal is Predator ? InhabitantsType.Predatory : InhabitantsType.Herbivores);
    }
    
    enclosure.IncrementCurrentAnimalCount();
  }

  public void AddEnclosure(Enclosure enclosure) {
    _enclosureRepository.AddEnclosure(enclosure);
  }

  public void RemoveEnclosure(int enclosureId) {
    _enclosureRepository.RemoveEnclosure(enclosureId);
  }
}