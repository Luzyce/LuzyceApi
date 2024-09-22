using Luzyce.Core.Models.Document;
using LuzyceApi.Repositories;
using LuzyceApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;
using Luzyce.Core.Models.User;
using LuzyceApi.Db.AppDb.Models;

namespace LuzyceApi.Controllers;
[Route("api/document")]
[ApiController]
public class DocumentController(DocumentRepository documentRepository, LogRepository logRepository) : Controller
{
    private readonly DocumentRepository documentRepository = documentRepository;
    private readonly LogRepository logRepository = logRepository;

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok(
            documentRepository
                .GetDocuments()
                .Select(x => new GetDocumentResponseDto
                {
                    Id = x.Id,
                    DocNumber = x.DocNumber,
                    Warehouse = x.Warehouse != null ? new GetWarehouseResponseDto
                    {
                        Id = x.Warehouse.Id,
                        Code = x.Warehouse.Code,
                        Name = x.Warehouse.Name
                    } : null,
                    Year = x.Year,
                    Number = x.Number ?? "",
                    DocumentsDefinition = x.DocumentsDefinition != null ? new GetDocumentsDefinitionResponseDto
                    {
                        Id = x.DocumentsDefinition.Id,
                        Code = x.DocumentsDefinition.Code,
                        Name = x.DocumentsDefinition.Name
                    } : null,
                    User = x.Operator != null ? new GetUserResponseDto
                    {
                        Id = x.Operator.Id,
                        Name = x.Operator.Name,
                        LastName = x.Operator.LastName,
                        Login = x.Operator.Login
                    } : null,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    ClosedAt = x.ClosedAt,
                    Status = x.Status != null ? new GetStatusResponseDto
                    {
                        Id = x.Status.Id,
                        Name = x.Status.Name,
                        Priority = x.Status.Priority
                    } : null
                })
                .ToList());
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

        return Ok(
            new GetDocumentResponseDto
            {
                Id = document.Id,
                DocNumber = document.DocNumber,
                Warehouse = document.Warehouse != null ? new GetWarehouseResponseDto
                {
                    Id = document.Warehouse.Id,
                    Code = document.Warehouse.Code,
                    Name = document.Warehouse.Name
                } : null,
                Year = document.Year,
                Number = document.Number ?? "",
                DocumentsDefinition = document.DocumentsDefinition != null ? new GetDocumentsDefinitionResponseDto
                {
                    Id = document.DocumentsDefinition.Id,
                    Code = document.DocumentsDefinition.Code,
                    Name = document.DocumentsDefinition.Name
                } : null,
                User = document.Operator != null ? new GetUserResponseDto
                {
                    Id = document.Operator.Id,
                    Name = document.Operator.Name,
                    LastName = document.Operator.LastName,
                    Login = document.Operator.Login
                } : null,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt,
                ClosedAt = document.ClosedAt,
                Status = document.Status != null ? new GetStatusResponseDto
                {
                    Id = document.Status.Id,
                    Name = document.Status.Name,
                    Priority = document.Status.Priority
                } : null
            });
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post([FromBody] CreateDocumentDto dto)
    {
        var newDocument = DocumentMappers.ToDocumentFromCreateDto(dto);
        newDocument.OperatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        newDocument = documentRepository.AddDocument(newDocument);

        var createdDocument = documentRepository.GetDocument(newDocument.Id);

        if (newDocument == null || createdDocument == null)
        {
            return BadRequest("Failed to create document.");
        }

        return CreatedAtAction(
            nameof(Get),
            new GetDocumentResponseDto
            {
                Id = createdDocument.Id,
                DocNumber = createdDocument.DocNumber,
                Warehouse = createdDocument.Warehouse != null ? new GetWarehouseResponseDto
                {
                    Id = createdDocument.Warehouse.Id,
                    Code = createdDocument.Warehouse.Code,
                    Name = createdDocument.Warehouse.Name
                } : null,
                Year = createdDocument.Year,
                Number = createdDocument.Number ?? "",
                DocumentsDefinition = createdDocument.DocumentsDefinition != null ? new GetDocumentsDefinitionResponseDto
                {
                    Id = createdDocument.DocumentsDefinition.Id,
                    Code = createdDocument.DocumentsDefinition.Code,
                    Name = createdDocument.DocumentsDefinition.Name
                } : null,
                User = createdDocument.Operator != null ? new GetUserResponseDto
                {
                    Id = createdDocument.Operator.Id,
                    Name = createdDocument.Operator.Name,
                    LastName = createdDocument.Operator.LastName,
                    Login = createdDocument.Operator.Login
                } : null,
                CreatedAt = createdDocument.CreatedAt,
                UpdatedAt = createdDocument.UpdatedAt,
                ClosedAt = createdDocument.ClosedAt,
                Status = createdDocument.Status != null ? new GetStatusResponseDto
                {
                    Id = createdDocument.Status.Id,
                    Name = createdDocument.Status.Name,
                    Priority = createdDocument.Status.Priority
                } : null
            });
    }

    [HttpPost("getDocumentByQrCode")]
    [Authorize]
    public IActionResult GetDocumentByQrCode([FromBody] GetDocumentByQrCodeDto dto)
    {
        var document = documentRepository
            .GetDocument(Int32.Parse(Regex.Match(dto.Number, "(kwit-(\\d+))").Groups[2].Value));

        if (document == null)
        {
            logRepository.AddLog(User, "Failed to get document by qr code - document not found", JsonSerializer.Serialize(dto));
            return NotFound();
        }
        if (documentRepository.IsDocumentLocked(document.Id))
        {
            logRepository.AddLog(User, "Failed to get document by qr code - document is locked", JsonSerializer.Serialize(dto));
            return Conflict();
        }

        documentRepository.LockDocument(document.Id, int.Parse(User.FindFirstValue(ClaimTypes.PrimarySid) ?? ""));

        logRepository.AddLog(User, "Get document by qr code", JsonSerializer.Serialize(dto));

        return Ok(new GetDocumentByQrCodeResponseDto
        {
            Id = document.Id,
            Number = document.Number,
            DocumentPositions = documentRepository.GetDocumentPositions(document.Id).Select(x => new GetDocumentPositionResponseDto
            {
                Id = x.Id,
                QuantityNetto = x.QuantityNetto,
                QuantityLoss = x.QuantityLoss,
                QuantityToImprove = x.QuantityToImprove
            }).ToList()
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
        return Ok(
            new GetDocumentResponseDto
            {
                Id = document.Id,
                DocNumber = document.DocNumber,
                Warehouse = document.Warehouse != null ? new GetWarehouseResponseDto
                {
                    Id = document.Warehouse.Id,
                    Code = document.Warehouse.Code,
                    Name = document.Warehouse.Name
                } : null,
                Year = document.Year,
                Number = document.Number ?? "",
                DocumentsDefinition = document.DocumentsDefinition != null ? new GetDocumentsDefinitionResponseDto
                {
                    Id = document.DocumentsDefinition.Id,
                    Code = document.DocumentsDefinition.Code,
                    Name = document.DocumentsDefinition.Name
                } : null,
                User = document.Operator != null ? new GetUserResponseDto
                {
                    Id = document.Operator.Id,
                    Name = document.Operator.Name,
                    LastName = document.Operator.LastName,
                    Login = document.Operator.Login
                } : null,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt,
                ClosedAt = document.ClosedAt,
                Status = document.Status != null ? new GetStatusResponseDto
                {
                    Id = document.Status.Id,
                    Name = document.Status.Name,
                    Priority = document.Status.Priority
                } : null
            });
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
        document = documentRepository.GetDocument(id);

        if (documentPosition == null || document == null)
        {
            return BadRequest("Failed to create document position.");
        }

        return CreatedAtAction(
            nameof(Get),
            new GetDocumentResponseDto
            {
                Id = document.Id,
                DocNumber = document.DocNumber,
                Warehouse = document.Warehouse != null ? new GetWarehouseResponseDto
                {
                    Id = document.Warehouse.Id,
                    Code = document.Warehouse.Code,
                    Name = document.Warehouse.Name
                } : null,
                Year = document.Year,
                Number = document.Number ?? "",
                DocumentsDefinition = document.DocumentsDefinition != null ? new GetDocumentsDefinitionResponseDto
                {
                    Id = document.DocumentsDefinition.Id,
                    Code = document.DocumentsDefinition.Code,
                    Name = document.DocumentsDefinition.Name
                } : null,
                User = document.Operator != null ? new GetUserResponseDto
                {
                    Id = document.Operator.Id,
                    Name = document.Operator.Name,
                    LastName = document.Operator.LastName,
                    Login = document.Operator.Login
                } : null,
                CreatedAt = document.CreatedAt,
                UpdatedAt = document.UpdatedAt,
                ClosedAt = document.ClosedAt,
                Status = document.Status != null ? new GetStatusResponseDto
                {
                    Id = document.Status.Id,
                    Name = document.Status.Name,
                    Priority = document.Status.Priority
                } : null
            });
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
        return Ok(new GetDocumentPositionResponseDto
        {
            Id = documentPosition.Id,
            QuantityNetto = documentPosition.QuantityNetto,
            QuantityLoss = documentPosition.QuantityLoss,
            QuantityToImprove = documentPosition.QuantityToImprove
        });
    }

    [HttpPut("updateDocumentPositionOnKwit/{id:int}")]
    [Authorize]
    public IActionResult UpdateDocumentPositionOnKwit(int id, [FromBody] UpdateDocumentPositionOnKwitDto dto)
    {
        var document = documentRepository.GetDocument(id);

        if (document?.DocumentsDefinition is not { Code: "KW" } || (dto.Type.Equals("+") && dto.Type.Equals("-")))
        {
            logRepository.AddLog(User, "Failed to update document position on kwit - invalid request", JsonSerializer.Serialize(dto));
            return BadRequest("Invalid request");
        }

        if (!documentRepository.IsDocumentLockedByUser(id, int.Parse(User.FindFirstValue(ClaimTypes.PrimarySid) ?? "")))
        {
            logRepository.AddLog(User, "Failed to update document position on kwit - document is not locked or locked by another user", JsonSerializer.Serialize(dto));
            return BadRequest("Document is locked by another user or not locked");
        }

        var documentPosition = documentRepository.GetDocumentPositions(id).FirstOrDefault();
        var documentPositionBefore = documentRepository.GetDocumentPositions(id).FirstOrDefault();

        if (documentPosition == null)
        {
            logRepository.AddLog(User, "Failed to update document position on kwit - document position not found", JsonSerializer.Serialize(dto));
            return NotFound();
        }

        if (dto.Field == "Dobrych")
        {
            if (documentPosition.QuantityNetto == 0 && dto.Type == '-')
            {
                return BadRequest("NetQuantity is 0");
            }
            documentPosition.QuantityNetto = dto.Type == '+' ? documentPosition.QuantityNetto + 1 : documentPosition.QuantityNetto - 1;
        }
        else if (dto.Field == "DoPoprawy")
        {
            if (documentPosition.QuantityToImprove == 0 && dto.Type == '-')
            {
                return BadRequest("QuantityToImprove is 0");
            }
            documentPosition.QuantityToImprove = dto.Type == '+' ? documentPosition.QuantityToImprove + 1 : documentPosition.QuantityToImprove - 1;
        }
        else if (dto.Field == "Zlych")
        {
            if (documentPosition.QuantityLoss == 0 && dto.Type == '-')
            {
                return BadRequest("QuantityLoss is 0");
            }
            if ((dto.ErrorCode == null || documentRepository.GetError(dto.ErrorCode ?? "0") == null) && dto.Type == '+')
            {
                return BadRequest(dto.ErrorCode);
            }

            documentPosition.QuantityLoss = dto.Type == '+' ? documentPosition.QuantityLoss + 1 : documentPosition.QuantityLoss - 1;
        }
        else
        {
            logRepository.AddLog(User, "Failed to update document position on kwit - invalid field", JsonSerializer.Serialize(dto));
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
            ErrorCodeId = documentRepository.GetError(dto.ErrorCode ?? "0")?.Id
        };

        documentRepository.AddOperation(newOperation);
        documentRepository.UpdateDocumentPosition(documentPosition);

        logRepository.AddLog(User, "Update document position on kwit", JsonSerializer.Serialize(dto));

        return Ok(new GetDocumentPositionResponseDto
        {
            Id = documentPosition.Id,
            QuantityNetto = documentPosition.QuantityNetto,
            QuantityLoss = documentPosition.QuantityLoss,
            QuantityToImprove = documentPosition.QuantityToImprove
        });
    }

    [HttpGet("closeDocument/{id}")]
    [Authorize]
    public IActionResult CloseDocument(int id)
    {
        if (!documentRepository.IsDocumentLockedByUser(id, int.Parse(User.FindFirstValue(ClaimTypes.PrimarySid) ?? "")))
        {
            logRepository.AddLog(User, "Failed to close document - document is not locked or locked by another user", JsonSerializer.Serialize(id));
            return BadRequest("Document is not locked or locked by another user");
        }

        documentRepository.UnlockDocument(id);

        logRepository.AddLog(User, "Close document", JsonSerializer.Serialize(id));

        return Ok();
    }
}
