namespace LuzyceApi.Dtos.Document;

public class UpdateDocumentPositionOnKwitDto
{
    public char type { get; set; }
    public required string field { get; set; }
    public string? errorCode { get; set; }
}
