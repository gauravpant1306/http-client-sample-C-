#region snippet_all
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientSample
{
    #region snippet_prod
    public class BMORequest
    {
        public string Id { get; set; }
        public string NameToInvoke { get; set; }
    }

    public class BMOResponse
    {
        public string Id { get; set; }
        public string Topic { get; set; }
    }
    #endregion

    class Program
    {
        #region snippet_HttpClient
        static HttpClient client = new HttpClient();
        #endregion

       
        #region snippet_GetProductAsync
        static async Task<BMOResponse> GetKafkaTopic(BMORequest bMORequest)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "aws-endpoint", bMORequest);
            response.EnsureSuccessStatusCode();
            BMOResponse bMOResponse = await response.Content.ReadAsAsync<BMOResponse>();
            Console.WriteLine("Topic:{0}", bMOResponse.Topic);
            return bMOResponse;
        }
        #endregion

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        #region snippet_run
        #region snippet5
        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://127.0.0.1:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            #endregion

            try
            {
                // Create a new product
                BMORequest bMORequest = new BMORequest
                {
                    NameToInvoke = "Test"
                };

                // Get the product
                BMOResponse bMOResponse = await GetKafkaTopic(bMORequest);
                Console.WriteLine($"Kafka (Topic = {bMOResponse.Topic})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
        #endregion
    }
}
#endregion