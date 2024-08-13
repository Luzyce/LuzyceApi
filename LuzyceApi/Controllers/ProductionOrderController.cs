using System.Security.Claims;
using Luzyce.Core.Models.ProductionOrder;
using LuzyceApi.Mappers;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[ApiController]
[Route("api/productionOrder")]
public class ProductionOrderController(ProductionOrderRepository productionOrderRepository) : Controller
{
    private readonly ProductionOrderRepository productionOrderRepository = productionOrderRepository;
    
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok(productionOrderRepository.GetProductionOrders());
    }
    
    [HttpGet("{id:int}")]
    [Authorize]
    public IActionResult Get(int id)
    {
        return Ok(productionOrderRepository.GetProductionOrder(id));
    }
    
    [HttpGet("positions")]
    [Authorize]
    public IActionResult GetPositions()
    {
        return Ok(productionOrderRepository.GetPositions());
    }
    
    [HttpPost("new")]
    [Authorize]
    public IActionResult CreateProductionOrder(CreateProductionOrderRequest createProductionOrderDto)
    {
        var operatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        
        if (operatorId == 0)
        {
            return Unauthorized();
        }
        
        var order = createProductionOrderDto.ToOrderFromCreateDto();
        
        var productionOrder = createProductionOrderDto.ToProductionOrderFromCreateDto();
        productionOrder.OperatorId = operatorId;
        
        var status = productionOrderRepository.SaveProdOrder(order, productionOrder);
        
        if (status == 0)
        {
            return Conflict();
        }
        
        return Ok();
    }
    
    [HttpPost("update/{id:int}")]
    [Authorize]
    public IActionResult UpdateProductionOrder(int id, UpdateProductionOrder updateProductionOrderDto)
    {
        var resp = productionOrderRepository.UpdateProdOrder(id, updateProductionOrderDto);
        
        if (resp == 0)
        {
            return Conflict();
        }
        
        return Ok();
    }
}