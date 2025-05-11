using System.Text;
using System.Text.Json;

namespace Bio.Clients
{
    public class UniprotClient
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            // Example GET request
            var getUrl = "https://rest.uniprot.org/uniprotkb/A2Z669.fasta";
            var getResponse = await GetAsync<Post>(getUrl);
            Console.WriteLine($"Title: {getResponse.Title}");

        }

        public static async Task<T> GetAsync<T>(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseBody);
        }
    }

    public class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
