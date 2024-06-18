using Luzyce.Core.Models.Order;
using LuzyceApi.Mappers;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController(OrderRepository orderRepository) : Controller
{
    private readonly OrderRepository orderRepository = orderRepository;

    [HttpPost("{offset}")]
    public IActionResult Get(int offset, GetOrdersDto getOrdersDto)
    {
        return Ok(orderRepository.GetOrders(offset: offset, ordersFilters: getOrdersDto.ToOrdersFiltersFromDto()));
    }

    [HttpGet("{offset}/{limit}")]
    public IActionResult Get(int offset, int limit)
    {
        return Ok(orderRepository.GetOrders(offset: offset, limit: limit));
    }

    [HttpGet("items/{orderId}")]
    public IActionResult GetItems(int orderId)
    {
        return Ok(orderRepository.GetOrderItems(orderId));
    }
}
