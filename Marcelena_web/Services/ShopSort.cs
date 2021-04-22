using Marcelena_web.Domain;
using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using static Marcelena_web.Domain.Enums;

namespace Marcelena_web.Services
{
    public class ShopSort
    {
        public static List<Shop> OrderBy(List<Shop> results, string query)
        {
            switch (ShopService.orderedBy)
            {
                case ORDER_BY.SHOP_NAME:
                    results.Sort(new ShopNameComparer());
                    break;
                case ORDER_BY.SHOP_ADDRESS:
                    results.Sort(new ShopAddressComparer());
                    break;
                case ORDER_BY.DISTANCE_TO:
                    results = OrderByDistance(results);
                    break;
                case ORDER_BY.PRODUCT_PRICE:
                    results = OrderByPrice(results, query);
                    break;
            }
            return results;
        }

        public static List<Shop> OrderByPrice(List<Shop> results, string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                List<Shop> prodResults = results
                  .Where(s => s.Products != null && s.Products.Any(z => z.ProductType.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0))
                  .OrderBy(s => s.Products.First(p => p.ProductType.Contains(query, StringComparison.OrdinalIgnoreCase)).ProductPrice).ToList();                
                results = prodResults;
                PlaceSearchedProductOnTop(results, query);
            }
            else
            {
                foreach (Shop r in results)
                {
                    r.Products.Sort(new ProductPriceComparer());
                }
            }
            return results;
        }

        public static void PlaceSearchedProductOnTop(List<Shop> results, string query)
        {
            foreach (Shop r in results)
            {
                Product item = r.Products.Find(x => x.ProductType.Contains(query, StringComparison.OrdinalIgnoreCase));
                r.Products.Remove(item);
                r.Products.Insert(0, item);
            }
        }

        public static List<Shop> OrderByDistance(List<Shop> results)
        {
            List<Distance> distances = ShopService.GetShopDistances(results, ShopService.userCoordinate);
            var sortedResults = distances
                .OrderBy(d => d.DistanceTo)
                .Join(results, d => d.ShopID, s => s._id, (d,s) => s).ToList();
            return sortedResults;
        }
    }
}
