using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[ApiController]
[Route("api/documentDependencyChart")]
public class DocumentDependencyChart(DocumentDependencyChartRepository documentDependencyChartRepository) : Controller
{
    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var documentDependencyChart = documentDependencyChartRepository.GetDocumentDependencyChart(id);
        return Ok(documentDependencyChart);
    }
}