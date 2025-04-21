using Microsoft.AspNetCore.Mvc;

[ApiController]
public class APIController : ControllerBase
{
    [HttpHead]
    [Route("api/ping")]
    public IActionResult Ping()
    {
        return Ok(); // Always returns 200
    }
}