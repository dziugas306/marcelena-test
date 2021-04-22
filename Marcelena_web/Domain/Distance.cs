namespace Marcelena_web.Domain
{
    public class Distance
    {
        public Distance(int ShopID, double DistanceTo)
        {
            this.ShopID = ShopID;
            this.DistanceTo = DistanceTo;
        }
        public int ShopID { get; set; }
        public double DistanceTo { get; set; }
    }
}
