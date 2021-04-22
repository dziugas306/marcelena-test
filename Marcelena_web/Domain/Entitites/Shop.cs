using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Marcelena_web.Domain.Entitites
{
    public class Shop
    {
        public Shop()
        {

        }
        public Shop(string shopName, Address shopAddress)
        {
            ShopName = shopName;
            ShopAddress = shopAddress;
        }
        public Shop(string shopName, Address shopAddress, List<Product> products)
        {
            ShopName = shopName;
            ShopAddress = shopAddress;
            Products = products;
        }

        [Key]
        public int _id { get; set; }
        [Required]
        [RegularExpression("^[\"A-Za-z\\p{L}\\s]+$", ErrorMessage = "Shop name can only consist of letters and spaces")]
        public string ShopName { get; set; }
        public virtual List<Product> Products { get; set; }
        [Required]
        public Address ShopAddress { get; set; }
        public List<User> Users { get; set; }
        public virtual List<Review> Reviews { get; set; }
        public Coordinate Coordinate { get; set; }
        public virtual List<Photo> PhotoPaths { get; set; }
    }
}
