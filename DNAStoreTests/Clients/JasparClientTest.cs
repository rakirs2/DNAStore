using DnaStore.Clients.Jaspar;

namespace BaseTests.Clients;

[TestClass]
public class JasparClientTest
{
    [TestMethod]
    public void GetSpeciesAsync()
    {
        var result = JasparClient.GetAllSpecies();
        Assert.IsNotNull(result.Result);
    }

    [TestMethod]
    public void GetHuman()
    {
        var result = JasparClient.GetSpeciesMotifs("9606");
        Assert.IsNotNull(result.Result);
    }

    [TestMethod]
    public void GetHumanAll()
    {
        var result = JasparClient.GetSpeciesMotifs("9606");
        Assert.IsNotNull(result.Result);
    }
}