using Marcelena_web.Domain.Entitites;
using System.Collections.Generic;

namespace Marcelena_web.Services.Comparers
{
    class ShopAddressComparer : IComparer<Shop>
    {
        public int Compare(Shop x, Shop y)
        {
            if (x.ShopAddress.CountryName.Equals(y.ShopAddress.CountryName))
            {
                if (x.ShopAddress.CityName.Equals(y.ShopAddress.CityName))
                {
                    if (x.ShopAddress.StreetName.Equals(y.ShopAddress.StreetName))
                    {
                        if (x.ShopAddress.StreetNumber.Equals(y.ShopAddress.StreetNumber))
                        {
                            return x.ShopAddress.ApartmentNumber.CompareTo(y.ShopAddress.ApartmentNumber);
                        }
                        else
                        {
                            return x.ShopAddress.StreetNumber.CompareTo(y.ShopAddress.StreetNumber);
                        }
                    }
                    else
                    {
                        return x.ShopAddress.StreetName.CompareTo(y.ShopAddress.StreetName);

                    }
                }
                else
                {
                    return x.ShopAddress.CityName.CompareTo(y.ShopAddress.CityName);
                }
            }
            else
            {
                return x.ShopAddress.CountryName.CompareTo(y.ShopAddress.CountryName);
            }

        }
    }
}
