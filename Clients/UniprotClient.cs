using Bio.IO;

namespace Clients;

public class UniprotClient
{
    private static readonly HttpClient Client = new();
    private static readonly string uniprotBaseUrl = "http://www.uniprot.org/uniprot/";
    private static readonly string fastaending = ".fasta";

    /// <summary>
    /// This maps directly to the http://www.uniprot.org/uniprot/uniprot_id.fasta endpoint
    /// The user only needs to add the id
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async Task<Fasta> GetAsync(string id)
    {
        if (id.Contains('_'))
        {
            int index = id.IndexOf('_');
            id = id.Substring(0, index);
        }

        var response = await Client.GetAsync(ConvertToUniProtFastaEndpoint(id));
        response.EnsureSuccessStatusCode();
        string? responseBody = await response.Content.ReadAsStringAsync();
        // ok, I should probably add a constructor here for one even if it's a pain.
        // we're not given a Json back in the body -- just a raw FASTA.
        return FastaParser.DeserializeRawString(responseBody);
    }

    /// <summary>
    /// This maps directly to the http://www.uniprot.org/uniprot/uniprot_id.fasta endpoint
    /// The user only needs to add the id
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static Fasta Get(string id)
    {
        if (id.Contains('_'))
        {
            int index = id.IndexOf('_');
            id = id.Substring(0, index);
        }

        var response = Client.GetAsync(ConvertToUniProtFastaEndpoint(id)).Result;
        response.EnsureSuccessStatusCode();
        string? responseBody = response.Content.ReadAsStringAsync().Result;
        // ok, I should probably add a constructor here for one even if it's a pain.
        // we're not given a Json back in the body -- just a raw FASTA.
        return FastaParser.DeserializeRawString(responseBody);
    }

    private static string ConvertToUniProtFastaEndpoint(string id)
    {
        return string.Concat(new string[] { uniprotBaseUrl, id, fastaending });
    }
}

public class Post
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
}