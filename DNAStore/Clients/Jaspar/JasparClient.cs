using System.Text.Json;
using System.Text.Json.Serialization;

namespace DNAStore.Clients.Jaspar;

public class JasparClient
{
    private static readonly HttpClient Client = new();
    private static readonly string baseUrl = "https://jaspar.elixir.no/api/v1";

    public static async Task<SpeciesList> GetAllSpecies()
    {
        var response = await Client.GetAsync(baseUrl + "/species");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<SpeciesList>(content);
    }

    public static async Task<JasparKnownMotifs> GetSpeciesMotifs(string taxId)
    {
        var response = await Client.GetAsync(baseUrl + $"/species/{taxId}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<JasparKnownMotifs>(content);
    }

    public static async Task<JasparKnownMotifs> GetAllSpeciesMotifs(string taxId)
    {
        var response = await Client.GetAsync(baseUrl + $"/species/{taxId}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<JasparKnownMotifs>(content);
    }

    public static async Task<JasparKnownMotifs> GetMatrixProfile(string taxId)
    {
        throw new NotImplementedException();
    }
}

public class Species
{
    [JsonPropertyName("tax_id")] public string Tax_Id { get; set; }
    [JsonPropertyName("species")] public string SpeciesName { get; set; }
    [JsonPropertyName("url")] public string Url { get; set; }
    [JsonPropertyName("matrix_url")] public string Matrix_url { get; set; }
}

public class SpeciesList
{
    [JsonPropertyName("count")] public int Count { get; set; }
    [JsonPropertyName("next")] public string Next { get; set; }
    [JsonPropertyName("previous")] public string Previous { get; set; }
    [JsonPropertyName("results")] public List<Species> Results { get; set; }
}

// TODO: consider refactoring these out
public class JasparKnownMotifs
{
    [JsonPropertyName("count")] public int Count { get; set; }
    public string Next { get; set; }
    [JsonPropertyName("previous")] public string Previous { get; set; }
    [JsonPropertyName("results")] public List<ProfileMatrix> Results { get; set; }
}

public class ProfileMatrix
{
    [JsonPropertyName("matrix_id")] public string MatrixId { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("collection")] public string Collection { get; set; }
    [JsonPropertyName("base_id")] public string BaseId { get; set; }
    [JsonPropertyName("sequence_logo")] public string SequenceLogoLink { get; set; }
    [JsonPropertyName("url")] public string Url { get; set; }
}