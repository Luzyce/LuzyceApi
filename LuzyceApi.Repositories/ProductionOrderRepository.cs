using System.Text.RegularExpressions;
using Luzyce.Core.Models.Document;
using Luzyce.Core.Models.Lampshade;
using Luzyce.Core.Models.ProductionOrder;
using Luzyce.Core.Models.User;
using LuzyceApi.Db.AppDb.Data;
using LuzyceApi.Db.AppDb.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace LuzyceApi.Repositories
{
    public class ProductionOrderRepository(ApplicationDbContext applicationDbContext)
    {
        private readonly ApplicationDbContext applicationDbContext = applicationDbContext;

        public GetProductionOrdersResponse GetProductionOrders()
        {
            return new GetProductionOrdersResponse()
            {
                ProductionOrders = applicationDbContext.Documents
                    .Where(d => d.DocumentsDefinitionId == Dictionaries.DocumentsDefinitions.ZlecenieProdukcji)
                    .Select(d => new GetProductionOrder
                    {
                        Id = d.Id,
                        DocNumber = d.DocNumber,
                        Warehouse = new GetWarehouseResponseDto
                        {
                            Id = d.WarehouseId,
                            Code = applicationDbContext.Warehouses
                                .Where(w => w.Id == d.WarehouseId)
                                .Select(w => w.Code)
                                .FirstOrDefault()
                        },
                        Year = d.Year,
                        Number = d.Number,
                        DocumentsDefinition = new GetDocumentsDefinitionResponseDto
                        {
                            Id = d.DocumentsDefinitionId,
                            Code = applicationDbContext.DocumentsDefinitions
                                .Where(dd => dd.Id == d.DocumentsDefinitionId)
                                .Select(dd => dd.Code)
                                .FirstOrDefault()
                        },
                        User = new GetUserResponseDto
                        {
                            Id = d.OperatorId,
                            Name = applicationDbContext.Users
                                .Where(u => u.Id == d.OperatorId)
                                .Select(u => u.Name)
                                .FirstOrDefault() ?? string.Empty
                        },
                        CreatedAt = d.CreatedAt,
                        UpdatedAt = d.UpdatedAt,
                        ClosedAt = d.ClosedAt,
                        Status = applicationDbContext.Statuses
                            .Where(s => s.Id == d.StatusId)
                            .Select(s => new GetStatusResponseDto
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Priority = s.Priority
                            })
                            .FirstOrDefault(),
                        Positions = null
                    })
                    .ToList()
            };
        }

        public GetProductionOrder GetProductionOrder(int Id)
        {
            return applicationDbContext.Documents
                .Where(d => d.DocumentsDefinitionId == Dictionaries.DocumentsDefinitions.ZlecenieProdukcji && d.Id == Id)
                .Select(d => new
                {
                    Document = d,
                    WarehouseCode = applicationDbContext.Warehouses
                        .Where(w => w.Id == d.WarehouseId)
                        .Select(w => w.Code)
                        .FirstOrDefault(),
                    DocumentsDefinitionCode = applicationDbContext.DocumentsDefinitions
                        .Where(dd => dd.Id == d.DocumentsDefinitionId)
                        .Select(dd => dd.Code)
                        .FirstOrDefault(),
                    OperatorName = applicationDbContext.Users
                        .Where(u => u.Id == d.OperatorId)
                        .Select(u => u.Name)
                        .FirstOrDefault(),
                    Status = applicationDbContext.Statuses
                        .Where(s => s.Id == d.StatusId)
                        .Select(s => new GetStatusResponseDto
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Priority = s.Priority
                        })
                        .FirstOrDefault(),
                    Positions = applicationDbContext.DocumentPositions
                        .Where(dp => dp.DocumentId == d.Id)
                        .Select(dp => new
                        {
                            Position = dp,
                            Lampshade = applicationDbContext.Lampshades
                                .Where(l => l.Id == dp.LampshadeId)
                                .Select(l => new GetLampshade
                                {
                                    Id = l.Id,
                                    Code = l.Code
                                })
                                .FirstOrDefault(),
                            LampshadeNorm = applicationDbContext.LampshadeNorms
                                .Where(ln => ln.Id == dp.LampshadeNormId)
                                .Select(ln => new
                                {
                                    Norm = ln,
                                    Lampshade = applicationDbContext.Lampshades
                                        .Where(l => l.Id == ln.LampshadeId)
                                        .Select(l => new GetLampshade
                                        {
                                            Id = l.Id,
                                            Code = l.Code
                                        })
                                        .FirstOrDefault(),
                                    Variant = applicationDbContext.LampshadeVariants
                                        .Where(v => v.Id == ln.VariantId)
                                        .Select(v => new GetVariantResponseDto
                                        {
                                            Id = v.Id,
                                            Name = v.Name
                                        })
                                        .FirstOrDefault()
                                })
                                .FirstOrDefault()
                        })
                        .ToList()
                })
                .AsEnumerable()
                .Select(d => new GetProductionOrder
                {
                    Id = d.Document.Id,
                    DocNumber = d.Document.DocNumber,
                    Warehouse = new GetWarehouseResponseDto
                    {
                        Id = d.Document.WarehouseId,
                        Code = d.WarehouseCode
                    },
                    Year = d.Document.Year,
                    Number = d.Document.Number,
                    DocumentsDefinition = new GetDocumentsDefinitionResponseDto
                    {
                        Id = d.Document.DocumentsDefinitionId,
                        Code = d.DocumentsDefinitionCode
                    },
                    User = new GetUserResponseDto
                    {
                        Id = d.Document.OperatorId,
                        Name = d.OperatorName ?? string.Empty
                    },
                    CreatedAt = d.Document.CreatedAt,
                    UpdatedAt = d.Document.UpdatedAt,
                    ClosedAt = d.Document.ClosedAt,
                    Status = d.Status,
                    Positions = d.Positions.Select(dp => new GetProductionOrderPositions
                    {
                        Id = dp.Position.Id,
                        QuantityNetto = dp.Position.QuantityNetto,
                        QuantityGross = dp.Position.QuantityGross,
                        CreatedAt = dp.Position.StartTime,
                        Lampshade = dp.Lampshade!,
                        LampshadeNorm = new GetLampshadeNorm
                        {
                            Id = dp.LampshadeNorm!.Norm.Id,
                            Lampshade = dp.LampshadeNorm.Lampshade!,
                            Variant = dp.LampshadeNorm.Variant!
                        },
                        LampshadeDekor = dp.Position.LampshadeDekor
                    }).ToList()
                })
                .FirstOrDefault()!;
        }

        public int SaveProductionOrder(Domain.Models.Order order, Domain.Models.ProductionOrder productionOrder)
        {
            using var transaction = applicationDbContext.Database.BeginTransaction();

            try
            {
                var existingOrder = applicationDbContext.OrdersForProduction
                    .FirstOrDefault(o => o.Id == order.Id);

                if (existingOrder != null)
                {
                    return CreateDocument(productionOrder, transaction);
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

                foreach (var position in order.Positions)
                {
                    var lampshadeCode = Regex.Match(position.Symbol, @"^[A-Z]{2}\d{4}").Value;

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

                    var orderPositionForProduction = new OrderPositionForProduction
                    {
                        Id = position.Id,
                        OrderId = order.Id,
                        OrderNumber = order.Number,
                        Symbol = position.Symbol,
                        ProductId = lampshade.Id,
                        Description = position.Description,
                        OrderPositionLp = position.OrderPositionLp,
                        Quantity = position.Quantity,
                        QuantityInStock = position.QuantityInStock,
                        Unit = position.Unit,
                        SerialNumber = position.SerialNumber,
                        ProductSymbol = position.ProductSymbol,
                        ProductName = position.ProductName,
                        ProductDescription = position.ProductDescription
                    };
                    applicationDbContext.OrderPositionsForProduction.Add(orderPositionForProduction);
                }

                applicationDbContext.SaveChanges();

                return CreateDocument(productionOrder, transaction);
            }
            catch
            {
                transaction.Rollback();
                return 0;
            }
        }

        private int CreateDocument(Domain.Models.ProductionOrder productionOrder, IDbContextTransaction transaction)
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
                    Number = $"{
                        applicationDbContext.Warehouses
                            .Where(w => w.Id == Dictionaries.Warehouses.Produkcja)
                            .Select(w => w.Code)
                            .FirstOrDefault()}/{docNumber:D4}/{
                            applicationDbContext.DocumentsDefinitions
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
                    var variant = applicationDbContext.LampshadeVariants
                        .FirstOrDefault(l => l.Id == position.VariantId);

                    if (lampshade == null || variant == null)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                    
                    var lampshadeNorms = applicationDbContext.LampshadeNorms
                        .FirstOrDefault(l => l.LampshadeId == lampshade.Id
                                             && l.VariantId == variant.Id);
                    
                    if (lampshadeNorms == null)
                    {
                        lampshadeNorms = new LampshadeNorm
                        {
                            LampshadeId = lampshade.Id,
                            VariantId = variant.Id
                        };
                        applicationDbContext.LampshadeNorms.Add(lampshadeNorms);
                        applicationDbContext.SaveChanges();
                    }

                    var documentPosition = new DocumentPositions
                    {
                        DocumentId = document.Id,
                        QuantityNetto = position.Net,
                        QuantityGross = position.Gross,
                        OperatorId = productionOrder.OperatorId,
                        StartTime = DateTime.Now,
                        StatusId = 1,
                        LampshadeId = lampshade.Id,
                        LampshadeNormId = lampshadeNorms.Id,
                        LampshadeDekor = position.Dekor,
                        OrderPositionForProductionId = position.DocumentPositionId
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