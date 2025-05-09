using Domain.Animal;
using Domain.Enclosure;

namespace Application.AnimalServices;

public interface IAnimalRegisterService {
  public Task<bool> AddAnimal(Guid enclosureId, string nickName, string rawFormatDate, AnimalTypes animalType,
    string preferredFood, AnimalGender gender, string animalSpecie, EnclosureTypes preferredEnclosureType);
  public Task<bool> DeleteAnimal(Guid animalId);
  
  public Task<Animal?> GetAnimalById(Guid animalId);
  public Task<IEnumerable<Animal>> GetAllAnimals();
  public Task<bool> MoveToAnotherEnclosure(Guid animalId, Guid enclosureId);
}