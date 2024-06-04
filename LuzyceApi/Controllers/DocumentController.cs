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
            documentPosition = documentRepository.GetDocumentPositions(document.Id).Select(x => new
            {
                x.Id,
                x.QuantityNetto,
                x.QuantityLoss,
                x.QuantityToImprove,
                x.QuantityGross,
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
        if (documentRepository.IsDocumentLocked(document.Id) == null)
        {
            return Conflict();
        }

        documentRepository.LockDocument(document.Id, HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0");

        return Ok(new
        {
            document.Id,
            document.Number,
            documentPosition = documentRepository.GetDocumentPositions(document.Id).Select(x => new
            {
                x.Id,
                x.QuantityNetto,
                x.QuantityLoss,
                x.QuantityToImprove
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
        documentPosition.QuantityNetto = dto.QuantityNetto;
        documentPosition.QuantityLoss = dto.QuantityLoss;
        documentPosition.QuantityToImprove = dto.QuantityToImprove;
        documentPosition.QuantityGross = dto.QuantityGross;
        documentRepository.UpdateDocumentPosition(documentPosition);
        return Ok(new
        {
            documentPosition = documentRepository.GetDocumentPosition(id)
        });
    }

    [HttpPut("updateDocumentPositionOnKwit/{id}")]
    [Authorize]
    public IActionResult UpdateDocumentPositionOnKwit(int id, [FromBody] UpdateDocumentPositionOnKwitDto dto)
    {
        var document = documentRepository.GetDocument(id);
        if (document == null || document.DocumentsDefinition == null || document.DocumentsDefinition.Code != "KW" || (dto.type.Equals("+") && dto.type.Equals("-")))
        {
            return BadRequest("Invalid request");
        }
        if (documentRepository.IsDocumentLocked(id) != HttpContext.Connection.RemoteIpAddress?.ToString())
        {
            return BadRequest("Document is locked by another user");
        }

        var documentPosition = documentRepository.GetDocumentPositions(id).FirstOrDefault();
        var documentPositionBefore = documentRepository.GetDocumentPositions(id).FirstOrDefault();

        if (documentPosition == null)
        {
            return NotFound();
        }

        if (dto.field == "Dobrych")
        {
            if (documentPosition.QuantityNetto == 0 && dto.type == '-')
            {
                return BadRequest("NetQuantity is 0");
            }
            documentPosition.QuantityNetto = dto.type == '+' ? documentPosition.QuantityNetto + 1 : documentPosition.QuantityNetto - 1;
        }
        else if (dto.field == "DoPoprawy")
        {
            if (documentPosition.QuantityToImprove == 0 && dto.type == '-')
            {
                return BadRequest("QuantityToImprove is 0");
            }
            documentPosition.QuantityToImprove = dto.type == '+' ? documentPosition.QuantityToImprove + 1 : documentPosition.QuantityToImprove - 1;
        }
        else if (dto.field == "Zlych")
        {
            if (documentPosition.QuantityLoss == 0 && dto.type == '-')
            {
                return BadRequest("QuantityLoss is 0");
            }
            if ((dto.errorCode == null || documentRepository.GetError(dto.errorCode ?? "0") == null) && dto.type == '+')
            {
                return BadRequest(dto.errorCode);
            }

            documentPosition.QuantityLoss = dto.type == '+' ? documentPosition.QuantityLoss + 1 : documentPosition.QuantityLoss - 1;
        }
        else
        {
            return BadRequest("Invalid field");
        }

        documentPosition.QuantityGross = documentPosition.QuantityNetto + documentPosition.QuantityLoss + documentPosition.QuantityToImprove;

        var newOperation = new Domain.Models.Operation
        {
            DocumentId = id,
            Operator = document.Operator,
            OperatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"),
            QuantityNetDelta = documentPosition.QuantityNetto - (documentPositionBefore?.QuantityNetto ?? 0),
            QuantityLossDelta = documentPosition.QuantityLoss - (documentPositionBefore?.QuantityLoss ?? 0),
            QuantityToImproveDelta = documentPosition.QuantityToImprove - (documentPositionBefore?.QuantityToImprove ?? 0),
            ErrorCodeId = documentRepository.GetError(dto.errorCode ?? "0")?.Id
        };

        documentRepository.AddOperation(newOperation);
        documentRepository.UpdateDocumentPosition(documentPosition);

        return Ok(new
        {
            documentPosition.Id,
            documentPosition.QuantityNetto,
            documentPosition.QuantityLoss,
            documentPosition.QuantityToImprove,
            documentPosition.QuantityGross
        });
    }

    [HttpGet("closeDocument/{id}")]
    [Authorize]
    public IActionResult CloseDocument(int id)
    {
        if (documentRepository.IsDocumentLocked(id) == null)
        {
            return BadRequest("Document is not locked");
        }
        if (documentRepository.IsDocumentLocked(id) != HttpContext.Connection.RemoteIpAddress?.ToString())
        {
            return BadRequest("Document is locked by another user");
        }

        documentRepository.UnlockDocument(id);
        return Ok();
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(HttpContext.Request.Headers);
    }
}
