using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[Route("api/[controller]")]
public class PlayerController : Controller
{
    [HttpGet("")]
    [HttpGet("home")]
    public IActionResult Index()
    {
        return Ok("Player Index");
    }
}
