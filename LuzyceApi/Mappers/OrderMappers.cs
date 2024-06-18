using Luzyce.Core.Models.Order;
using LuzyceApi.Domain.Models;

namespace LuzyceApi.Mappers;

public static class OrderMappers
{
    public static OrdersFilters ToOrdersFiltersFromDto(this GetOrdersDto dto)
    {
        return new OrdersFilters
        {
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            customerName = dto.customerName
        };
    }
}
