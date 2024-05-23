namespace LuzyceApi.Dtos.Document;

public class CreateDocumentPositionDto
{
    public int NetQuantity { get; set; }
    public int QuantityLoss { get; set; } = 0;
    public int QuantityToImprove { get; set; } = 0;
    public int GrossQuantity { get; set; } = 0;
    public int LampshadeId { get; set; }
}
