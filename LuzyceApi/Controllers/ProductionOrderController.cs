using System.Security.Claims;
using Luzyce.Core.Models.ProductionOrder;
using LuzyceApi.Mappers;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[ApiController]
[Route("api/productionOrder")]
public class ProductionOrderController(ProductionOrderRepository productionOrderRepository) : Controller
{
    private readonly ProductionOrderRepository productionOrderRepository = productionOrderRepository;
    
    [HttpPost("new")]
    public IActionResult CreateProductionOrder(CreateProductionOrderDto createProductionOrderDto)
    {
        var operatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        
        if (operatorId == 0)
        {
            return Unauthorized();
        }
        
        var order = createProductionOrderDto.ToOrderFromCreateDto();
        
        var productionOrder = createProductionOrderDto.ToProductionOrderFromCreateDto();
        productionOrder.OperatorId = operatorId;
        
        var status = productionOrderRepository.SaveProductionOrder(order, productionOrder);
        
        if (status == 0)
        {
            return Conflict();
        }
        
        return Ok();
    }
}