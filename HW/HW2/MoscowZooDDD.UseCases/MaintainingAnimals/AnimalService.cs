using MoscowZooDDD.Entities.Animal;
using MoscowZooDDD.UseCases.MaintainingEnclosures;
using MoscowZooDDD.Infrastructure.Repo;

namespace MoscowZooDDD.UseCases.MaintainingAnimals;

internal class AnimalService : IAnimalService {
  private readonly IAnimalRepository _animalRepository;
  private readonly IEnclosureService _enclosureService;

  public AnimalService(IAnimalRepository animalRepository, IEnclosureService enclosureService) {
    _animalRepository = animalRepository;
    _enclosureService = enclosureService;
  }
  
  public void TransferAnimal(int animalId, int destinationEnclosureId) {
    if (destinationEnclosureId < 0) {
      throw new ArgumentException("Нельзя переместить животное в вольер с отрицательным " +
                                  "идентификационным номером - таких не существует", nameof(destinationEnclosureId));
    }
    
    // On this state we ensured that data is correct
    Animal animal = _animalRepository.GetAnimal(animalId);
    
    RemoveAnimal(animalId);
    animal.MoveToAnotherEnclosure(destinationEnclosureId);
    AddAnimal(animal);
  }

  public void SetHealthStatus(int animalId, AnimalHealthState assignedState) {
    var animal = _animalRepository.GetAnimal(animalId);
    animal.ChangeHealthState(assignedState);
  }

  public void AddAnimal(Animal animal) {
    _animalRepository.RegisterNewAnimal(animal);
    _enclosureService.IncreaseAnimalPopulation(animal.EnclosureId, animal);
  }

  public void RemoveAnimal(int animalId) {
    _animalRepository.DischargeAnimal(animalId);
    _enclosureService.DecreaseAnimalPopulation(animalId);
  }
}