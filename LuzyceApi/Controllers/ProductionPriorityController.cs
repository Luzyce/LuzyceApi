﻿using System.Text.Json;
using Luzyce.Core.Models.ProductionPriority;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[ApiController]
[Route("api/productionPriority")]
public class ProductionPriorityController(ProductionPriorityRepository productionPriorityRepository, LogRepository logRepository) : Controller
{
    private readonly ProductionPriorityRepository productionPriorityRepository = productionPriorityRepository;
    private readonly LogRepository logRepository = logRepository;
    
    [HttpPost("updatePriorities")]
    [Authorize]
    public IActionResult UpdatePriorities([FromBody] UpdateProductionPrioritiesRequest updateProductionPrioritiesRequest)
    {
        var status = productionPriorityRepository.UpdatePriorities(updateProductionPrioritiesRequest);
        
        if (status == 0)
        {
            logRepository.AddLog(User, "Nie udało się zaktualizować priorytetów - wystąpił błąd podczas aktualizacji", JsonSerializer.Serialize(updateProductionPrioritiesRequest));
            return Conflict();
        }

        logRepository.AddLog(User, "Zaktualizowano priorytety", JsonSerializer.Serialize(updateProductionPrioritiesRequest));

        return Ok();
    }
}