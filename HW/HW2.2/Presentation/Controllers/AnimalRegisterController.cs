using Microsoft.AspNetCore.Mvc;
using Application.AnimalServices;
using Domain.Animal;
using Domain.Enclosure;
using MoscowZooDDD.DTO;

namespace MoscowZooDDD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalRegisterController : ControllerBase {
  
  private readonly IAnimalRegisterService _animalRegisterService;

  public AnimalRegisterController(IAnimalRegisterService animalRegisterService) {
    _animalRegisterService = animalRegisterService;
  }

  [HttpPost("/animals/new")]
  public async Task<ActionResult> AddAnimal(
    [FromQuery] Guid enclosureId,
    [FromQuery] string nickName,
    [FromQuery] string birthDate,
    [FromQuery] AnimalTypes animalType,
    [FromQuery] string preferredFood,
    [FromQuery] AnimalGender gender,
    [FromQuery] string animalSpecie,
    [FromQuery] EnclosureTypes preferredEnclosureType) {
    var res = await _animalRegisterService.AddAnimal(enclosureId, nickName, birthDate, animalType, preferredFood, gender, animalSpecie,
      preferredEnclosureType);

    return res ? Created() : BadRequest();
  }

  [HttpGet("/animals/all")]
  public async Task<IEnumerable<AnimalWithTypeName>> GetAllAnimals() {
    return (from animal in await _animalRegisterService.GetAllAnimals()
            select AnimalWithTypeName.Map(animal));
  }

  [HttpGet("/animals/{animalId}")]
  public async Task<ActionResult<AnimalWithTypeName>> GetAnimalById(Guid animalId) {
    var result= await _animalRegisterService.GetAnimalById(animalId);
    return result is not null? Ok(AnimalWithTypeName.Map(result)) : NotFound();
  }

  [HttpDelete("/animals/erase={animalId}")]
  public async Task<ActionResult> DeleteAnimal(Guid animalId) {
    var result = await _animalRegisterService.DeleteAnimal(animalId);
    return result ? Ok() : NotFound();
  }

  [HttpPatch("/animals/{animalId}/move_to={newEnclosureId}")]
  public async Task<ActionResult> MoveAnimal(Guid animalId, Guid newEnclosureId) {
    var result = await _animalRegisterService.MoveToAnotherEnclosure(animalId, newEnclosureId);
    return result ? Ok(result) : BadRequest();
  }
}