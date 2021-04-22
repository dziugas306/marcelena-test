using System;
using System.ComponentModel.DataAnnotations;

namespace Marcelena_web.Domain.Entitites
{
    public class Review
    {
        public Review()
        {

        }

        [Key]
        public int _id { get; set; }
        [Required]
        public string Text { get; set; }
        public string userId { get; set; }
        public DateTime Time { get; set; }
    }
}
