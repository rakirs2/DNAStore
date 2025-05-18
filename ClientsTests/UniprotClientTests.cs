using Clients;

namespace ClientsTests;

[TestClass]
public class UniprotClientTests
{
    [TestMethod]
    public void GetAsync()
    {
        // TODO: need to learn this later, but this can't be ideal
        var result = UniprotClient.GetAsync("B5ZC00");
        Assert.IsNotNull(result.Result);
    }

    [TestMethod]
    public void GetAsyncComplicatedId()
    {
        // TODO: need to learn this later, but this can't be ideal
        var result = UniprotClient.GetAsync("P07204_TRBM_HUMAN");
        Assert.IsNotNull(result.Result);
    }
}