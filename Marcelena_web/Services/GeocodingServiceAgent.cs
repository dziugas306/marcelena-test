using Marcelena_web.Data;
using Marcelena_web.Domain.Entitites;

using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Marcelena_web.Services
{
    public class GeocodingServiceAgent
    {
        public static string GetCoordinates(Address address)
        {
            string apiKey = "";
            string urlParameters = LocationService.UrlFormatter(address.ToString());
            string URL = $"https://maps.googleapis.com/maps/api/geocode/json?address={urlParameters}&key={apiKey}";
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(URL).Result;
            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new InvalidOperationException("Invalid address input");
            }
        }
    }
}
