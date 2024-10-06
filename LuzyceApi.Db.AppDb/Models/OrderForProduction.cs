﻿namespace LuzyceApi.Db.AppDb.Models;

public class OrderForProduction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; } = string.Empty;
    public string? OriginalNumber { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public List<Document> Documents { get; set; } = [];
    public List<OrderPositionForProduction> OrderPosition { get; set; } = [];
}