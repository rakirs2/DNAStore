using System.Text;
using System.Text.Json;
using Bio.IO;

namespace Bio.Clients
{
    public class UniprotClient
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<Fasta> GetAsync(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            // ok, I should probably add a constructor here for one even if it's a pain.
            // we're not given a Json back in the body -- just a raw FASTA.
            return FastaParser.DeserializeRawString(responseBody);
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
