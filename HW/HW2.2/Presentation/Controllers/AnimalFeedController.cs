using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Application.FeedingService;
using Domain.Feeding;

namespace MoscowZooDDD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalFeedController : ControllerBase {

  private IAnimalFeedingService _animalFeedingService;

  public AnimalFeedController(IAnimalFeedingService animalFeedingService) {
    _animalFeedingService = animalFeedingService;
  }

  [HttpPatch("/feeding/{feedingTaskId}/complete")]
  public async Task<ActionResult> CompleteFeedingTask(Guid feedingTaskId) {
    var res = await _animalFeedingService.CompleteFeedingTask(feedingTaskId);
    return res ? Ok() : NotFound();
  }

  [HttpGet("/feeding/{feedingTaskId}")]
  public async Task<ActionResult<Feeding>> GetFeedingTaskById(Guid feedingTaskId) {
    var res = await _animalFeedingService.GetTaskById(feedingTaskId);
    return res == null ? Ok() : NotFound();
  }

  [HttpGet("/feeding/all")]
  public async Task<ActionResult<IEnumerable<Feeding>>> GetFeedingTimeTable() {
    var res = await _animalFeedingService.GetFeedingTimeTable();
    return Ok(res);
  }

  [HttpPost("/feeding/new")]
  public async Task<ActionResult<bool>> AddFeedingTask(
    [FromQuery] Guid animalId,
    [FromQuery] string foodDescription,
    [FromQuery] string optimalTimeRaw) {
    var res = await _animalFeedingService.AddFeedingTask(animalId, foodDescription, optimalTimeRaw);
    return res ? Ok() : BadRequest();
  }

  [HttpDelete("/feeding/erase={feedingTaskId}")]
  public async Task<ActionResult<bool>> DeleteFeedingTask(Guid feedingTaskId) {
    var res = await _animalFeedingService.RemoveFeedingTask(feedingTaskId);
    return res ? Ok() : NotFound();
  }

  [HttpPatch("/feeding/{feedingTaskId}/update_time")]
  public async Task<ActionResult<bool>> ChangeFeedingTaskTime(Guid feedingTaskId, string rawTime) {
    var res = await _animalFeedingService.ChangeTaskOptimalTime(feedingTaskId, rawTime);
    return res ? Ok() : BadRequest();
  }

  [HttpPatch("/feeding/all/reset")]
  public async Task<ActionResult<bool>> ResetDailyFeedingTasks() {
    var res = await _animalFeedingService.SetNewFeedingDay();
    return res ? Ok() : BadRequest();
  }
}