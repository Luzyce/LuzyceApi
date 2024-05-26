namespace LuzyceApi.Dtos.Document;

public class UpdateDocumentPositionOnKwit
{
    public char type { get; set; }
    public required string field { get; set; }
    public int? errorCode { get; set; }
}
