
using LuzyceApi.Domain.Models;
using LuzyceApi.Dtos.Document;

namespace LuzyceApi.Mappers;

public static class DocumentMappers
{
    public static Document ToDocumentFromCreateDto(this CreateDocumentDto dto)
    {
        return new Document
        {
            WarehouseId = dto.WarehouseId,
            DocumentsDefinitionId = dto.DocumentsDefinitionId
        };
    }

    public static DocumentPositions ToDocumentPositionFromCreateDto(this CreateDocumentPositionDto dto)
    {
        return new DocumentPositions
        {
            QuantityNetto = dto.QuantityNetto,
            QuantityLoss = dto.QuantityLoss,
            QuantityToImprove = dto.QuantityToImprove,
            QuantityGross = dto.QuantityGross,
            StartTime = DateTime.Now,
            LampshadeId = dto.LampshadeId
        };
    }
}
