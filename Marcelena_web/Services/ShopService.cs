using Marcelena_web.Domain;
using Marcelena_web.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using static Marcelena_web.Domain.Enums;

namespace Marcelena_web.Services
{

    public class ShopService
    {
        public static ORDER_BY orderedBy = ORDER_BY.NONE;
        public static EQ eq = EQ.NONE;

        public delegate void PerformFilter(List<Shop> results, double number, string query);
        public static List<Distance> Distances { get; set; }
        public static Coordinate userCoordinate { get; set; }

        public static List<Shop> DoSort(List<Shop> results, int orderby, string query, Coordinate coordinate)
        {
            orderedBy = (ORDER_BY)orderby;
            if (orderby != 0)
            {
                switch (orderby)
                {
                    case 3:
                        if(coordinate == null)
                        {
                            orderedBy = 0;
                        }
                        userCoordinate = coordinate;
                        break;
                }
            }
            results = ShopSort.OrderBy(results, query);
            return results;
        }

        public static List<Shop> DoFilter(List<Shop> results, int filterby, int fiterByEQ, string number, string query, Coordinate coordinate)
        {
            eq = (EQ)fiterByEQ;
            if (fiterByEQ != 0)
            {
                switch (filterby)
                {
                    case 1:
                        results = ShopFilter.FilterShops(results, number, query, ShopFilter.PriceSort);
                        break;
                    case 2:
                        if (coordinate == null)
                        {
                            break;
                        }
                        userCoordinate = coordinate;
                        results = ShopFilter.FilterShops(results, number, query, ShopFilter.DistanceSort);
                        break;
                }
            }
            return results;
        }

        public static List<Shop> ShopByQuery(List<Shop> shops, string Query)
        {
            List<Shop> filteredShops = shops.Where(s => s.ShopName != null && s.ShopName.Contains(Query, StringComparison.OrdinalIgnoreCase)
                  || s.Products != null && s.Products.Any(z => z.ProductType.Contains(Query, StringComparison.OrdinalIgnoreCase))
                  || s.ShopAddress != null && s.ShopAddress.ToString().Contains(Query, StringComparison.OrdinalIgnoreCase)).ToList();
            return filteredShops;
        }

        public static List<Distance> GetShopDistances(List<Shop> Shop, Coordinate coordinate)
        {
            Distances = new List<Distance>();
            foreach (var item in Shop)
            {
                //Constant for now, until we can get the location of the current user.
                Distances.Add(new Distance(item._id, LocationService.CalculateDistance(item.Coordinate, coordinate)));
            }
            return Distances;
        }
    }
}
