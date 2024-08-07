using Luzyce.Core.Models.Order;
using LuzyceApi.Db.Subiekt.Data;
using LuzyceApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LuzyceApi.Repositories;

public class OrderRepository(SubiektDbContext subiektDbContext)
{
    private readonly SubiektDbContext subiektDbContext = subiektDbContext;

    public GetOrdersResponse GetOrders(int limit = 10, int offset = 0, OrdersFilters? ordersFilters = null)
    {
        var query = subiektDbContext.DokDokuments
            .Where(d => d.DokTyp == 16 && d.DokDataWyst > DateTime.Now.AddYears(-2))
            .Join(subiektDbContext.AdrHistoria,
                  d => d.DokPlatnikAdreshId,
                  o => o.AdrhId,
                  (d, o) => new { d, o })
            .Join(subiektDbContext.KhKontrahents,
                  temp => temp.d.DokPlatnikId,
                  k => k.KhId,
                  (temp, k) => new { temp.d, temp.o, k })
            .GroupJoin(subiektDbContext.DokPozycjas,
                       dok => dok.d.DokId,
                       pozycja => pozycja.ObDokHanId,
                       (dok, pozycje) => new { dok, pozycje })
            .SelectMany(temp => temp.pozycje.DefaultIfEmpty(),
                        (temp, pozycja) => new { temp.dok.d, temp.dok.o, temp.dok.k, pozycja })
            .GroupJoin(subiektDbContext.TwTowars,
                       temp => temp.pozycja!.ObTowId,
                       towar => towar.TwId,
                       (temp, towary) => new { temp, towary })
            .SelectMany(temp => temp.towary.DefaultIfEmpty(),
                        (temp, towar) => new { temp.temp.d, temp.temp.o, temp.temp.k, temp.temp.pozycja, towar })
            .GroupBy(temp => new
            {
                temp.d.DokId,
                temp.d.DokDataWyst,
                temp.d.DokNrPelny,
                temp.k.KhId,
                temp.k.KhSymbol,
                temp.o.AdrhNazwaPelna
            })
            .Select(group => new Order
            {
                Id = group.Key.DokId,
                Date = group.Key.DokDataWyst.Date,
                Number = group.Key.DokNrPelny,
                CustomerId = group.Key.KhId,
                CustomerSymbol = group.Key.KhSymbol,
                CustomerName = group.Key.AdrhNazwaPelna,
                Positions = group.Where(x => x.pozycja != null && x.towar != null)
                             .Select(x => new OrderPosition
                             {
                                 Id = x.pozycja!.ObId,
                                 OrderId = x.pozycja.ObDokHanId,
                                 OrderNumber = x.d.DokNrPelny,
                                 Symbol = x.towar!.TwSymbol,
                                 ProductId = x.towar.TwId,
                                 Description = x.pozycja.ObOpis,
                                 OrderPositionLp = x.pozycja.ObDokHanLp,
                                 Quantity = x.pozycja.ObIlosc,
                                 QuantityInStock = x.pozycja.ObIloscMag,
                                 Unit = x.pozycja.ObJm,
                                 SerialNumber = x.pozycja.ObNumerSeryjny,
                                 ProductSymbol = x.towar.TwSymbol,
                                 ProductName = x.towar.TwNazwa,
                                 ProductDescription = x.towar.TwOpis
                             })
                             .OrderBy(x => x.OrderPositionLp)
                             .ToList()
            });

        if (ordersFilters != null)
        { 
            if (ordersFilters.StartDate.HasValue)
            {
                query = query.Where(o => o.Date >= ordersFilters.StartDate.Value);
            }

            if (ordersFilters.EndDate.HasValue)
            {
                query = query.Where(o => o.Date <= ordersFilters.EndDate.Value);
            }

            if (!string.IsNullOrEmpty(ordersFilters.CustomerName))
            {
                query = query.Where(o => o.CustomerName.Contains(ordersFilters.CustomerName));
            }
        }

        return new GetOrdersResponse
        {
            CurrentPage = offset,
            TotalPages = (int)Math.Ceiling(query.Count() / (double)limit),
            TotalOrders = query.Count(),
            Orders = query
                .OrderByDescending(x => x.Date)
                .ThenByDescending(x => x.Id)
                .Skip((offset - 1) * limit)
                .Take(limit)
                .ToList()
        };
    }

    public List<OrderPosition> GetOrderPositions(int dokId)
    {
        var orderPositions = subiektDbContext.DokPozycjas
            .Where(p => p.ObDokHanId == dokId)
            .Join(subiektDbContext.TwTowars,
                  p => p.ObTowId,
                  t => t.TwId,
                  (p, t) => new { p, t })
            .Join(subiektDbContext.DokDokuments,
                  temp => temp.p.ObDokHanId,
                  d => d.DokId,
                  (temp, d) => new { temp, d })
            .Select(x => new OrderPosition
            {
                Id = x.temp.p.ObId,
                OrderId = x.temp.p.ObDokHanId,
                OrderNumber = x.d.DokNrPelny,
                Symbol = x.temp.t.TwSymbol,
                ProductId = x.temp.t.TwId,
                Description = x.temp.p.ObOpis,
                OrderPositionLp = x.temp.p.ObDokHanLp,
                Quantity = x.temp.p.ObIlosc,
                QuantityInStock = x.temp.p.ObIloscMag,
                Unit = x.temp.p.ObJm,
                SerialNumber = x.temp.p.ObNumerSeryjny,
                ProductSymbol = x.temp.t.TwSymbol,
                ProductName = x.temp.t.TwNazwa,
                ProductDescription = x.temp.t.TwOpis
            })
            .ToList();
        return orderPositions;
    }

    public StockResponse GetWarehousesLevels(StockRequest filters)
    {
        var productIds = filters.ProductIds;
        var warehouseIds = filters.WarehouseIds;

        var query = subiektDbContext.TwStans
            .Where(x => productIds.Contains(x.StTowId) && warehouseIds.Contains(x.StMagId))
            .Include(x => x.StMag)
            .Select(x => new
            {
                x.StTowId,
                x.StMagId,
                x.StMag.MagNazwa,
                Quantity = (int)x.StStan,
                QuantityMin = (int)x.StStanMin,
                QuantityRes = (int)x.StStanRez,
                QuantityMax = (int)x.StStanMax
            })
            .AsEnumerable()
            .GroupBy(x => x.StTowId)
            .ToDictionary(
                g => g.Key,
                g => new ProductWarehouseStocks
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(x => x.Quantity),
                    QuantityMin = g.Min(x => x.QuantityMin),
                    QuantityRes = g.Sum(x => x.QuantityRes),
                    QuantityMax = g.Max(x => x.QuantityMax),
                    WarehouseStocks = g.Select(x => new WarehouseStocks
                    {
                        WarehouseId = x.StMagId,
                        WarehouseName = x.MagNazwa,
                        Quantity = x.Quantity,
                        QuantityMin = x.QuantityMin,
                        QuantityRes = x.QuantityRes,
                        QuantityMax = x.QuantityMax
                    }).ToList()
                });

        var response = new StockResponse
        {
            ProductWarehousesStocks = productIds.Select(productId =>
            {
                if (query.TryGetValue(productId, out var productStock))
                {
                    return productStock;
                }

                return new ProductWarehouseStocks
                {
                    ProductId = productId,
                    Quantity = 0,
                    QuantityMin = 0,
                    QuantityRes = 0,
                    QuantityMax = 0,
                    WarehouseStocks = warehouseIds.Select(warehouseId => new WarehouseStocks
                    {
                        WarehouseId = warehouseId,
                        WarehouseName = string.Empty,
                        Quantity = 0,
                        QuantityMin = 0,
                        QuantityRes = 0,
                        QuantityMax = 0
                    }).ToList()
                };
            }).ToList()
        };

        return response;
    }

}
