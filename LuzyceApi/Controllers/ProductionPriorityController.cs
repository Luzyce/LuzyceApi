using System.Security.Claims;
using Luzyce.Core.Models.ProductionPriority;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[ApiController]
[Route("api/productionPriority")]
public class ProductionPriorityController(ProductionPriorityRepository productionPriorityRepository) : Controller
{
    private readonly ProductionPriorityRepository productionPriorityRepository = productionPriorityRepository;
    
    [HttpPost("save")]
    [Authorize]
    public IActionResult SavePriorities([FromBody] CreateProductionPriorityRequest createProductionPriorityRequest)
    {
        var status = productionPriorityRepository.SavePriorities(createProductionPriorityRequest);
        
        if (status == 0)
        {
            return Conflict();
        }
        
        return Ok();
    }
}