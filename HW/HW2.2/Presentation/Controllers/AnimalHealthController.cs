using Domain.Animal;
using Microsoft.AspNetCore.Mvc;
using Application.AnimalServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MoscowZooDDD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalHealthController : ControllerBase {

  private readonly IAnimalHealthMonitoringService _animalHealthMonitoringService;

  public AnimalHealthController(IAnimalHealthMonitoringService animalHealthMonitoringService) {
    _animalHealthMonitoringService = animalHealthMonitoringService;
  }
  
  [HttpPatch("/animals/{animalId}/health")]
  public async Task<ActionResult> UpdateHealthState(
    [FromQuery] Guid animalId,
    [FromQuery] HealthState newHealthState) {
    var res = await _animalHealthMonitoringService.ChangeStatus(animalId, newHealthState);
    return res ? Ok() : BadRequest();
  }
}