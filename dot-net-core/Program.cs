using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace dot_net_core
{

    static class Helpers
    {
        public static async Task<Dictionary<string, string>> ReadAsDictionaryAsync(this HttpContent content)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(await content.ReadAsStringAsync());
        }

        public static async Task<IEnumerable<Dictionary<string, string>>> ReadAsEnumerableOfDictionaryAsync(this HttpContent content)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, string>>>(await content.ReadAsStringAsync());
        }

        public static string ToOutraFilterQueryString(this IDictionary<string, object> queryParams)
        {
            var query = System.Web.HttpUtility.ParseQueryString(String.Empty);
            query["filter"] = JsonConvert.SerializeObject(queryParams);
            return query.ToString();
        }

        // public static async Task<HttpResponseMessage> GetWithOutraFilterAsync(this HttpClient client, IDictionary<string, string> where = null) {

        // }
    }
    class Program
    {
        // HttpClient should only be instantiated once
        private static readonly HttpClient client = new HttpClient();

        // Should only need to log in once
        private static async Task InitializeClient() {
            client.BaseAddress = new Uri("https://api.outra.co.uk/api/v0/");
            var email = Environment.GetEnvironmentVariable("OUTRA_EMAIL");
            var password = Environment.GetEnvironmentVariable("OUTRA_PASSWORD");

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                throw new ArgumentException("OUTRA_EMAIL and OUTRA_PASSWORD environment variables must be set.");
            }

            var loginResult = await client.PostAsJsonAsync("Users/login", new
            {
                email = email,
                password = password
            });

            loginResult.EnsureSuccessStatusCode();

            var response = await loginResult.Content.ReadAsDictionaryAsync();
            // Add an Authorization header to all future requests
            client.DefaultRequestHeaders.Add("Authorization", response["id"]);
        }

        private static async Task MakeRequests()
        {
            await InitializeClient();

            var propertyFilterQueryString = new Dictionary<string, object>() {
            { "where", new { postcode = "W6 9PF"}}
        }.ToOutraFilterQueryString();
            var propertiesResponse = await client.GetAsync($"Properties?{propertyFilterQueryString}");
            var properties = await propertiesResponse.Content.ReadAsEnumerableOfDictionaryAsync();

            foreach (var property in properties)
            {
                Console.WriteLine(JsonConvert.SerializeObject(property, Newtonsoft.Json.Formatting.Indented));
            }



        }

        static void Main(string[] args)
        {
            MakeRequests().Wait();
        }
    }
}
