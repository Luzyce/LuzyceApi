using System.Text.RegularExpressions;
using LuzyceApi.Db.AppDb.Data;
using LuzyceApi.Db.AppDb.Data.Models;
using LuzyceApi.Db.AppDb.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace LuzyceApi.Repositories
{
    public class ProductionOrderRepository(ApplicationDbContext applicationDbContext)
    {
        private readonly ApplicationDbContext applicationDbContext = applicationDbContext;
        public int SaveProductionOrder(Domain.Models.Order order, Domain.Models.ProductionOrder productionOrder)
        {
            using var transaction = applicationDbContext.Database.BeginTransaction();

            try
            {
                var existingOrder = applicationDbContext.OrdersForProduction
                    .FirstOrDefault(o => o.Id == order.Id);

                if (existingOrder != null)
                {
                    return CreateDocument(productionOrder, existingOrder.Id, transaction);
                }

                var orderForProduction = new OrderForProduction
                {
                    Id = order.Id,
                    Date = order.Date,
                    Number = order.Number,
                    CustomerId = order.CustomerId,
                    CustomerSymbol = order.CustomerSymbol,
                    CustomerName = order.CustomerName
                };
                applicationDbContext.OrdersForProduction.Add(orderForProduction);

                foreach (var item in order.Items)
                {
                    var lampshadeCode = Regex.Match(item.Symbol, @"^[A-Z]{2}\d{4}").Value;
                    
                    var lampshade = applicationDbContext.Lampshades
                        .FirstOrDefault(l => l.Code == lampshadeCode);

                    if (lampshade == null)
                    {
                        lampshade = new Lampshade
                        {
                            Code = lampshadeCode
                        };
                        applicationDbContext.Lampshades.Add(lampshade);
                        applicationDbContext.SaveChanges();
                    }
                    
                    var orderItemForProduction = new OrderItemForProduction
                    {
                        Id = item.Id,
                        OrderId = order.Id,
                        OrderNumber = order.Number,
                        Symbol = item.Symbol,
                        OrderItemId = item.Id,
                        ProductId = lampshade.Id,
                        Description = item.Description,
                        OrderItemLp = item.OrderItemLp,
                        Quantity = item.Quantity,
                        QuantityInStock = item.QuantityInStock,
                        Unit = item.Unit,
                        SerialNumber = item.SerialNumber,
                        ProductSymbol = item.ProductSymbol,
                        ProductName = item.ProductName,
                        ProductDescription = item.ProductDescription
                    };
                    applicationDbContext.OrderItemsForProduction.Add(orderItemForProduction);
                }
                applicationDbContext.SaveChanges();

                return CreateDocument(productionOrder, orderForProduction.Id, transaction);
            }
            catch (Exception exception)
            {
                exception.ToString();
                transaction.Rollback();
                return 0;
            }
        }

        private int CreateDocument(Domain.Models.ProductionOrder productionOrder, int orderForProductionId, IDbContextTransaction transaction)
        {
            try
            {
                var currentYear = DateTime.Now.Year;
                var docNumber = applicationDbContext.Documents
                    .Where(d => d.WarehouseId == Dictionaries.Warehouses.Produkcja
                                && d.Year == currentYear
                                && d.DocumentsDefinitionId == Dictionaries.DocumentsDefinitions.ZlecenieProdukcji)
                    .Select(d => d.DocNumber)
                    .ToList()
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                var document = new Document
                {
                    DocNumber = docNumber,
                    WarehouseId = Dictionaries.Warehouses.Produkcja,
                    Year = currentYear,
                    Number = $"{docNumber:D4}/{applicationDbContext.DocumentsDefinitions
                        .Where(w => w.Id == Dictionaries.DocumentsDefinitions.ZlecenieProdukcji)
                        .Select(w => w.Code)
                        .FirstOrDefault()}/{currentYear}",
                    DocumentsDefinitionId = Dictionaries.DocumentsDefinitions.ZlecenieProdukcji,
                    OperatorId = productionOrder.OperatorId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    ClosedAt = null,
                    StatusId = 1
                };
                applicationDbContext.Documents.Add(document);
                applicationDbContext.SaveChanges();

                foreach (var position in productionOrder.ProductionOrderPositions)
                {
                    var lampshade = applicationDbContext.Lampshades
                        .FirstOrDefault(l => l.Code == position.Symbol);

                    var documentPosition = new DocumentPositions
                    {
                        DocumentId = document.Id,
                        QuantityNetto = position.Net,
                        QuantityGross = position.Gross,
                        OperatorId = productionOrder.OperatorId,
                        StartTime = DateTime.Now,
                        StatusId = 1,
                        LampshadeId = lampshade.Id,
                        OrderForProductionId = orderForProductionId
                    };
                    applicationDbContext.DocumentPositions.Add(documentPosition);
                }
                applicationDbContext.SaveChanges();

                transaction.Commit();
                return 1;
            }
            catch
            {
                transaction.Rollback();
                return 0;
            }
        }
    }
}
