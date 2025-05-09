using Application.Event;
using Domain.Animal;
using Domain.Enclosure;
using Domain.Event;
using Infrastructure.Animal;
using Infrastructure.Enclosure;

namespace Application.AnimalServices;

public class AnimalRegisterService : IAnimalRegisterService {
  private readonly IAnimalRepository _animalRepository;
  private readonly IEnclosureRepository _enclosureRepository;
  private readonly IDomainEventService _domainEventService;
  
  private readonly PredatorFactory _predatorFactory = new();
  private readonly HerbivoreFactory _herbivoreFactory = new();

  public AnimalRegisterService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository,
    IDomainEventService domainEventService) {
    _animalRepository = animalRepository;
    _enclosureRepository = enclosureRepository;
    _domainEventService = domainEventService;
  }
  
  public Task<bool> AddAnimal(Guid enclosureId, string nickName, string rawFormatDate, AnimalTypes animalType, string preferredFood,
    AnimalGender gender, string animalSpecie, EnclosureTypes preferredEnclosureType) {
    Animal? newAnimal;

    DateOnly birthDate;
    
    if (!DateOnly.TryParse(rawFormatDate, out birthDate)) {
      return Task.FromResult(false);  
    }

    // If someone tries to create "neutral" animal
    if (animalType == AnimalTypes.Neutral) {
      return Task.FromResult(false);
    }
    
    if (animalType == AnimalTypes.Herbivore) {
      newAnimal = _herbivoreFactory.Create(enclosureId, nickName, birthDate,
        preferredFood, gender, animalSpecie, preferredEnclosureType);
    }
    else {
      newAnimal = _predatorFactory.Create(enclosureId, nickName, birthDate,
        preferredFood, gender, animalSpecie, preferredEnclosureType);
    }
    
    // Checking if animal cannot be created with this args
    if (newAnimal is null) {
      return Task.FromResult(false);
    }

    // Checking if proposed enclosure exists 
    if (!_enclosureRepository.IsEnclosureExists(enclosureId).Result) {
      return Task.FromResult(false);
    }

    // Getting enclosure object
    var suggestedEnclosure = _enclosureRepository.GetEnclosureById(enclosureId).Result!;
    
    // If types correct than proceeds working
    if (suggestedEnclosure.CurrentAnimalType != animalType) {
      if (suggestedEnclosure.CurrentAnimalType != AnimalTypes.Neutral) {
        return Task.FromResult(false);
      }
    }
    
    // If enclosure type dont match preferred type of animal
    if (Enum.GetName(preferredEnclosureType) != suggestedEnclosure.GetType().Name) {
      return Task.FromResult(false);
    }
    
    // Increasing animal count 
    var response = _enclosureRepository.IncreaseAnimalsCount(newAnimal!.EnclosureId);

    // If enclosure is full
    if (!response.Result) {
      return Task.FromResult(false);
    }
    
    // Adding animal in repo
    _animalRepository.RegisterNewAnimal(newAnimal);

    // Locking type of animals stored here if enclosure was empty
    if (suggestedEnclosure.CurrentAnimalType == AnimalTypes.Neutral) {
      _enclosureRepository.ChangeEnclosureAnimalType(animalType, newAnimal.EnclosureId);
    }

    return Task.FromResult(true);
  }

  public Task<bool> MoveToAnotherEnclosure(Guid animalId, Guid enclosureId) {
    // Checking if this animal exists
    if (_animalRepository.GetAnimalById(animalId).Result == null) {
      return Task.FromResult(false);
    }

    // Checking if new enclosure is exists
    if (!_enclosureRepository.IsEnclosureExists(enclosureId).Result) {
      return Task.FromResult(false);
    }

    var newEnclosure = _enclosureRepository.GetEnclosureById(enclosureId).Result!;
    var animal = _animalRepository.GetAnimalById(animalId).Result!;
    
    // Checking - if enclosure is full
    if (newEnclosure.CurrentAnimalsInside == newEnclosure.State.MaxAnimalsCount) {
      return Task.FromResult(false);
    }

    // If types of enclosure and animal don't meets
    if (Enum.GetName(newEnclosure.CurrentAnimalType) != animal.GetType().Name) {
      // If enclosure isn't empty now - than its bad
      if (newEnclosure.CurrentAnimalType != AnimalTypes.Neutral) {
        return Task.FromResult(false);
      }
    }

    // Checking if preferred type of enclosure is match with new one
    if (Enum.GetName(animal.RequiredEnclosureType) != newEnclosure.GetType().Name) {
      return Task.FromResult(false);
    }
    
    // Now all invariants are correct

    var previousEnclosureId = animal.EnclosureId;
    
    // Change state of old enclosure if necessary
    _enclosureRepository.DecreaseAnimalsCount(animal.EnclosureId);
    if (_enclosureRepository.GetEnclosureById(animal.EnclosureId).Result!.CurrentAnimalsInside == 0) {
      _enclosureRepository.ChangeEnclosureAnimalType(AnimalTypes.Neutral, animal.EnclosureId);
    }
    
    _animalRepository.GetAnimalById(animalId).Result!.EnclosureId = enclosureId;
    
    // Change state of new enclosure if necessary
    _enclosureRepository.IncreaseAnimalsCount(enclosureId);
    if (_enclosureRepository.GetEnclosureById(enclosureId).Result!.CurrentAnimalType == AnimalTypes.Neutral) {
      _enclosureRepository.ChangeEnclosureAnimalType(Enum.Parse<AnimalTypes>(animal.GetType().Name), enclosureId);
    }
    
    // Now if we good we can raise event 
    _domainEventService.RaiseEvent(new AnimalMovedEvent(DateTime.Now, previousEnclosureId, enclosureId, animalId));
    return Task.FromResult(true);
  }
  
  public Task<bool> DeleteAnimal(Guid animalId) {
    var animal = _animalRepository.GetAnimalById(animalId);
    
    // If animal with <animalId> doesn't exist's then stops working
    if (animal.Result is null) {
      return Task.FromResult(false);
    }
    
    // If enclosure doesn't exists breaks
    if (!_enclosureRepository.IsEnclosureExists(animal.Result!.EnclosureId).Result) {
      return Task.FromResult(false);
    }
    
    // Decreasing count of animals in enclosure
    var response = _enclosureRepository.DecreaseAnimalsCount(animal.Result!.EnclosureId);

    // If enclosure is empty
    if (!response.Result) {
      return Task.FromResult(false);
    }
    
    // Poping out from repo
    _animalRepository.UnregisterAnimal(animalId);

    // If now enclosure is empty
    if (_enclosureRepository.GetEnclosureById(animal.Result!.EnclosureId)!.Result!.CurrentAnimalsInside == 0) {
      _enclosureRepository.ChangeEnclosureAnimalType(AnimalTypes.Neutral, animal.Result!.EnclosureId);
    }
    
    return Task.FromResult(true);
  }

  public Task<Animal?> GetAnimalById(Guid animalId) {
    return Task.FromResult(_animalRepository.GetAnimalById(animalId).Result);
  }

  public Task<IEnumerable<Animal>> GetAllAnimals() {
    return Task.FromResult(_animalRepository.GetListOfAnimals().Result);
  }
}