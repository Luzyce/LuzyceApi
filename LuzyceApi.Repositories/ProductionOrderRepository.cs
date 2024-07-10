using System.Text.RegularExpressions;
using Luzyce.Core.Models.Document;
using Luzyce.Core.Models.Lampshade;
using Luzyce.Core.Models.ProductionOrder;
using Luzyce.Core.Models.User;
using LuzyceApi.Db.AppDb.Data;
using LuzyceApi.Db.AppDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LuzyceApi.Repositories;

public class ProductionOrderRepository(ApplicationDbContext applicationDbContext)
{
    private readonly ApplicationDbContext applicationDbContext = applicationDbContext;

    public GetProductionOrdersResponse GetProductionOrders()
    {
        return new GetProductionOrdersResponse()
        {
            ProductionOrders = applicationDbContext.Documents
                .Where(d => d.DocumentsDefinitionId == Dictionaries.DocumentsDefinitions.ZP_ID)
                .Include(d => d.Warehouse)
                .Include(d => d.DocumentsDefinition)
                .Include(d => d.Operator)
                .Include(d => d.Status)
                .Select(d => new GetProductionOrder
                {
                    Id = d.Id,
                    DocNumber = d.DocNumber,
                    Warehouse = new GetWarehouseResponseDto
                    {
                        Id = d.Warehouse!.Id,
                        Code = d.Warehouse.Code
                    },
                    Year = d.Year,
                    Number = d.Number,
                    DocumentsDefinition = new GetDocumentsDefinitionResponseDto
                    {
                        Id = d.DocumentsDefinition!.Id,
                        Code = d.DocumentsDefinition.Code
                    },
                    User = new GetUserResponseDto
                    {
                        Id = d.Operator!.Id,
                        Name = d.Operator.Name
                    },
                    CreatedAt = d.CreatedAt,
                    UpdatedAt = d.UpdatedAt,
                    ClosedAt = d.ClosedAt,
                    Status = new GetStatusResponseDto
                    {
                        Id = d.Status!.Id,
                        Name = d.Status.Name,
                        Priority = d.Status.Priority
                    }
                })
                .ToList()
        };
    }

    public GetProductionOrder? GetProductionOrder(int Id)
    {
        var document = applicationDbContext.Documents
            .Where(d => d.DocumentsDefinitionId == Dictionaries.DocumentsDefinitions.ZP_ID && d.Id == Id)
            .Include(d => d.Warehouse)
            .Include(d => d.DocumentsDefinition)
            .Include(d => d.Operator)
            .Include(d => d.Status)
            .FirstOrDefault();

        if (document == null)
        {
            return null;
        }

        var positions = applicationDbContext.DocumentPositions
            .Where(dp => dp.DocumentId == Id)
            .Include(dp => dp.Lampshade)
            .Include(dp => dp.LampshadeNorm)
            .ThenInclude(ln => ln!.Variant)
            .Include(op => op.OrderPositionForProduction)
            .Select(dp => new GetProductionOrderPosition
            {
                Id = dp.Id,
                QuantityNetto = dp.QuantityNetto,
                QuantityGross = dp.QuantityGross,
                ExecutionDate = dp.EndTime,
                Lampshade = new GetLampshade
                {
                    Id = dp.Lampshade!.Id,
                    Code = dp.Lampshade.Code
                },
                LampshadeNorm = new GetLampshadeNorm()
                {
                    Id = dp.LampshadeNorm!.Id,
                    Lampshade = new GetLampshade
                    {
                        Id = dp.Lampshade.Id,
                        Code = dp.Lampshade.Code
                    },
                    Variant = new GetVariantResponseDto
                    {
                        Id = dp.LampshadeNorm.Variant!.Id,
                        Name = dp.LampshadeNorm.Variant.Name,
                        ShortName = dp.LampshadeNorm.Variant.ShortName
                    },
                    QuantityPerChange = dp.LampshadeNorm.QuantityPerChange ?? 0
                },
                LampshadeDekor = dp.LampshadeDekor,
                Remarks = dp.Remarks,
                NumberOfChanges = dp.po_NumberOfChanges,
                QuantityMade = dp.po_QuantityMade,
                MethodOfPackaging = dp.MethodOfPackaging,
                QuantityPerPack = dp.QuantityPerPack,
                ProductId = dp.SubiektProductId ?? 0,
                Unit = dp.OrderPositionForProduction!.Unit!
            })
            .ToList();

        return new GetProductionOrder
        {
            Id = document.Id,
            DocNumber = document.DocNumber,
            Warehouse = new GetWarehouseResponseDto
            {
                Id = document.Warehouse!.Id,
                Code = document.Warehouse.Code
            },
            Year = document.Year,
            Number = document.Number,
            DocumentsDefinition = new GetDocumentsDefinitionResponseDto
            {
                Id = document.DocumentsDefinition!.Id,
                Code = document.DocumentsDefinition.Code
            },
            User = new GetUserResponseDto
            {
                Id = document.Operator!.Id,
                Name = document.Operator.Name
            },
            CreatedAt = document.CreatedAt,
            UpdatedAt = document.UpdatedAt,
            ClosedAt = document.ClosedAt,
            Status = new GetStatusResponseDto
            {
                Id = document.Status!.Id,
                Name = document.Status.Name,
                Priority = document.Status.Priority
            },
            Positions = positions
        };
    }
        
    public GetOrdersPositionsResponse GetPositions()
    {
        return new GetOrdersPositionsResponse
        {
            OrdersPositions = new List<GetProductionOrderPosition>(applicationDbContext.DocumentPositions
                .Include(dp => dp.Document)
                .Include(dp => dp.Lampshade)
                .Include(dp => dp.LampshadeNorm)
                .ThenInclude(ln => ln!.Variant)
                .Include(op => op.OrderPositionForProduction)
                .Where(dp => dp.Document!.DocumentsDefinitionId == Dictionaries.DocumentsDefinitions.ZP_ID && dp.Document.StatusId == 1)
                .Select(dp => new GetProductionOrderPosition
                {
                    Id = dp.Id,
                    QuantityNetto = dp.QuantityNetto,
                    QuantityGross = dp.QuantityGross,
                    ExecutionDate = dp.EndTime,
                    Lampshade = new GetLampshade
                    {
                        Id = dp.Lampshade!.Id,
                        Code = dp.Lampshade.Code
                    },
                    LampshadeNorm = new GetLampshadeNorm()
                    {
                        Id = dp.LampshadeNorm!.Id,
                        Lampshade = new GetLampshade
                        {
                            Id = dp.Lampshade.Id,
                            Code = dp.Lampshade.Code
                        },
                        Variant = new GetVariantResponseDto
                        {
                            Id = dp.LampshadeNorm.Variant!.Id,
                            Name = dp.LampshadeNorm.Variant.Name,
                            ShortName = dp.LampshadeNorm.Variant.ShortName
                        },
                        QuantityPerChange = dp.LampshadeNorm.QuantityPerChange ?? 0
                    },
                    LampshadeDekor = dp.LampshadeDekor,
                    Remarks = dp.Remarks,
                    NumberOfChanges = dp.po_NumberOfChanges,
                    QuantityMade = dp.po_QuantityMade,
                    MethodOfPackaging = dp.MethodOfPackaging,
                    QuantityPerPack = dp.QuantityPerPack,
                    ProductId = dp.SubiektProductId ?? 0,
                    Unit = dp.OrderPositionForProduction!.Unit!,
                    ProductionOrderNumber = dp.Document!.Number,
                    Client = dp.OrderPositionForProduction.Order!.CustomerName,
                    Priority = dp.Priority ?? 0
                })
                .ToList()
                .OrderBy(x => x.Priority))
        };
    }

    public GetProductionOrder? GetProductionOrderByNumber(string number)
    {
        var document = applicationDbContext.Documents
            .Where(d => d.DocumentsDefinitionId == Dictionaries.DocumentsDefinitions.ZP_ID && d.Number == number)
            .Include(d => d.Warehouse)
            .Include(d => d.DocumentsDefinition)
            .Include(d => d.Operator)
            .Include(d => d.Status)
            .FirstOrDefault();

        if (document == null)
        {
            return null;
        }

        var positions = applicationDbContext.DocumentPositions
            .Where(dp => dp.DocumentId == document.Id)
            .Include(dp => dp.Lampshade)
            .Include(dp => dp.LampshadeNorm)
            .ThenInclude(ln => ln!.Variant)
            .Include(op => op.OrderPositionForProduction)
            .Select(dp => new GetProductionOrderPosition
            {
                Id = dp.Id,
                QuantityNetto = dp.QuantityNetto,
                QuantityGross = dp.QuantityGross,
                ExecutionDate = dp.EndTime,
                Lampshade = new GetLampshade
                {
                    Id = dp.Lampshade!.Id,
                    Code = dp.Lampshade.Code
                },
                LampshadeNorm = new GetLampshadeNorm()
                {
                    Id = dp.LampshadeNorm!.Id,
                    Lampshade = new GetLampshade
                    {
                        Id = dp.Lampshade.Id,
                        Code = dp.Lampshade.Code
                    },
                    Variant = new GetVariantResponseDto
                    {
                        Id = dp.LampshadeNorm.Variant!.Id,
                        Name = dp.LampshadeNorm.Variant.Name,
                        ShortName = dp.LampshadeNorm.Variant.ShortName
                    },
                    QuantityPerChange = dp.LampshadeNorm.QuantityPerChange ?? 0
                },
                LampshadeDekor = dp.LampshadeDekor,
                Remarks = dp.Remarks,
                NumberOfChanges = dp.po_NumberOfChanges,
                QuantityMade = dp.po_QuantityMade,
                MethodOfPackaging = dp.MethodOfPackaging,
                QuantityPerPack = dp.QuantityPerPack,
                ProductId = dp.SubiektProductId ?? 0,
                Unit = dp.OrderPositionForProduction!.Unit!
            })
            .ToList();

        return new GetProductionOrder
        {
            Id = document.Id,
            DocNumber = document.DocNumber,
            Warehouse = new GetWarehouseResponseDto
            {
                Id = document.Warehouse!.Id,
                Code = document.Warehouse.Code
            },
            Year = document.Year,
            Number = document.Number,
            DocumentsDefinition = new GetDocumentsDefinitionResponseDto
            {
                Id = document.DocumentsDefinition!.Id,
                Code = document.DocumentsDefinition.Code
            },
            User = new GetUserResponseDto
            {
                Id = document.Operator!.Id,
                Name = document.Operator.Name
            },
            CreatedAt = document.CreatedAt,
            UpdatedAt = document.UpdatedAt,
            ClosedAt = document.ClosedAt,
            Status = new GetStatusResponseDto
            {
                Id = document.Status!.Id,
                Name = document.Status.Name,
                Priority = document.Status.Priority
            },
            Positions = positions
        }; 
    }
    public int SaveProdOrder(Domain.Models.Order order, Domain.Models.ProductionOrder productionOrder)
    {
        using var transaction = applicationDbContext.Database.BeginTransaction();

        try
        {
            var existingOrder = applicationDbContext.OrdersForProduction
                .FirstOrDefault(o => o.Id == order.Id);

            if (existingOrder != null)
            {
                return CreateProdOrder(productionOrder, transaction);
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

            return CreateProdOrder(productionOrder, transaction);
        }
        catch
        {
            transaction.Rollback();
            return 0;
        }
    }

    private int CreateProdOrder(Domain.Models.ProductionOrder productionOrder, IDbContextTransaction transaction)
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var docNumber = applicationDbContext.Documents
                .Where(d => d.WarehouseId == Dictionaries.Warehouses.P_ID
                            && d.Year == currentYear
                            && d.DocumentsDefinitionId == Dictionaries.DocumentsDefinitions.ZP_ID)
                .Select(d => d.DocNumber)
                .ToList()
                .DefaultIfEmpty(0)
                .Max() + 1;

            var document = new Document
            {
                DocNumber = docNumber,
                WarehouseId = Dictionaries.Warehouses.P_ID,
                Year = currentYear,
                Number = $"{Dictionaries.Warehouses.P_CODE}/{docNumber:D4}/{Dictionaries.DocumentsDefinitions.ZP_CODE}/{currentYear}",
                DocumentsDefinitionId = Dictionaries.DocumentsDefinitions.ZP_ID,
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
                    LampshadeId = lampshade.Id,
                    LampshadeNormId = lampshadeNorms.Id,
                    LampshadeDekor = position.Dekor,
                    OrderPositionForProductionId = position.DocumentPositionId,
                    SubiektProductId = position.SubiektProductId
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

    public int UpdateProdOrder(int id, UpdateProductionOrder updateProductionOrderDto)
    {
        using var transaction = applicationDbContext.Database.BeginTransaction();
        try
        {
            foreach (var position in updateProductionOrderDto.Positions)
            {
                var existingPosition = applicationDbContext.DocumentPositions
                    .Include(dp => dp.LampshadeNorm)
                    .Where(dp => dp.DocumentId == id)
                    .FirstOrDefault(dp => dp.Id == position.Id);
            
                if (existingPosition?.LampshadeNorm == null)
                {
                    transaction.Rollback();
                    return 0;
                }
            
                existingPosition.QuantityNetto = position.QuantityNetto;
                existingPosition.po_NumberOfChanges = position.NumberOfChanges;
                existingPosition.LampshadeNorm.QuantityPerChange = position.QuantityPerChange;
                existingPosition.EndTime = position.ExecutionDate;
                existingPosition.po_QuantityMade = position.QuantityMade;
                existingPosition.Remarks = position.Remarks;
                existingPosition.MethodOfPackaging = position.MethodOfPackaging;
                existingPosition.QuantityPerPack = position.QuantityPerPack;
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