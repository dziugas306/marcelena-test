using System.ComponentModel.DataAnnotations;

namespace Marcelena_web.Domain.Entitites
{
    public class Coordinate
    {
        [Key]
        public int _id { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public Coordinate()
        {

        }
        public Coordinate(double latitude, double longtitude)
        {
            Latitude = latitude;
            Longtitude = longtitude;
        }
        public override string ToString()
        {
            return Latitude + "," + Longtitude;
        }

    }
}
