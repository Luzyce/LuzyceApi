using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;
[Route("api/document")]
[ApiController]
public class DocumentController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("DocumentController.Get");
    }
}
