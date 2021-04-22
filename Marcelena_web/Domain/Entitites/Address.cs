using System.ComponentModel.DataAnnotations;

namespace Marcelena_web.Domain.Entitites
{
    public class Address
    {
        public Address()
        {

        }

        public Address(string streetName, string cityName, string countryName)
        {
            CountryName = countryName;
            CityName = cityName;
            StreetName = streetName;
        }

        public Address(string countryName, string cityName, string streetName, string streetNumber)
        {
            CountryName = countryName;
            CityName = cityName;
            StreetName = streetName;
            StreetNumber = streetNumber;
            ApartmentNumber = "";
        }

        public Address(string countryName, string cityName, string streetName, string streetNumber, string apartmentNumber) : this(streetName, cityName, countryName, streetNumber)
        {
            ApartmentNumber = "-" + apartmentNumber;
        }

        [Key]
        public int _id { get; set; }
        [Required]
        [RegularExpression("^[\\-A-Za-z\\s\\p{L}]+$", ErrorMessage = "Country name can only consist of letters, '-' and spaces.")]
        public string CountryName { get; set; }
        [Required]
        [RegularExpression("^[\\-A-Za-z\\s\\p{L}]+$", ErrorMessage = "City name can only consist of letters, '-' and spaces.")]
        public string CityName { get; set; }
        [Required]
        [RegularExpression("^[\\-A-Za-z\\.\\s\\p{L}]+$", ErrorMessage = "Street name can only consist of letters, '-' and spaces.")]
        public string StreetName { get; set; }
        [Required]
        [RegularExpression("^[\\d]+[A-Za-z]*$", ErrorMessage = "Street number can only consist of numbers.")]
        public string StreetNumber { get; set; }
        [RegularExpression("^(([\\d]+[\\dA-Za-z\\-]*)|\\-)?$", ErrorMessage = "Apartment number can only consist of numbers.")]
        public string ApartmentNumber { get; set; }

        public override string ToString()
        {
            return StreetName + " " + StreetNumber + " " + ApartmentNumber + ", " + CityName + ", " + CountryName;
        }
    }
}
