using Luzyce.Core.Models.Document;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace LuzyceApi.Controllers;
[Route("api/document")]
[ApiController]
public class KwitController(KwitRepository kwitRepository, LogRepository logRepository) : Controller
{
    private readonly KwitRepository kwitRepository = kwitRepository;
    private readonly LogRepository logRepository = logRepository;

    [HttpPost("terminal/getKwit")]
    [Authorize]
    public IActionResult TerminalGetKwit([FromBody] GetDocumentByQrCodeDto dto)
    {
        var kwit = kwitRepository
            .GetKwit(Int32.Parse(Regex.Match(dto.Number, "(kwit-(\\d+))").Groups[2].Value));

        if (kwit == null)
        {
            logRepository.AddLog(User, "Nie udało się pobrać kwitu — kwit nie został znaleziony", JsonSerializer.Serialize(dto));
            return NotFound();
        }
        if (kwitRepository.IsKwitLocked(kwit.Id) && !kwitRepository.IsKwitLockedByUser(kwit.Id, int.Parse(User.FindFirstValue(ClaimTypes.PrimarySid) ?? "")))
        {
            logRepository.AddLog(User, "Nie udało się pobrać kwitu — kwit jest zablokowany", JsonSerializer.Serialize(dto));
            return Conflict();
        }

        kwitRepository.LockKwit(kwit.Id, int.Parse(User.FindFirstValue(ClaimTypes.PrimarySid) ?? ""));

        logRepository.AddLog(User, "Pobrano kwit na terminalu", JsonSerializer.Serialize(dto));

        return Ok(new GetDocumentByQrCodeResponseDto
        {
            Id = kwit.Id,
            Number = kwit.Number,
            DocumentPositions = kwitRepository.GetKwitPositions(kwit.Id).Select(x => new GetDocumentPositionResponseDto
            {
                Id = x.Id,
                QuantityNetto = x.QuantityNetto,
                QuantityLoss = x.QuantityLoss,
                QuantityToImprove = x.QuantityToImprove
            }).ToList()
        });
    }

    [HttpPut("terminal/updateKwit/{id:int}")]
    [Authorize]
    public IActionResult TerminalUpdateKwit(int id, [FromBody] UpdateDocumentPositionOnKwitDto dto)
    {
        var kwit = kwitRepository.GetKwit(id);

        if (kwit?.DocumentsDefinition is not { Code: "KW" } || (dto.Type != '+' && dto.Type != '-'))
        {
            logRepository.AddLog(User, "Nie udało się zaktualizować kwitu - nieprawidłowe żądanie", JsonSerializer.Serialize(dto));
            return BadRequest("Invalid request");
        }

        if (!kwitRepository.IsKwitLockedByUser(id, int.Parse(User.FindFirstValue(ClaimTypes.PrimarySid) ?? "")))
        {
            logRepository.AddLog(User, "Nie udało się zaktualizować kwitu – kwit nie jest zablokowany lub został zablokowany przez innego użytkownika", JsonSerializer.Serialize(dto));
            return BadRequest("Document is locked by another user or not locked");
        }

        var documentPosition = kwitRepository.GetKwitPositions(id).FirstOrDefault();
        var documentPositionBefore = documentPosition;

        if (documentPosition == null)
        {
            logRepository.AddLog(User, "Nie udało się zaktualizować kwitu - pozycja dokumentu nie została znaleziona", JsonSerializer.Serialize(dto));
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
            if ((dto.ErrorCode == null || kwitRepository.GetError(dto.ErrorCode ?? "0") == null) && dto.Type == '+')
            {
                return BadRequest(dto.ErrorCode);
            }

            documentPosition.QuantityLoss = dto.Type == '+' ? documentPosition.QuantityLoss + 1 : documentPosition.QuantityLoss - 1;
        }
        else
        {
            logRepository.AddLog(User, "Nie udało się zaktualizować kwitu - nieprawidłowe pole", JsonSerializer.Serialize(dto));
            return BadRequest("Invalid field");
        }

        documentPosition.QuantityGross = documentPosition.QuantityNetto + documentPosition.QuantityLoss + documentPosition.QuantityToImprove;

        var newOperation = new Domain.Models.Operation
        {
            DocumentId = id,
            Operator = kwit.Operator,
            OperatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"),
            QuantityNetDelta = documentPosition.QuantityNetto - (documentPositionBefore?.QuantityNetto ?? 0),
            QuantityLossDelta = documentPosition.QuantityLoss - (documentPositionBefore?.QuantityLoss ?? 0),
            QuantityToImproveDelta = documentPosition.QuantityToImprove - (documentPositionBefore?.QuantityToImprove ?? 0),
            ErrorCodeId = kwitRepository.GetError(dto.ErrorCode ?? "0")?.Id
        };

        kwitRepository.AddOperation(newOperation);
        kwitRepository.UpdateKwitPosition(documentPosition);

        logRepository.AddLog(User, "Zaktualizowano Kwit przez Terminal", JsonSerializer.Serialize(dto));

        return Ok(new GetDocumentPositionResponseDto
        {
            Id = documentPosition.Id,
            QuantityNetto = documentPosition.QuantityNetto,
            QuantityLoss = documentPosition.QuantityLoss,
            QuantityToImprove = documentPosition.QuantityToImprove
        });
    }

    [HttpGet("terminal/closeKwit/{id:int}")]
    [Authorize]
    public IActionResult CloseKwit(int id)
    {
        if (!kwitRepository.IsKwitLockedByUser(id, int.Parse(User.FindFirstValue(ClaimTypes.PrimarySid) ?? "")))
        {
            logRepository.AddLog(User, "Nie udało się zamknąć kwitu - kwit nie jest zablokowany lub został zablokowany przez innego użytkownika", JsonSerializer.Serialize(id));
            return BadRequest("Kwit is not locked or locked by another user");
        }

        kwitRepository.UnlockKwit(id);

        logRepository.AddLog(User, "Zamknięto Kwit", JsonSerializer.Serialize(id));

        return Ok();
    }
}
