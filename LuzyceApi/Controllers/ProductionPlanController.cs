using Luzyce.Core.Models.ProductionPlan;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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
        var resp = productionPlanRepository.AddPositionsToProductionPlan(request);
        
        if (resp == 0)
        {
            return Conflict();
        }
           
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
    
    [HttpGet("kwit-{id:int}.pdf")]
    public IResult GetZlecenieProdPDF(int id)
    {
        // var prodOrder = productionPlanRepository.GetProductionOrder(id);
        
        // if (prodOrder == null)
        // {
        //     return Results.File(Array.Empty<byte>(), "application/pdf");
        // }
        //
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                // page.Header()
                //     .Text(prodOrder.Number)
                //     .SemiBold().FontSize(36);

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
}