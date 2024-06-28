using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[ApiController]
[Route("api/lampshade")]
public class LampshadeController(LampshadeRepository lampshadeRepository) : Controller
{
    private readonly LampshadeRepository lampshadeRepository = lampshadeRepository;
    
    [HttpGet("variants")]
    public IActionResult GetLampshadeVariants()
    {
        return Ok(lampshadeRepository.GetLampshadeVariants());
    }
    
    [HttpGet("variants/{name}")]
    public IActionResult GetLampshadeVariant(string name)
    {
        return Ok(lampshadeRepository.GetLampshadeVariant(name));
    }
}