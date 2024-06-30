using Luzyce.Core.Models.Lampshade;
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
        return Ok(new GetVariantsResponseDto
        {
            Variants = lampshadeRepository.GetLampshadeVariants().Select(x => new GetVariantResponseDto
                {
                    Id = x.Id,
                    ShortName = x.ShortName,
                    Name = x.Name
                }).ToList()
        });
    }
    
    [HttpGet("variants/{shortName}")]
    [Authorize]
    public IActionResult GetLampshadeVariant(string shortName)
    {
        var variant = lampshadeRepository.GetLampshadeVariant(shortName);
        
        if (variant == null)
        {
            return NotFound();
        }
        
        return Ok(new GetVariantResponseDto()
        {
            Id = variant.Id,
            Name = variant.Name,
            ShortName = variant.ShortName
        });
    }
}

