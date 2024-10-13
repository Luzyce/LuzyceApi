using Luzyce.Core.Models.DocumentDependencyChart;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[ApiController]
[Route("api/documentDependencyChart")]
public class DocumentDependencyChart(DocumentDependencyChartRepository documentDependencyChartRepository) : Controller
{
    [HttpPost]
    [Authorize]
    public IActionResult Get([FromBody] GetDocumentDependencyChartRequest getDocumentDependencyChartRequest)
    {
        var documentDependencyChart = documentDependencyChartRepository.GetDocumentDependencyChart(getDocumentDependencyChartRequest);
        return Ok(documentDependencyChart);
    }
}