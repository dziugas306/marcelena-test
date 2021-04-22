using Marcelena_web.Domain.Entitites;
using System.Collections.Generic;

namespace Marcelena_web.Services.Comparers
{
    class ShopNameComparer : IComparer<Shop>
    {
        public int Compare(Shop x, Shop y)
        {
            return x.ShopName.CompareTo(y.ShopName);
        }
    }

}