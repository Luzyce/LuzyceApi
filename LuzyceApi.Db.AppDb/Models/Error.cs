namespace LuzyceApi.Db.AppDb.Data.Models;

public class Error
{
    public int Id { get; set; }
    public required int Code { get; set; }
    public required string ShortName { get; set; }
    public required string Name { get; set; }
}
