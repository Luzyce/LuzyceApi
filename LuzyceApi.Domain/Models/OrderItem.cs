namespace LuzyceApi.Domain.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public int? OrderItemId { get; set; }
    public int ProductId { get; set; }
    public string? Description { get; set; }
    public int? OrderItemLp { get; set; }
    public decimal Quantity { get; set; }
    public decimal QuantityInStock { get; set; }
    public string? Unit { get; set; }
    public string? SerialNumber { get; set; }
    public string ProductSymbol { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
}
