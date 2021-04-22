using Marcelena_web.Domain;
using Marcelena_web.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using static Marcelena_web.Services.ShopService;

namespace Marcelena_web.Services
{
    public class ShopFilter
    {
        public static List<Shop> FilterShops(List<Shop> results, string number, string query, PerformFilter performFilter)
        {
            if (!string.IsNullOrEmpty(number) && Validation.isNumberValid(number))
            {
                performFilter(results, double.Parse(number.Replace('.', ',')), query);
            }
            return results;
        }

        public static void PriceSort(List<Shop> results, double number, string query)
        {
            foreach (Shop s in results)
            {
                for (int i = 0; i < s.Products.Count; i++)
                {
                    Product p = s.Products[i];
                    switch (eq)
                    {
                        case Enums.EQ.LESS:
                            if (p.ProductPrice >= number)
                            {
                                s.Products.Remove(p);
                                i--;
                            }
                            break;
                        case Enums.EQ.MORE:
                            if (p.ProductPrice <= number)
                            {
                                s.Products.Remove(p);
                                i--;
                            }
                            break;
                        case Enums.EQ.EQUAL:
                            if (p.ProductPrice != number)
                            {
                                s.Products.Remove(p);
                                i--;
                            }
                            break;
                    }
                }
            }
            for (int i = 0; i < results.Count; i++)
            {
                Shop s = results[i];
                if (s.Products.Count < 1)
                {
                    results.Remove(s);
                    i--;
                }
            }
            if (!string.IsNullOrEmpty(query))
            {
                var SearchResultsList = results
                    .Where(s => s.Products != null && s.Products.Any(z => z.ProductType.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
                results = SearchResultsList;
            }
        }

        public static void DistanceSort(List<Shop> results, double number, string query)
        {
            List<Distance> Distances = GetShopDistances(results, ShopService.userCoordinate);
            for (int i = 0; i < Distances.Count; i++)
            {
                Distance d = Distances[i];
                switch (eq)
                {
                    case Enums.EQ.LESS:
                        if (d.DistanceTo / 1000 >= number)
                        {
                            results.RemoveAt(i);
                            Distances.RemoveAt(i);
                            i--;
                        }
                        break;
                    case Enums.EQ.MORE:
                        if (d.DistanceTo / 1000 <= number)
                        {
                            results.RemoveAt(i);
                            Distances.RemoveAt(i);
                            i--;
                        }
                        break;
                    case Enums.EQ.EQUAL:
                        if (d.DistanceTo / 1000 != number)
                        {
                            results.RemoveAt(i);
                            Distances.RemoveAt(i);
                            i--;
                        }
                        break;
                }
            }
        }
    }
}
