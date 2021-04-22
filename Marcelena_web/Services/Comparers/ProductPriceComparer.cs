using Marcelena_web.Domain.Entitites;
using System.Collections.Generic;

namespace Marcelena_web.Services.Comparers
{
    class ProductPriceComparer : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            return x.ProductPrice.CompareTo(y.ProductPrice);
        }
    }
}
