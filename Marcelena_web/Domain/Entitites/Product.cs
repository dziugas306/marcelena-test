
using System.ComponentModel.DataAnnotations;

namespace Marcelena_web.Domain.Entitites
{
    public class Product
    {
        public Product()
        {

        }

        public Product(string productType, double productPrice)
        {
            ProductType = productType;
            ProductPrice = productPrice;
        }

        [Key]
        public int _id { get; set; }
        [Required]
        [RegularExpression("^[\"\\-A-Za-z\\s\\p{L}]+$", ErrorMessage = "Product type can only consist letters, '-' and spaces.")]
        public string ProductType { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [RegularExpression("^[0-9]+([,][0-9][0-9]?)?$", ErrorMessage = "Wrong price fromat entered. Correct price format: (#,##).")]
        public double ProductPrice { get; set; }
    }
}
