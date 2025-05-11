using System.Text;
using System.Text.Json;
using Bio.IO;

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

            // Example POST request
            var postUrl = "https://jsonplaceholder.typicode.com/posts";
            var newPost = new Post
            {
                UserId = 1,
                Title = "foo",
                Body = "bar"
            };
            var postResponse = await PostAsync<Post>(postUrl, newPost);
            Console.WriteLine($"Created Post ID: {postResponse.Id}");
        }

        public static async Task<T> GetAsync<T>(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            // ok, I should probably add a constructor here for one even if it's a pain.
            // we're not given a Json back in the body -- just a raw FASTA.
            return JsonSerializer.Deserialize<T>(responseBody);
        }

        public static async Task<T> PostAsync<T>(string url, T data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
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
