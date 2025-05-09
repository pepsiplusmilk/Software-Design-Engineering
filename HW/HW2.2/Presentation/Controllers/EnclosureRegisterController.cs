using Application.EnclosureServices;
using Microsoft.AspNetCore.Mvc;
using Domain.Enclosure;
using Domain.Animal;
using MoscowZooDDD.DTO;

namespace MoscowZooDDD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnclosureRegisterController : Controller {

  private readonly IEnclosureRegisterService _enclosureRegisterService;
  
  public EnclosureRegisterController(IEnclosureRegisterService enclosureRegisterService) {
    _enclosureRegisterService = enclosureRegisterService;
  }

  [HttpPost("enclosures/new")]
  public async Task<ActionResult> AddEnclosure(
    [FromQuery] int maxAnimalsCount, 
    [FromQuery] EnclosureTypes enclosureType) {
    var res = await _enclosureRegisterService.AddEnclosure(maxAnimalsCount, enclosureType);
    return res ? Created() : BadRequest();
  }

  [HttpGet("/enclosures/{enclosureId}")]
  public async Task<ActionResult<EnclosureWithTypeName>> GetEnclosureById(Guid enclosureId) {
    var res = await _enclosureRegisterService.GetEnclosureById(enclosureId);
    return res is null ? NotFound() : Ok(EnclosureWithTypeName.Map(res));
  }

  [HttpGet("/enclosures/all")]
  public async Task<IEnumerable<EnclosureWithTypeName>> GetAllEnclosures() {
    return (from enclosure in await _enclosureRegisterService.GetAllEnclosures()
            select EnclosureWithTypeName.Map(enclosure));
  }

  [HttpDelete("/enclosures/erase={enclosureId}")]
  public async Task<ActionResult> DeleteEnclosure(Guid enclosureId) {
    var res = await _enclosureRegisterService.DeleteEnclosure(enclosureId);
    return res ? Ok() : NotFound();
  }
}