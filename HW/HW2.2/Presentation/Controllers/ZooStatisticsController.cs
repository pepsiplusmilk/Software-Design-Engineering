using Application.ReportSaving;
using Application.ZooStatistics;
using Domain.Animal;
using Domain.Enclosure;
using Microsoft.AspNetCore.Mvc;
using MoscowZooDDD.DTO;

namespace MoscowZooDDD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZooStatisticsController : ControllerBase {

  private readonly IZooReporterService _zooReporterService;
  private readonly IReportSaverService _reportSaverService;

  public ZooStatisticsController(IZooReporterService zooReporterService, IReportSaverService reportSaverService) {
    _zooReporterService = zooReporterService;
    _reportSaverService = reportSaverService;
  }
  
  [HttpGet("/stat/animals/total_count")]
  public async Task<int> ShowNumberOfAnimals() {
    var res = await _zooReporterService.GetSizeOfAnimalsRepository();
    return res;
  }

  [HttpGet("/stat/animals/health_state={thisHealthState}")]
  public async Task<IEnumerable<AnimalWithTypeName>> ShowListOfAnimalsWithHealthState(HealthState thisHealthState) {
    var res = await _zooReporterService.GetListOfAnimalsWithHealthStatus(thisHealthState);
    return (from animal in res
            select AnimalWithTypeName.Map(animal));
  }

  [HttpGet("/stat/enclosures/total_count")]
  public async Task<int> ShowNumberOfEnclosures() {
    var res = await _zooReporterService.GetSizeOfEnclosuresRepository();
    return res;
  }

  [HttpGet("/stat/enclosures/free_enclosures")]
  public async Task<IEnumerable<EnclosureWithTypeName>> ShowFreeEnclosures() {
    var res = await _zooReporterService.GetListOfFreeEnclosures();
    return (from enclosure in res
            select EnclosureWithTypeName.Map(enclosure));
  }
  
  [HttpGet("/stat/feeding/completed")]
  public async Task<ActionResult> ExportCompletedTasksReport() {
    var res = await _reportSaverService.SaveFeedingsReport();
    return res ? Ok() : BadRequest();
  }

  [HttpGet("/stat/animals/moved")]
  public async Task<ActionResult> ExportCompletedMovementsReport() {
    var res = await _reportSaverService.SaveMovementsReport();
    return res ? Ok() : BadRequest();
  }

}