using LuzyceApi.Db.Subiekt.Data;
using LuzyceApi.Domain.Models;

namespace LuzyceApi.Repositories;

public class OrderRepository(SubiektDbContext subiektDbContext)
{
    private readonly SubiektDbContext subiektDbContext = subiektDbContext;

    public List<Order> GetOrders(int limit = 20, int offset = 0)
    {
        var allOrders = subiektDbContext.DokDokuments
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
                                 OrderId = x.pozycja.ObDokHanId ?? 0,
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
            })
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.Id)
            .ToList();

        return allOrders.Skip(offset).Take(limit).ToList();
    }
}
