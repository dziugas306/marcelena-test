using GeoCoordinatePortable;
using Marcelena_web.Data;
using Marcelena_web.Domain.Entitites;
using Newtonsoft.Json.Linq;
using System;

namespace Marcelena_web.Services
{
    public class LocationService
    {
        public static string UrlFormatter(string query)
        {
            return query.Replace(" ", "%20").Replace(",", "%2C");
        }
        public static string GetMapsUrl(string query)
        {
            string apiKey = "";
            string formattedQuery = UrlFormatter(query);
            return $"https://www.google.com/maps/embed/v1/place?key={apiKey}&q={formattedQuery}";
        }
        public static Coordinate GetCoordinates(Address address)
        {
            var response = GeocodingServiceAgent.GetCoordinates(address);
            JObject s = JObject.Parse(response.ToString());

            var lat = s["results"][0]["geometry"]["location"].First.ToObject<double>();
            var lng = s["results"][0]["geometry"]["location"].Last.ToObject<double>();

            return new Coordinate(lat, lng);
        }

        public static double CalculateDistance(Coordinate a, Coordinate b)
        {
            var geoA = new GeoCoordinate(a.Latitude, a.Longtitude);
            var geoB = new GeoCoordinate(b.Latitude, b.Longtitude);

            return geoA.GetDistanceTo(geoB);
        }
    }
}