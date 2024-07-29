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
}