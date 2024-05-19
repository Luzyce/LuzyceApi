using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;
[Route("api/document")]
[ApiController]
public class DocumentController(DocumentRepository documentRepository) : Controller
{
    private readonly DocumentRepository documentRepository = documentRepository;

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        var documents = documentRepository.GetDocuments().Select(x => new
        {
            x.Id,
            x.Number,
            x.Warehouse,
            x.Year,
            Operator = new { x.Operator.Id, x.Operator.Name, x.Operator.LastName },
            x.CreatedAt,
            x.UpdatedAt,
            x.ClosedAt,
            x.Status,
            x.DocumentsDefinition
        }
        );
        return Ok(documents);
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult Get(int id)
    {
        var document = documentRepository.GetDocument(id);
        if (document == null)
        {
            return NotFound();
        }
        return Ok(new
        {
            document.Id,
            document.Number,
            document.Warehouse,
            document.Year,
            Operator = new { document.Operator.Id, document.Operator.Name, document.Operator.LastName },
            document.CreatedAt,
            document.UpdatedAt,
            document.ClosedAt,
            document.Status,
            document.DocumentsDefinition
        });
    }
}
