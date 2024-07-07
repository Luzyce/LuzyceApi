using System.Security.Claims;
using Luzyce.Core.Models.ProductionOrder;
using LuzyceApi.Mappers;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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
    
    [HttpGet("zlecenieProdukcji-{id:int}.pdf")]
    public IResult GetZlecenieProdPDF(int id)
    {
        var prodOrder = productionOrderRepository.GetProductionOrder(id);
        
        if (prodOrder == null)
        {
            return Results.File(Array.Empty<byte>(), "application/pdf");
        }
        
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header()
                    .Text(prodOrder.Number)
                    .SemiBold().FontSize(36);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);

                        x.Item().Text(Placeholders.LoremIpsum());
                        x.Item().Image(Placeholders.Image(200, 100));
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        });
        
        var pdf = document.GeneratePdf();
        
        return Results.File(pdf, "application/pdf");
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