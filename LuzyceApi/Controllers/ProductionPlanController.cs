﻿using Luzyce.Core.Models.ProductionPlan;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
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
    public IResult GetKwitPdf(int id)
    {
        var kwit = productionPlanRepository.GetKwit(id);

        if (kwit == null)
        {
            return Results.File(Array.Empty<byte>(), "application/pdf");
        }

        var url = $"http://localhost:5132/api/productionPlan/kwit-{id}.pdf";
        var qrSVGString = "";
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        using var qrSVG = new SvgQRCode(qrCodeData);
        qrSVGString = qrSVG.GetGraphic(100);

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header().Layers(layers =>
                {
                    layers.PrimaryLayer()
                        .TranslateX(-1, Unit.Centimetre)
                        .AlignRight()
                        .Width(2, Unit.Centimetre)
                        .Svg(qrSVGString);

                    layers.Layer()
                        .Text(kwit.Number)
                        .SemiBold()
                        .FontSize(16);
                });

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);

                        x.Item()
                            .Text(
                                $"Asortyment: {kwit.DocumentPositions[0].Lampshade?.Code} {kwit.DocumentPositions[0].LampshadeNorm?.Variant?.Name} {kwit.DocumentPositions[0].LampshadeDekor}")
                            .FontSize(16);
                        x.Item()
                            .Text(
                                $"Ilość: {kwit.ProductionPlanPositions?.Quantity} {kwit.DocumentPositions[0].OrderPositionForProduction?.Unit}")
                            .FontSize(16);
                    });
            });
        });

        var pdf = document.GeneratePdf();

        return Results.File(pdf, "application/pdf");
    }

    [HttpGet("productionPlan-{data}.pdf")]
    public IResult GetProductionPlanPdf(DateOnly data)
    {
        var productionPlans = productionPlanRepository.GetProductionPlanPdf(data);

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header()
                    .Text($"Plan produkcji na {data.ToString("d")}")
                    .SemiBold().FontSize(36);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(0.2f);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Padding(5).Text("").FontSize(16);
                                header.Cell().Element(CellStyle).Padding(5).Text("Zmiana 1").FontSize(16);
                                header.Cell().Element(CellStyle).Padding(5).Text("Zmiana 2").FontSize(16);
                                header.Cell().Element(CellStyle).Padding(5).Text("Zmiana 3").FontSize(16);
                            });

                            for (var x = 1; x <= 3; x++)
                            {
                                table.Cell().Element(CellStyle).Padding(5).AlignCenter().RotateLeft().Width(90).Text("Zespół " + x).AlignCenter().FontSize(16);;
                                
                                for (var y = 1; y <= 3; y++)
                                {
                                    var plan = productionPlans
                                        .Find(p => p.Team == x && p.Change == y);

                                    if (plan != null)
                                    {
                                        var cellText = "";
                                        for (var i = 0; i < plan.Positions.Count; i++)
                                        {
                                            if (i != 0)
                                            {
                                                cellText += "\n";
                                            }

                                            var position = plan.Positions[i];
                                            cellText += $"{i + 1}. " +
                                                        $"{position.Kwit.First().DocumentPositions.First().Lampshade!.Code} " +
                                                        $"{position.Kwit.First().DocumentPositions.First().LampshadeNorm!.Variant!.Name} " +
                                                        $"{position.Kwit.First().DocumentPositions.First().LampshadeDekor}\n" +
                                                        $"Ilość: {position.Quantity}\n" +
                                                        $"Kwit: {position.Kwit.First().Number}\n";
                                        }

                                        table.Cell()
                                            .Element(CellStyle).Padding(5).Text(cellText).FontSize(11);
                                    }
                                    else
                                    {
                                        table.Cell().Element(CellStyle).Text("-").AlignCenter();
                                    }
                                }
                            }
                        });
                    });
            });
        });

        var pdf = document.GeneratePdf();

        return Results.File(pdf, "application/pdf");
    }

    IContainer CellStyle(IContainer container)
    {
        return container
            .Border(1)
            .BorderColor(Colors.Black);
    }
}