using LuzyceApi.Dtos.Document;
using LuzyceApi.Repositories;
using LuzyceApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LuzyceApi.Domain.Models;

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
            x.DocNumber,
            x.Warehouse,
            x.Year,
            x.Number,
            x.DocumentsDefinition,
            Operator = x.Operator != null ? new { x.Operator.Id, x.Operator.Name, x.Operator.LastName } : null,
            x.CreatedAt,
            x.UpdatedAt,
            x.ClosedAt,
            x.Status
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
            document.DocNumber,
            document.Warehouse,
            document.Year,
            document.Number,
            document.DocumentsDefinition,
            Operator = document.Operator != null ? new { document.Operator.Id, document.Operator.Name, document.Operator.LastName } : null,
            document.CreatedAt,
            document.UpdatedAt,
            document.ClosedAt,
            document.Status,
            documentPositions = documentRepository.GetDocumentPositions(document.Id).Select(x => new
            {
                x.Id,
                x.NetQuantity,
                x.QuantityLoss,
                x.QuantityToImprove,
                x.GrossQuantity,
                Operator = x.Operator != null ? new { x.Operator.Id, x.Operator.Name, x.Operator.LastName } : null,
                x.StartTime,
                x.EndTime,
                Lampshade = x.Lampshade != null ? new { x.Lampshade.Id, x.Lampshade.Code } : null
            })
        });
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post([FromBody] CreateDocumentDto dto)
    {
        var newDocument = DocumentMappers.ToDocumentFromCreateDto(dto);
        newDocument.OperatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        newDocument = documentRepository.AddDocument(newDocument);

        if (newDocument != null)
        {
            return CreatedAtAction(nameof(Get), new { id = newDocument.Id }, documentRepository.GetDocument(newDocument.Id));
        }
        else
        {
            return BadRequest("Failed to create document.");
        }
    }

    [HttpPost("getByNumber")]
    [Authorize]
    public IActionResult GetByNumber([FromBody] GetDocumentByNumberDto dto)
    {
        var document = documentRepository.GetDocumentByNumber(dto.number);
        if (document == null)
        {
            return NotFound();
        }
        return Ok(new
        {
            document.Id,
            document.DocNumber,
            document.Warehouse,
            document.Year,
            document.Number,
            document.DocumentsDefinition,
            Operator = document.Operator != null ? new { document.Operator.Id, document.Operator.Name, document.Operator.LastName } : null,
            document.CreatedAt,
            document.UpdatedAt,
            document.ClosedAt,
            document.Status,
            documentPositions = documentRepository.GetDocumentPositions(document.Id).Select(x => new
            {
                x.Id,
                x.NetQuantity,
                x.QuantityLoss,
                x.QuantityToImprove,
                x.GrossQuantity,
                Operator = x.Operator != null ? new { x.Operator.Id, x.Operator.Name, x.Operator.LastName } : null,
                x.StartTime,
                x.EndTime,
                Lampshade = x.Lampshade != null ? new { x.Lampshade.Id, x.Lampshade.Code } : null
            })
        });
    }

    [HttpPut("changeStatus/{id}")]
    [Authorize]
    public IActionResult ChangeStatus(int id, [FromBody] ChangeDocumentStatusDto dto)
    {
        var document = documentRepository.GetDocument(id);
        if (document == null)
        {
            return NotFound();
        }
        document.StatusId = dto.StatusId;
        documentRepository.UpdateDocument(document);
        return Ok(documentRepository.GetDocument(id));
    }

    [HttpPost("addDocumentPosition/{id}")]
    [Authorize]
    public IActionResult AddDocumentPosition(int id, [FromBody] CreateDocumentPositionDto dto)
    {
        var document = documentRepository.GetDocument(id);
        if (document == null)
        {
            return NotFound();
        }
        var documentPosition = DocumentMappers.ToDocumentPositionFromCreateDto(dto);
        documentPosition.DocumentId = id;
        documentPosition.OperatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        documentPosition = documentRepository.AddDocumentPosition(documentPosition);
        if (documentPosition != null)
        {
            return CreatedAtAction(nameof(Get), new { id = documentPosition.Id }, documentRepository.GetDocumentPosition(documentPosition.Id));
        }
        else
        {
            return BadRequest("Failed to create document position.");
        }
    }

    [HttpPut("updateDocumentPosition/{id}")]
    [Authorize]
    public IActionResult UpdateDocumentPosition(int id, [FromBody] UpdateDocumentPositionDto dto)
    {
        var documentPosition = documentRepository.GetDocumentPosition(id);
        if (documentPosition == null)
        {
            return NotFound();
        }
        documentPosition.NetQuantity = dto.NetQuantity;
        documentPosition.QuantityLoss = dto.QuantityLoss;
        documentPosition.QuantityToImprove = dto.QuantityToImprove;
        documentPosition.GrossQuantity = dto.GrossQuantity;
        documentRepository.UpdateDocumentPosition(documentPosition);
        return Ok(documentRepository.GetDocumentPosition(id));
    }

    [HttpPut("updateDocumentPositionOnKwit/{id}")]
    [Authorize]
    public IActionResult UpdateDocumentPositionOnKwit(int id, [FromBody] UpdateDocumentPositionDto dto)
    {
        var document = documentRepository.GetDocument(id);
        if (document == null || document.DocumentsDefinition == null || document.DocumentsDefinition.Code != "KW")
        {
            return NotFound(document);
        }

        var documentPosition = documentRepository.GetDocumentPositions(id).FirstOrDefault();

        if (documentPosition != null)
        {
            documentPosition.NetQuantity = dto.NetQuantity;
            documentPosition.QuantityLoss = dto.QuantityLoss;
            documentPosition.QuantityToImprove = dto.QuantityToImprove;
            documentPosition.GrossQuantity = dto.GrossQuantity;
            documentPosition.EndTime = DateTime.Now;
            documentRepository.UpdateDocumentPosition(documentPosition);
        }

        return Ok(documentRepository.GetDocumentPosition(id));
    }
}
