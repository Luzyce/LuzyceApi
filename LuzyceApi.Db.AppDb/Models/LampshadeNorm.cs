using LuzyceApi.Db.AppDb.Data.Models;

namespace LuzyceApi.Db.AppDb.Models;

public class LampshadeNorm
{
    public int Id { get; set; }
    public int LampshadeId { get; set; }
    public Lampshade? Lampshade { get; set; }
    public int VariantId { get; set; }
    public LampshadeVariant? Variant { get; set; }
    public int QuantityPerChange { get; set; }
}