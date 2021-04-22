using System.ComponentModel.DataAnnotations;

namespace Marcelena_web.Domain.Entitites
{
    public class Photo
    {
        public Photo()
        {

        }
        public Photo(string path)
        {
            PhotoPath = path;
        }
        [Key]
        public int _id { get; set; }
        public string PhotoPath { get; set; }
    }
}
