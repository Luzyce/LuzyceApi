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
    
    [HttpGet("variants/{shortName}")]
    public IActionResult GetLampshadeVariant(string shortName)
    {
        return Ok(lampshadeRepository.GetLampshadeVariant(shortName));
    }
    
    [HttpGet("dekors")]
    public IActionResult GetLampshadeDekors()
    {
        return Ok(lampshadeRepository.GetLampshadeDekors());
    }
    
    [HttpGet("dekors/{shortName}")]
    public IActionResult GetLampshadeDekor(string shortName)
    {
        return Ok(lampshadeRepository.GetLampshadeDekor(shortName));
    }
}

