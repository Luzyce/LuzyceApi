namespace LuzyceApi.Domain.Models;

public class OrdersFilters
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? customerName { get; set; }
}
