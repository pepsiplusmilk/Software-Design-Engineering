using Microsoft.AspNetCore.Mvc;
using MoscowZooDDD.Entities.Animal;
using MoscowZooDDD.Infrastructure.Repo;
namespace MoscowZooDDD.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalController : ControllerBase {
  private readonly IAnimalRepository _repository;

  public AnimalController(IAnimalRepository repository) {
    _repository = repository;
  }

  [HttpPost]
  public IActionResult AddAnimal() {
    
  }
}