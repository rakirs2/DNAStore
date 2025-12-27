using DNAStore.BioMath;

namespace DNAStoreTests.BioMath;

[TestClass]
public class NumericalSetTest
{
    [TestMethod]
    public void SimpleConstruction()
    {
        var numericalSet = new NumericalSet(10, new List<int> { 1, 2, 3, 4, 5 });
        Assert.AreEqual(10, numericalSet.MaxValue);
        Assert.IsTrue(numericalSet.Values.ToList().SequenceEqual(new List<int> { 1, 2, 3, 4, 5 }));
    }

    [TestMethod]
    public void Complement()
    {
        var numericalSet = new NumericalSet(10, new List<int> { 1, 2, 3, 4, 5 });
        var complement = numericalSet.GetComplement();
        Assert.IsTrue(complement.Values.ToList().SequenceEqual(new List<int> { 6, 7, 8, 9, 10 }));
    }

    [TestMethod]
    public void Addition()
    {
        var setOne = new NumericalSet(10, new List<int> { 1, 2, 3, 4, 5 });
        var setTwo = new NumericalSet(10, new List<int> { 7, 8 });
        var output = setOne + setTwo;
        Assert.IsTrue(output.Values.ToList().SequenceEqual(new List<int> { 1, 2, 3, 4, 5, 7, 8 }));
    }

    [TestMethod]
    public void Subtraction()
    {
        var setOne = new NumericalSet(10, new List<int> { 1, 2, 3, 4, 5 });
        var setTwo = new NumericalSet(10, new List<int> { 1, 2 });
        var output = setOne - setTwo;
        Assert.IsTrue(output.Values.ToList().SequenceEqual(new List<int> { 3, 4, 5 }));
    }

    [TestMethod]
    public void Intersection()
    {
        var setOne = new NumericalSet(10, new List<int> { 1, 2, 3, 4, 5 });
        var setTwo = new NumericalSet(10, new List<int> { 1, 2 });
        var output = NumericalSet.Intersection(setOne, setTwo);
        Assert.IsTrue(output.Values.ToList().SequenceEqual(new List<int> { 1, 2 }));
    }

    [TestMethod]
    public void Union()
    {
        var setOne = new NumericalSet(10, new List<int> { 1, 2, 3, 4, 5 });
        var setTwo = new NumericalSet(10, new List<int> { 7, 8 });
        var output = NumericalSet.Union(setOne, setTwo);
        Assert.IsTrue(output.Values.ToList().SequenceEqual(new List<int> { 1, 2, 3, 4, 5, 7, 8 }));
    }

    [TestMethod]
    public void StringOutputs()
    {
        var setOne = new NumericalSet(10, new List<int> { 1, 2, 3, 4, 5 });
        var emptySet = new NumericalSet(10, new List<int>());
        Assert.AreEqual("{1, 2, 3, 4, 5}", setOne.ToString());
        Assert.AreEqual("{}", emptySet.ToString());
    }
}