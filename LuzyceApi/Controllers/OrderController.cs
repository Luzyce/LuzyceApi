using Luzyce.Core.Models.Order;
using LuzyceApi.Mappers;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController(OrderRepository orderRepository) : Controller
{
    private readonly OrderRepository orderRepository = orderRepository;

    [HttpPost("{offset}")]
    [Authorize]
    public IActionResult Get(int offset, GetOrdersDto getOrdersDto)
    {
        var response = orderRepository.GetOrders(offset: offset, ordersFilters: getOrdersDto.ToOrdersFiltersFromDto());
        return Ok(new GetOrdersResponseDto
        {
            CurrentPage = response.CurrentPage,
            TotalPages = response.TotalPages,
            TotalOrders = response.TotalOrders,
            Orders = response.Orders.Select(x => new GetOrderResponseDto
            {
                Id = x.Id,
                Date = x.Date,
                Number = x.Number,
                CustomerId = x.CustomerId,
                CustomerSymbol = x.CustomerSymbol,
                CustomerName = x.CustomerName,
                Positions = x.Positions.Select(y => new GetOrderPositionResponseDto
                {
                    Id = y.Id,
                    OrderId = y.OrderId,
                    OrderNumber = y.OrderNumber,
                    Symbol = y.Symbol,
                    ProductId = y.ProductId,
                    Description = y.Description,
                    OrderPositionLp = y.OrderPositionLp,
                    Quantity = y.Quantity,
                    QuantityInStock = y.QuantityInStock,
                    Unit = y.Unit,
                    SerialNumber = y.SerialNumber,
                    ProductSymbol = y.ProductSymbol,
                    ProductName = y.ProductName,
                    ProductDescription = y.ProductDescription

                }).ToList()
            }).ToList()
        });
    }

    [HttpGet("{offset}/{limit}")]
    [Authorize]
    public IActionResult Get(int offset, int limit)
    {
        return Ok(orderRepository.GetOrders(offset: offset, limit: limit));
    }

    [HttpGet("positions/{orderId}")]
    [Authorize]
    public IActionResult GetItems(int orderId)
    {
        return Ok(orderRepository.GetOrderPositions(orderId));
    }
    
    [HttpPost("stock")]
    [Authorize]
    public IActionResult GetWarehousesLevels(StockRequest stockRequest)
    {
        var response = orderRepository.GetWarehousesLevels(stockRequest);
        return Ok(response);
    }
}
