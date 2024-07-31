using Luzyce.Core.Models.ProductionPlan;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[ApiController]
[Route("api/productionPlan")]
public class ProductionPlanController(ProductionPlanRepository productionPlanRepository) : Controller
{
    [HttpPost]
    [Authorize]
    public IActionResult GetProductionPlans(GetMonthProductionPlanRequest request)
    {
        return Ok(productionPlanRepository.GetProductionPlans(request));
    }
    
    [HttpPost("addPositions")]
    [Authorize]
    public IActionResult AddPositionsToProductionPlan(AddPositionsToProductionPlan request)
    {
        productionPlanRepository.AddPositionsToProductionPlan(request);
        return Ok();
    }
    
    [HttpPost("getProductionPlan")]
    [Authorize]
    public IActionResult GetProductionPlan(GetProductionPlanPositionsRequest request)
    {
        return Ok(productionPlanRepository.GetProductionPlan(request));
    }
    
    [HttpDelete("delPosition/{id:int}")]
    [Authorize]
    public IActionResult DeletePosition(int id)
    {
        productionPlanRepository.DeletePosition(id);
        return Ok();
    }
    
    [HttpGet("getShiftSupervisor")]
    [Authorize]
    public IActionResult getShiftSupervisor()
    {
        return Ok(productionPlanRepository.ShiftSupervisor());
    }
    
    [HttpGet("headsOfMetallurgicalTeams")]
    [Authorize]
    public IActionResult GetHeadsOfMetallurgicalTeams()
    {
        return Ok(productionPlanRepository.GetHeadsOfMetallurgicalTeams());
    }
    
    [HttpPut("updateProductionPlan")]
    [Authorize]
    public IActionResult UpdatePositions(UpdateProductionPlan request)
    {
        productionPlanRepository.UpdateProductionPlan(request);
        return Ok();
    }
}