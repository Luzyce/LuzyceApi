using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[ApiController]
[Route("api/lampshade")]
public class LampshadeController(LampshadeRepository lampshadeRepository) : Controller
{
    private readonly LampshadeRepository lampshadeRepository = lampshadeRepository;
    
    [HttpGet("variants")]
    [Authorize]
    public IActionResult GetLampshadeVariants()
    {
        return Ok(lampshadeRepository.GetLampshadeVariants());
    }
    
    [HttpGet("variants/{shortName}")]
    [Authorize]
    public IActionResult GetLampshadeVariant(string shortName)
    {
        return Ok(lampshadeRepository.GetLampshadeVariant(shortName));
    }
    
    [HttpGet("dekors")]
    [Authorize]
    public IActionResult GetLampshadeDekors()
    {
        return Ok(lampshadeRepository.GetLampshadeDekors());
    }
    
    [HttpGet("dekors/{shortName}")]
    [Authorize]
    public IActionResult GetLampshadeDekor(string shortName)
    {
        return Ok(lampshadeRepository.GetLampshadeDekor(shortName));
    }
}

