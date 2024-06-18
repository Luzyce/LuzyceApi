using LuzyceApi.Db.Subiekt.Data;
using LuzyceApi.Domain.Models;

namespace LuzyceApi.Repositories;

public class OrderRepository(SubiektDbContext subiektDbContext)
{
    private readonly SubiektDbContext subiektDbContext = subiektDbContext;

    public List<Order> GetOrders(int limit = 20, int offset = 0, OrdersFilters? ordersFilters = null)
    {
        var query = subiektDbContext.DokDokuments
            .Where(d => d.DokTyp == 16 && d.DokDataWyst > DateTime.Now.AddYears(-1))
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
                Items = group.Where(x => x.pozycja != null && x.towar != null)
                             .Select(x => new OrderItem
                             {
                                 Id = x.pozycja!.ObId,
                                 OrderId = x.pozycja.ObDokHanId,
                                 OrderNumber = x.d.DokNrPelny,
                                 Symbol = x.towar!.TwSymbol,
                                 OrderItemId = x.pozycja.ObTowId,
                                 ProductId = x.towar.TwId,
                                 Description = x.pozycja.ObOpis,
                                 OrderItemLp = x.pozycja.ObDokHanLp,
                                 Quantity = x.pozycja.ObIlosc,
                                 QuantityInStock = x.pozycja.ObIloscMag,
                                 Unit = x.pozycja.ObJm,
                                 SerialNumber = x.pozycja.ObNumerSeryjny,
                                 ProductSymbol = x.towar.TwSymbol,
                                 ProductName = x.towar.TwNazwa,
                                 ProductDescription = x.towar.TwOpis
                             }).ToList()
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

            if (!string.IsNullOrEmpty(ordersFilters.customerName))
            {
                // query = query.Where(o => o.CustomerName.StartsWith(ordersFilters.customerName));
                query = query.Where(o => o.CustomerName.Contains(ordersFilters.customerName));
            }
        }

        return query
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.Id)
            .Skip(offset)
            .Take(limit)
            .ToList();
    }

    public List<OrderItem> GetOrderItems(int dokId)
    {
        var orderItems = subiektDbContext.DokPozycjas
            .Where(p => p.ObDokHanId == dokId)
            .Join(subiektDbContext.TwTowars,
                  p => p.ObTowId,
                  t => t.TwId,
                  (p, t) => new { p, t })
            .Join(subiektDbContext.DokDokuments,
                  temp => temp.p.ObDokHanId,
                  d => d.DokId,
                  (temp, d) => new { temp, d })
            .Select(x => new OrderItem
            {
                Id = x.temp.p.ObId,
                OrderId = x.temp.p.ObDokHanId,
                OrderNumber = x.d.DokNrPelny,
                Symbol = x.temp.t.TwSymbol,
                OrderItemId = x.temp.p.ObTowId,
                ProductId = x.temp.t.TwId,
                Description = x.temp.p.ObOpis,
                OrderItemLp = x.temp.p.ObDokHanLp,
                Quantity = x.temp.p.ObIlosc,
                QuantityInStock = x.temp.p.ObIloscMag,
                Unit = x.temp.p.ObJm,
                SerialNumber = x.temp.p.ObNumerSeryjny,
                ProductSymbol = x.temp.t.TwSymbol,
                ProductName = x.temp.t.TwNazwa,
                ProductDescription = x.temp.t.TwOpis
            })
            .ToList();
        return orderItems;
    }
}
