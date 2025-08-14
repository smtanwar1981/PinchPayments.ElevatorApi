using Microsoft.AspNetCore.Mvc;
using PinchPayments.ElevatorApi.DTOs;
using PinchPayments.ElevatorApi.Services;

namespace PinchPayments.ElevatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ElevatorController(ElevatorService _elevatorService) : ControllerBase
{

    [HttpPost("getElevatorRoute")]
    public IActionResult GetElevatorRoute(int initialFloor, List<SummonRequest> requests)
    {
        var route = _elevatorService.GetElevatorRoute(initialFloor, requests);
        return Ok(route);
    }
}
