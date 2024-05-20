using LuzyceApi.Dtos.Document;
using LuzyceApi.Repositories;
using LuzyceApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            Operator = x.Operator != null ? new { x.Operator.Id, x.Operator.Name, x.Operator.LastName } : null,
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
            Operator = document.Operator != null ? new { document.Operator.Id, document.Operator.Name, document.Operator.LastName } : null,
            document.CreatedAt,
            document.UpdatedAt,
            document.ClosedAt,
            document.Status,
            document.DocumentsDefinition
        });
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post([FromBody] CreateDocumentDto dto)
    {
        var newDocument = DocumentMappers.ToDocumentFromCreateDto(dto);
        newDocument.OperatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        newDocument = documentRepository.AddDocument(newDocument);
        return CreatedAtAction(nameof(Get), new { id = newDocument.Id }, documentRepository.GetDocument(newDocument.Id));
    }
}
