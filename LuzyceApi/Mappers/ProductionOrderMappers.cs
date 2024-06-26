using Luzyce.Core.Models.ProductionOrder;
using LuzyceApi.Domain.Models;

namespace LuzyceApi.Mappers;

public static class ProductionOrderMappers
{
    public static Order ToOrderFromCreateDto(this CreateProductionOrderDto dto)
    {
        return new Order()
        {
            Id = dto.Order.Id,
            Date = dto.Order.Date,
            Number = dto.Order.Number,
            CustomerId = dto.Order.CustomerId,
            CustomerSymbol = dto.Order.CustomerSymbol,
            CustomerName = dto.Order.CustomerName,
            Items = dto.Order.Items.Select(x => new OrderItem
            {
                Id = x.Id,
                OrderId = x.OrderId,
                OrderNumber = x.OrderNumber,
                Symbol = x.Symbol,
                OrderItemId = x.OrderItemId,
                ProductId = x.ProductId,
                Description = x.Description,
                OrderItemLp = x.OrderItemLp,
                Quantity = x.Quantity,
                QuantityInStock = x.QuantityInStock,
                Unit = x.Unit,
                SerialNumber = x.SerialNumber,
                ProductSymbol = x.ProductSymbol,
                ProductName = x.ProductName,
                ProductDescription = x.ProductDescription
            }).ToList()
        };
    } 
    public static ProductionOrder ToProductionOrderFromCreateDto(this CreateProductionOrderDto dto)
    {
        return new ProductionOrder()
        {
            OperatorId = 0,
            ProductionOrderPositions = dto.ProductionOrderPositions.Select(x => new ProductionOrderPosition
            {
                DocumentId = x.DocumentId,
                Symbol = x.Symbol,
                Gross = x.Gross,
                Net = x.Net
            }).ToList()
        };
    }
}