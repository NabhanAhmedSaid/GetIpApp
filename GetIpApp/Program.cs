using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GetIpApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string ipApi = "https://api.ipify.org/?format=json";
            string result = await CallURL(ipApi);

            if (!string.IsNullOrEmpty(result))
            {
                
                  
                try
                {
                    IpClass ipObject = JsonConvert.DeserializeObject<IpClass>(result);
                    Console.WriteLine($"IP: {ipObject.ip}");
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Failed to get IP from API");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        public static async Task<string> CallURL(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    client.DefaultRequestHeaders.Accept.Clear();
                    return await client.GetStringAsync(url);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling URL: {ex.Message}");
                return string.Empty;
            }
        }

        public class IpClass
        {
            [JsonProperty("ip")]
            public string ip { get; set; }
        }
    }
}