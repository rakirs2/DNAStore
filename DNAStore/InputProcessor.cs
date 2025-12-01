using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Base.Algorithms;
using Base.DataStructures;
using Bio.Analysis.Types;
using Bio.IO;
using Bio.Sequence.Types;
using BioMath;
using Clients;

namespace DNAStore;

internal static class InputProcessor
{
    public static IExecutor GetExecutor(string request)
    {
        return BaseExecutor.GetExecutorFromString(request);
    }

    private static void WriteToDesktopOutputFile(string filecontents)
    {
        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        File.WriteAllText(desktopPath + "/output.txt", filecontents);
    }

    public class ExecutorRegistry
    {
        private static Dictionary<string, IExecutor>? _map;

        [ModuleInitializer]
        public static void Initialize()
        {
            _map = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IExecutor).IsAssignableFrom(t) && !t.IsAbstract)
                .Where(t => t.GetCustomAttribute<Executor>() != null)
                .ToDictionary(
                    t => t.Name, // use class name automatically
                    t => (IExecutor)Activator.CreateInstance(t)!
                );
        }

        public static IExecutor Get(string name)
        {
            return _map.TryGetValue(name, out var exe)
                ? exe
                : new SequenceAnalysis(); // your fallback
        }
    }

    public interface IExecutor
    {
        /// <summary>
        ///     Executes the request.
        /// </summary>
        void Run();
    }

    [AttributeUsage(AttributeTargets.Class)]
    private class Executor : Attribute
    {
    }

    [Executor]
    private abstract class BaseExecutor : IExecutor
    {
        private Stopwatch? _stopwatch;
        protected string? Output;

        public void Run()
        {
            GetInputs();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            Console.WriteLine("Calculating");
            CalculateResult();
            _stopwatch.Stop();
            OutputResult();
            ReportMetrics();
        }

        public static IExecutor GetExecutorFromString(string input)
        {
            return ExecutorRegistry.Get(input);
        }

        /// <summary>
        ///     Should solely be user facing
        /// </summary>
        protected abstract void GetInputs();

        /// <summary>
        ///     Ideally this is one function. However, that isn't always possible
        /// </summary>
        protected abstract void CalculateResult();

        /// <summary>
        ///     Controls the basic output flow. Everything should go to the output.txt file on the desktop along with
        ///     a clean print to the console.
        /// </summary>
        private void OutputResult()
        {
            Console.WriteLine($"{Output}");
            WriteToDesktopOutputFile(Output);
        }

        private void ReportMetrics()
        {
            Console.WriteLine($"Calculation took: {_stopwatch.ElapsedMilliseconds}ms");
        }
    }

    // TODO: there should be a way to avoid the analyzer for this
    private class HammingSequenceMatch : BaseExecutor
    {
        private SequenceMatchLocations? _matcher;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the match sequence");
            var inputString = Console.ReadLine();

            Console.WriteLine("Please enter the tolerance");

            var tolerance = int.Parse(Console.ReadLine());
            var matchLogic = new HammingMatch(inputString, tolerance);

            Console.WriteLine("Please enter the sequence to be analyzed");
            var sequence = new AnySequence(Console.ReadLine());

            _matcher = new SequenceMatchLocations(sequence, matchLogic);
        }

        protected override void CalculateResult()
        {
            Output = string.Join(' ', _matcher.GetLocations());
        }
    }

    private class HammingFuzzyMatch : BaseExecutor
    {
        private string? _input;
        private MismatchKmerCounter? _matcher;
        private HashSet<string>? output;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the match sequence, usually ACGT");
            _input = Console.ReadLine();

            Console.WriteLine("Please enter the length of the Kmer");
            var kmerLength = int.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the tolerance");
            var tolerance = int.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the sequence to be analyzed");
            var sequence = new AnySequence(Console.ReadLine());

            _matcher = new MismatchKmerCounter(kmerLength, sequence, tolerance);
        }

        protected override void CalculateResult()
        {
            Output = string.Join(' ', _matcher.GetKmers(_input));
        }
    }

    private class ThreeSum : BaseExecutor
    {
        private List<List<int>> _lists = new List<List<int>>();
        private DnaSequence _sequence;

        protected override void GetInputs()
        {
            List<List<double>> profileValues = new();
            var input = "";
            while (true)
            {
                input = Console.ReadLine();
                if (input == "done") break;

                var inputA = input.Split(" ")
                    .Select(s => int.Parse(s))
                    .ToList();
                _lists.Add(inputA);
            }
        }

        protected override void CalculateResult()
        {
            var values = new  List<List<List<int>>>();
            foreach (var list in _lists)
            {
                values.Add(Search.ThreeSumNoSort(list, 0));
            }

            var sb = new StringBuilder();
            foreach (var list in values)
            {
                if (list.Count == 0)
                {
                    sb.Append("-1");
                }
                else
                {
                    List<int> incrementedList = list[0].Select(x => x + 1).ToList();
                    sb.Append(string.Join(" ", incrementedList));
                }
                
                sb.Append("\n");
            }
            
            Output =  sb.ToString();
        }
    }
    private class DnaProfileHighestLikelihoodString : BaseExecutor
    {
        private ProbabilityProfile _matrix;
        private DnaSequence _sequence;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the match sequence, usually ACGT");
            _sequence = new DnaSequence(Console.ReadLine());
            List<List<double>> profileValues = new();
            var input = "";
            while (true)
            {
                input = Console.ReadLine();
                if (input == "done") break;

                var inputA = input.Split(" ")
                    .Select(s => double.Parse(s))
                    .ToList();
                profileValues.Add(inputA);
            }

            _matrix = new ProbabilityProfile(profileValues, "ACGT");
        }

        protected override void CalculateResult()
        {
            Output = _matrix.HighestLikelihood(_sequence);
        }
    }

    private class MergeSort : BaseExecutor
    {
        private int[] _list;

        protected override void GetInputs()
        {
            Console.WriteLine("Input the list to be sorted as a single list");
            _list = Console.ReadLine().Split(" ")
                .Select(s => int.Parse(s))
                .ToArray();
        }

        protected override void CalculateResult()
        {
            Sorters<int>.InPlaceMergeSort(ref _list);
            Output = string.Join(" ", _list);
        }
    }
    
    private class InversionCounter : BaseExecutor
    {
        private int[] _list;

        protected override void GetInputs()
        {
            Console.WriteLine("Input the list to be sorted as a single list");
            _list = Console.ReadLine().Split(" ")
                .Select(s => int.Parse(s))
                .ToArray();
        }

        protected override void CalculateResult()
        {
            Output = Sorters<int>.InPlaceMergeSort(ref _list).ToString();
        }
    }

    private class GetFirstSubsequenceIndices : BaseExecutor
    {
        private AnySequence? mainSequence;
        private List<int>? output;
        private AnySequence? subSequence;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter main sequence");
            mainSequence = new AnySequence(Console.ReadLine());

            Console.WriteLine("Please enter the subsequence to be analyzed");
            subSequence = new AnySequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            Output = string.Join(' ', mainSequence.FindFirstPossibleSubSequence(subSequence));
        }
    }

    private class SetCalculations : BaseExecutor
    {
        private NumericalSet a;
        private NumericalSet aComplement;
        private NumericalSet aMinusB;
        private NumericalSet b;
        private NumericalSet bComplement;
        private NumericalSet bMinusA;
        private NumericalSet intersection;

        private NumericalSet union;

        protected override void GetInputs()
        {
            Console.WriteLine("Enter the maximum value");
            var maxValue = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the values of set A");
            var inputA = Console.ReadLine().Split(", ")
                .Select(s => int.Parse(s))
                .ToList();

            Console.WriteLine("Enter the values of set B");
            var inputB = Console.ReadLine().Split(", ")
                .Select(s => int.Parse(s))
                .ToList();

            a = new NumericalSet(maxValue, inputA);
            b = new NumericalSet(maxValue, inputB);
        }

        protected override void CalculateResult()
        {
            union = NumericalSet.Union(a, b);
            intersection = NumericalSet.Intersection(a, b);
            aMinusB = a - b;
            bMinusA = b - a;
            aComplement = a.GetComplement();
            bComplement = b.GetComplement();
            var results = new[] { union, intersection, aMinusB, bMinusA, aComplement, bComplement };
            Output = string.Join("\n", results.Select(r => r.ToString()));
        }
    }

    private class MergeTwoSorted : BaseExecutor
    {
        private int[] a;
        private int[] b;

        protected override void GetInputs()
        {
            Console.WriteLine("Enter the values of list A");
            a = Console.ReadLine().Split(" ")
                .Select(s => int.Parse(s))
                .ToArray();

            Console.WriteLine("Enter the values of set B");
            b = Console.ReadLine().Split(" ")
                .Select(s => int.Parse(s))
                .ToArray();
        }

        protected override void CalculateResult()
        {
            Output = string.Join(" ", Sorters<int>.Merge2SortedArrays(a, b));
        }
    }

    private class GetLongestSubSequences : BaseExecutor
    {
        private List<int>? decreasing;
        private List<int>? increasing;
        private List<int>? input;

        protected override void GetInputs()
        {
            Console.WriteLine("Enter elements of array");
            input = Console.ReadLine().Split(' ')
                .Select(s => int.Parse(s))
                .ToList();
        }

        protected override void CalculateResult()
        {
            decreasing = input.LongestDecreasingSubsequence();
            increasing = input.LongestIncreasingSubsequence();
            Output = string.Join(' ', increasing) + '\n' + string.Join(' ', decreasing);
        }
    }

    private class CandidateProteinsFromDNA : BaseExecutor
    {
        private List<ProteinSequence>? _proteins;

        private DnaSequence? _sequence;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the DNAstring");
            _sequence = new DnaSequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            Output = string.Join('\n', _sequence.GetCandidateProteinSequences());
        }
    }

    private class DNeighborhood : BaseExecutor
    {
        private readonly List<AnySequence>? _introns = new();
        private int _d;

        private DnaSequence? _input;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the full sequence");
            _input = new DnaSequence(Console.ReadLine());
            Console.WriteLine("distance allowed");
            _d = int.Parse(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            var neighbors = _input.DNeighborhood(_d);
            Output = string.Join("\n", neighbors);
        }
    }

    private class SplicedDNAToProtein : BaseExecutor
    {
        private readonly List<AnySequence>? _introns = new();

        private DnaSequence? _input;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the full sequence");
            _input = new DnaSequence(Console.ReadLine());

            var input = "z";

            while (true)
            {
                input = Console.ReadLine();
                if (input.Equals("done")) break;
                _introns.Add(new DnaSequence(input));
            }
        }

        protected override void CalculateResult()
        {
            var splicedSequence = _input.RemoveIntrons(_introns);
            var dnaSequence = new DnaSequence(splicedSequence.ToString());
            var rnaSequence = dnaSequence.TranscribeToRNA();
            Output = rnaSequence.GetExpectedProteinString();
        }
    }

    private class HammingFuzzyMatchWithComplement : BaseExecutor
    {
        private string? _input;
        private MismatchKmerCounter? _matcher;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the match sequence, usually ACGT");
            _input = Console.ReadLine();

            Console.WriteLine("Please enter the length of the Kmer");
            var kmerLength = int.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the tolerance");
            var tolerance = int.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the sequence to be analyzed");
            var sequence = new AnySequence(Console.ReadLine());

            _matcher = new MismatchKmerCounter(kmerLength, sequence, tolerance);
        }

        protected override void CalculateResult()
        {
            _matcher.GetKmers(_input);
            Output = string.Join(' ', _matcher.HighestFrequencyKmers);
        }
    }

    private class SequenceAnalysis : BaseExecutor
    {
        private AnySequence? _anySequence;
        private string? _inputString;

        protected override void GetInputs()
        {
            Console.WriteLine("Please Input the string in question");
            _inputString = Console.ReadLine();
        }

        protected override void CalculateResult()
        {
            _anySequence = new AnySequence(_inputString ?? string.Empty);
            Output = _anySequence?.Counts.ToString();
        }
    }

    private class BinarySearchArray : BaseExecutor
    {
        private List<int>? inputs;
        private List<int>? output;
        private List<int>? valuesToCheck;

        protected override void GetInputs()
        {
            Console.WriteLine("Please Input the string in question");
            var inputString = Console.ReadLine();
            inputs = inputString?
                .Split(" ") // Split the string by the delimiter
                .Select(int.Parse) // Convert each substring to an integer
                .ToList();
            var valuesToCheckString = Console.ReadLine();
            valuesToCheck = valuesToCheckString?
                .Split(" ") // Split the string by the delimiter
                .Select(int.Parse) // Convert each substring to an integer
                .ToList();
        }

        protected override void CalculateResult()
        {
            var temp = BinarySearch.GetIndices(inputs, valuesToCheck, true);
            Output = string.Join(" ", temp);
        }
    }

    private class EdgeList : BaseExecutor
    {
        private readonly UndirectedGraph<int> _undirectedGraph = new();
        private readonly List<Tuple<int, int>>? inputs = new();
        private List<int>? output;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input the edges in question");
            var inputString = "";
            while (true)
            {
                inputString = Console.ReadLine();
                if (inputString.Equals("done"))
                    break;

                var temp = inputString?
                    .Split(" ") // Split the string by the delimiter
                    .Select(int.Parse) // Convert each substring to an integer
                    .ToList();
                inputs.Add(new Tuple<int, int>(temp[0], temp[1]));
            }
        }

        protected override void CalculateResult()
        {
            foreach (var input in inputs) _undirectedGraph.Insert(input.Item1, input.Item2);
            var output = _undirectedGraph.GetEdgeList();
            var edgeCounts = new List<int>();
            foreach (var kvp in output) edgeCounts.Add(kvp.Value.Count);

            Output = string.Join(' ', edgeCounts);
        }
    }

    private class MajorityElement : BaseExecutor
    {
        private readonly List<List<int>?>? inputs = new();

        protected override void GetInputs()
        {
            Console.WriteLine("Please add the integer arrays");
            var inputString = "";
            while (true)
            {
                inputString = Console.ReadLine();
                if (inputString.Equals("done"))
                    break;

                inputs.Add(inputString?
                    .Split(" ") // Split the string by the delimiter
                    .Select(int.Parse) // Convert each substring to an integer
                    .ToList());
            }
        }

        protected override void CalculateResult()
        {
            var temp = new List<int>();
            foreach (var input in inputs)
            {
                var val = MajorityElement<int>.SimpleDictionary(input);
                temp.Add(val == 0 ? -1 : val);
            }

            Output = string.Join(' ', temp);
        }
    }

    private class EdgesToMakeTree : BaseExecutor
    {
        private readonly List<Tuple<int, int>>? inputs = new();
        private UndirectedGraph<int>? graph;
        private List<int>? output;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input the size of the array");
            var inputString = Console.ReadLine();
            if (int.TryParse(inputString, out var size)) graph = new UndirectedGraph<int>(size);

            while (true)
            {
                inputString = Console.ReadLine();
                if (inputString.Equals("done"))
                    break;

                var temp = inputString?
                    .Split(" ") // Split the string by the delimiter
                    .Select(int.Parse) // Convert each substring to an integer
                    .ToList();
                inputs.Add(new Tuple<int, int>(temp[0], temp[1]));
            }
        }

        protected override void CalculateResult()
        {
            foreach (var input in inputs) graph.Insert(input.Item1, input.Item2);
            Output = graph.EdgesToMakeTree().ToString();
        }
    }

    private class DoubleDegreeArray : BaseExecutor
    {
        private readonly List<Tuple<int, int>>? inputs = new();
        private UndirectedGraph<int>? graph;
        private List<int>? output;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input the size of the array");
            var inputString = Console.ReadLine();
            if (int.TryParse(inputString, out var size)) graph = new UndirectedGraph<int>(size);

            while (true)
            {
                inputString = Console.ReadLine();
                if (inputString.Equals("done"))
                    break;

                var temp = inputString?
                    .Split(" ") // Split the string by the delimiter
                    .Select(int.Parse) // Convert each substring to an integer
                    .ToList();
                inputs.Add(new Tuple<int, int>(temp[0], temp[1]));
            }
        }

        protected override void CalculateResult()
        {
            foreach (var input in inputs) graph.Insert(input.Item1, input.Item2);
            var array = new int[graph.NumNodes];
            var edgeList = graph.GetEdgeList();
            foreach (var kvp in edgeList)
            {
                var temp = 0;
                foreach (var edge in kvp.Value) temp += edgeList[edge].Count;
                array[kvp.Key - 1] = temp;
            }

            Output = string.Join(" ", array);
        }
    }

    private class ClumpFinder : BaseExecutor
    {
        private AnySequence? _a;

        private int _kmerLength;
        private int _minCount;
        private int _windowSize;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the sequence");
            _a = new AnySequence(Console.ReadLine());
            Console.WriteLine("Please enter the expected Length");
            _kmerLength = int.Parse(Console.ReadLine());

            Console.WriteLine("Window Size");
            _windowSize = int.Parse(Console.ReadLine());

            Console.WriteLine("Minimum Count");
            _minCount = int.Parse(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            var clumpCounter = new KmerClumpCounter(_a, _windowSize, _kmerLength, _minCount);
            Output = string.Join(' ', clumpCounter.ValidKmers);
        }
    }

    private class DNAComplement : BaseExecutor
    {
        private DnaSequence? _dnaSequence;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input the DNA in question");
            var inputString = Console.ReadLine();
            if (inputString != null) _dnaSequence = new DnaSequence(inputString);
        }

        protected override void CalculateResult()
        {
            Output = _dnaSequence?.GetReverseComplement().ToString();
        }
    }

    private class EasterEgg : BaseExecutor
    {
        protected override void CalculateResult()
        {
            StringBuilder theWhy = new();
            theWhy.Append("Welcome to DNA Store, a C# based implementation of everything bioinformatics related. ");
            theWhy.Append(
                "Today is 4/30/2025. At some point of analyzing file streams, I realized I want to focus on the life stream in my spare time. ");
            theWhy.Append("I really do miss Biology and Chemistry. They were my first loves for a reason.");

            Output = theWhy.ToString();
        }

        protected override void GetInputs()
        {
        }
    }

    private class GCContent : BaseExecutor
    {
        private IList<Fasta>? fastas;
        private Fasta? largestGCContent;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input path to file");
            var location = Console.ReadLine();
            if (location != null) fastas = FastaParser.Read(location);
        }

        protected override void CalculateResult()
        {
            largestGCContent = fastas?.Aggregate((i1, i2) => i1.GCContent > i2.GCContent ? i1 : i2);
            Output = string.Format($"{largestGCContent?.Name}\n{largestGCContent?.GCContent * 100}");
        }
    }

    private class RandomStringProbability : BaseExecutor
    {
        private List<double> gcPercentages;
        private DnaSequence sequence;

        protected override void GetInputs()
        {
            Console.WriteLine("Input DNA sequence");
            sequence = new DnaSequence(Console.ReadLine());
            gcPercentages = new List<double>();
            while (true)
            {
                var input = Console.ReadLine();
                if (input.Equals("done")) break;
                gcPercentages.Add(double.Parse(input));
            }
        }

        protected override void CalculateResult()
        {
            var output = new List<double>();
            foreach (var percentage in gcPercentages)
                output.Add(Math.Round(sequence.RandomStringProbability(percentage), 3));

            Output = string.Join(' ', output);
        }
    }

    private class InsertionSortSwaps : BaseExecutor
    {
        private List<int> values;

        protected override void GetInputs()
        {
            values = new List<int>();
            while (true)
            {
                var input = Console.ReadLine();
                if (input.Equals("done")) break;
                values.Add(int.Parse(input));
            }
        }

        protected override void CalculateResult()
        {
            Output = InsertionSorter<int>.NumberOfSwapsInList(values).ToString();
        }
    }

    private class LongestCommonSubsequenceAlignment : BaseExecutor
    {
        private string _a;
        private string _b;

        protected override void GetInputs()
        {
            Console.WriteLine("sequence A");
            _a = Console.ReadLine();
            Console.WriteLine("sequence B");
            _b = Console.ReadLine();
        }

        protected override void CalculateResult()
        {
            Output = AlignmentMatrix.LongestCommonSubSequence(_a, _b);
        }
    }

    private class DeBrujinString : BaseExecutor
    {
        private readonly DeBrujin deBrujin = new();
        private IEnumerable<string> text;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input path to file");
            var location = Console.ReadLine();
            if (location != null) text = File.ReadLines(location);
        }

        protected override void CalculateResult()
        {
            foreach (var item in text) deBrujin.GenerateFromString(item);
            Output = deBrujin.GetEdgeList();
        }
    }

    private class PossibleErrorCorrections : BaseExecutor
    {
        private List<DnaSequence>? _dnaSequence;
        private List<ErrorCorrection>? _errorCorrections;
        private List<Fasta>? fastas;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input path to file");
            var location = Console.ReadLine();
            if (location != null) fastas = FastaParser.Read(location);
            _dnaSequence = fastas.PostProcessAsDNASequence();
        }

        protected override void CalculateResult()
        {
            _errorCorrections = _dnaSequence.GenerateErrorCorrections();
            Output = string.Join('\n', _errorCorrections);
        }
    }

    private class MedianString : BaseExecutor
    {
        private readonly List<DnaSequence>? _dnaSequences = new();
        private int _kmerLength;

        protected override void GetInputs()
        {
            Console.WriteLine("please input kmerLength");
            _kmerLength = int.Parse(Console.ReadLine());
            while (true)
            {
                var input = Console.ReadLine();
                if (input.Equals("done")) break;
                _dnaSequences.Add(new DnaSequence(input));
            }
        }

        protected override void CalculateResult()
        {
            var commonMotifs = _dnaSequences.MedianString(_kmerLength);
            Output = commonMotifs[0];
        }
    }

    private class MotifEnumeration : BaseExecutor
    {
        private readonly List<DnaSequence>? _dnaSequences = new();
        private int _distance;
        private int _kmerLength;

        protected override void GetInputs()
        {
            Console.WriteLine("please input kmerLength");
            _kmerLength = int.Parse(Console.ReadLine());
            Console.WriteLine("please input distance");
            _distance = int.Parse(Console.ReadLine());
            while (true)
            {
                var input = Console.ReadLine();
                if (input.Equals("done")) break;
                _dnaSequences.Add(new DnaSequence(input));
            }
        }

        protected override void CalculateResult()
        {
            var commonMotifs = _dnaSequences.MotifEnumeration(_kmerLength, _distance);
            Output = string.Join(' ', commonMotifs);
        }
    }

    private class KmerComposition : BaseExecutor
    {
        private int[] kmerComposition;
        private int kmerLength;
        private DnaSequence sequence;

        protected override void GetInputs()
        {
            Console.WriteLine("Enter Kmer length to be analyzed");
            kmerLength = int.Parse(Console.ReadLine());

            Console.WriteLine("Please input sequence");
            var inputString = Console.ReadLine();
            sequence = new DnaSequence(inputString);
        }

        protected override void CalculateResult()
        {
            Output = string.Join(' ', sequence.KmerComposition(kmerLength));
        }
    }

    private class KmerCompositionString : BaseExecutor
    {
        private HashSet<string> kmerComposition;
        private int kmerLength;
        private DnaSequence sequence;

        protected override void GetInputs()
        {
            Console.WriteLine("Enter Kmer length to be analyzed");
            kmerLength = int.Parse(Console.ReadLine());

            Console.WriteLine("Please input sequence");
            var inputString = Console.ReadLine();
            sequence = new DnaSequence(inputString);
        }

        protected override void CalculateResult()
        {
            Output = string.Join('\n', sequence.KmerCompositionUniqueString(kmerLength));
        }
    }

    private class HammingDistance : BaseExecutor
    {
        private AnySequence? a;
        private AnySequence? b;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the first sequence");
            a = new AnySequence(Console.ReadLine());
            Console.WriteLine("Please enter the second sequence");
            b = new AnySequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            Output = AnySequence.HammingDistance(a, b).ToString();
        }
    }


    private class LongestCommonSubsequence : BaseExecutor
    {
        private List<Fasta>? _fastas;
        private Bio.Analysis.Types.LongestCommonSubsequence? _result;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input path to file");
            var location = Console.ReadLine();
            if (location != null) _fastas = FastaParser.Read(location);
        }

        protected override void CalculateResult()
        {
            _result = new Bio.Analysis.Types.LongestCommonSubsequence(_fastas);
            Output = _result.GetAnyLongest().ToString();
        }
    }

    private class GreedyStringAssembly : BaseExecutor
    {
        private List<Fasta>? _fastas;
        private DnaSequence _result;
        private List<DnaSequence> _sequences;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input path to file");
            var location = Console.ReadLine();
            if (location != null) _fastas = FastaParser.Read(location);
            _sequences = _fastas.PostProcessAsDNASequence();
        }

        protected override void CalculateResult()
        {
            Output = _sequences.GenerateLongestStringGreedy().ToString();
        }
    }


    private class MinGCSkewLocation : BaseExecutor
    {
        private int[]? output;

        private NucleotideSequence? sequence;

        protected override void GetInputs()
        {
            Console.WriteLine("Enter Sequence To be analyzed");
            sequence = new DnaSequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            Output = string.Join(' ', sequence.CalculateMinPrefixGCSkew());
        }
    }

    private class MotifFinder : BaseExecutor
    {
        private bool _isZeroIndex;
        private AnySequence? a;
        private Motif? b;
        private long[]? result;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the first sequence");
            a = new AnySequence(Console.ReadLine());
            Console.WriteLine("Please enter the motif");
            var motifString = Console.ReadLine();
            Console.WriteLine("Please enter the expected Length");
            var expectedLength = int.Parse(Console.ReadLine());
            b = new Motif(motifString, expectedLength);

            Console.WriteLine("Is Zero Index 'y'");
            var input = Console.ReadLine();
            _isZeroIndex = input.Equals("y", StringComparison.OrdinalIgnoreCase);
        }

        protected override void CalculateResult()
        {
            result = a.MotifLocations(b, _isZeroIndex);
            var indexType = _isZeroIndex ? "Zero" : "One";
            Output = string.Join(" ", result);
        }
    }

    private class GenerateLexicographicKmers : BaseExecutor
    {
        private int k;
        private string? possibleValues;

        private List<string>? result;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the first sequence");
            possibleValues = Console.ReadLine();
            Console.WriteLine("Please enter the size k");
            k = int.Parse(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            result = Probability.GenerateAllKmers(possibleValues, k);
            Output = string.Join('\n', result);
        }
    }

    private class GenerateLexicographicKmersAndSubKmers : BaseExecutor
    {
        private int k;
        private string? possibleValues;

        private List<string>? result;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the first sequence");
            possibleValues = Console.ReadLine();
            Console.WriteLine("Please enter the size k");
            k = int.Parse(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            result = Probability.GenerateAllKmersAndSubKmers(possibleValues, k);
            Output = string.Join("\n", result);
        }
    }

    private class GenerateFrequencyArray : BaseExecutor
    {
        private FrequencyArray? _frequencyArray;
        private int _length;
        private string? _values;
        private AnySequence? a;
        private List<int>? result;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the first sequence");
            a = new AnySequence(Console.ReadLine());
            Console.WriteLine("Please enter the valid values");
            _values = Console.ReadLine();
            Console.WriteLine("Please enter the expected Length");
            _length = int.Parse(Console.ReadLine());
            _frequencyArray = new FrequencyArray(a);
        }

        protected override void CalculateResult()
        {
            Output = string.Join(' ', _frequencyArray.GetFrequencyArrayInLexicographicOrder(_values, _length));
        }
    }

    private class OverlapGraphExecutor : BaseExecutor
    {
        private IList<Fasta>? _fastas;
        private OverlapGraph? _overlapGraph;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input path to file");
            var location = Console.ReadLine();
            if (location != null) _fastas = FastaParser.Read(location);
        }

        protected override void CalculateResult()
        {
            _overlapGraph = new OverlapGraph(_fastas, 3);
            var sb = new StringBuilder();
            foreach (var tuple in _overlapGraph.GetOverlaps())
                sb.Append(tuple.Item1.Name + " " + tuple.Item2.Name);
            Output = sb.ToString();
        }
    }


    private class PercentDominant : BaseExecutor
    {
        private uint k;
        private uint m;
        private uint n;
        private double output;

        protected override void GetInputs()
        {
            Console.WriteLine("k");
            var inputString = Console.ReadLine();
            k = uint.Parse(inputString);
            Console.WriteLine("m");
            inputString = Console.ReadLine();
            m = uint.Parse(inputString);
            Console.WriteLine("n");
            inputString = Console.ReadLine();
            n = uint.Parse(inputString);
        }

        protected override void CalculateResult()
        {
            Output = Probability.PercentDominant(k, m, n).ToString();
        }
    }


    private class Permutations : BaseExecutor
    {
        private int total;

        private IEnumerable<IEnumerable<int>>? values;

        protected override void GetInputs()
        {
            Console.WriteLine("Input Number");
            total = int.Parse(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            values = Probability.GetPermutations(Enumerable.Range(1, total), total);
            var sb = new StringBuilder();
            sb.Append(values.Count());
            sb.AppendLine();
            foreach (var row in values) sb.Append(string.Join(" ", row));
        }
    }

    private class ProfileMatrixExecutor : BaseExecutor
    {
        private IList<Fasta>? fastas;
        private ProfileMatrix<DnaSequence>? matrix;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input path to file");
            var location = Console.ReadLine();
            if (location != null) fastas = FastaParser.Read(location);
        }

        protected override void CalculateResult()
        {
            matrix = new ProfileMatrix<DnaSequence>(fastas);
            Output = matrix.GetProfileSequence().ToString() + '\n' + matrix.FrequencyMatrix();
        }
    }

    private class DistanceMatrix : BaseExecutor
    {
        private List<Fasta>? fastas;
        private List<List<double>>? matrix;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input path to file");
            var location = Console.ReadLine();
            if (location != null) fastas = FastaParser.Read(location);
        }

        protected override void CalculateResult()
        {
            matrix = fastas.GenerateDistanceMatrix();
            var sb = new StringBuilder();
            for (var i = 0; i < matrix.Count; i++) sb.Append(string.Join(" ", matrix[i]));

            Output = sb.ToString();
        }
    }

    private class ProteinMotifFinder : BaseExecutor
    {
        private readonly List<string> inputNames = new();

        private readonly List<long[]> output = new();
        private List<AnySequence> sequencesToCompare = new();

        protected override void GetInputs()
        {
            // TODO: at some point get a better pattern
            sequencesToCompare = new List<AnySequence>();
            while (true)
            {
                Console.WriteLine("Type the UniProt Protein for motif. type 'complete' when ready to start analysis");
                var input = Console.ReadLine();

                if (input.Equals("done", StringComparison.InvariantCultureIgnoreCase)) break;

                // TODO: async all the way
                var seq = UniprotClient.GetAsync(input).Result;

                inputNames.Add(input);
                sequencesToCompare.Add(new AnySequence(seq));
            }
        }

        protected override void CalculateResult()
        {
            foreach (var seq in sequencesToCompare)
                output.Add(seq.MotifLocations(KnownMotifs.NGlycostatin));

            var sb = new StringBuilder();

            for (var i = 0; i < sequencesToCompare.Count; i++)
                if (output[i].Length > 0)
                {
                    sb.Append($"{inputNames[i]}");
                    sb.Append($"{string.Join(" ", output[i])}");
                }

            Output = sb.ToString();
        }
    }


    private class ProteinToNumRNACount : BaseExecutor
    {
        private Motif? b;

        private ProteinSequence? protein;
        private long result;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the first sequence");
            protein = new ProteinSequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            Output = protein.NumberOfPossibleRNA().ToString();
        }
    }

    private class ProteinWeight : BaseExecutor
    {
        private ProteinSequence? _protein;

        private double _result;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input the protein string");
            _protein = new ProteinSequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            Output = _protein.MolecularWeight.ToString();
        }
    }


    private class RestrictionSites : BaseExecutor
    {
        private List<Tuple<int, int>>? output;

        private DnaSequence? sequence;

        protected override void GetInputs()
        {
            Console.WriteLine("Enter Sequence To be analyzed");
            sequence = new DnaSequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            output = sequence.RestrictionSites();
            var sb = new StringBuilder();
            foreach (var tuple in output)
            {
                sb.Append($"{tuple.Item1} {tuple.Item2}");
                sb.AppendLine();
            }
        }
    }


    private class TranscribeDna : IExecutor
    {
        private DnaSequence? _dnaSequence;

        public void Run()
        {
            GetInputs();
            OutputResult();
        }

        private void GetInputs()
        {
            Console.WriteLine("Please input the DNA in question");
            var inputString = Console.ReadLine();
            if (inputString != null) _dnaSequence = new DnaSequence(inputString);
        }

        private void OutputResult()
        {
            Console.WriteLine(_dnaSequence?.TranscribeToRNA());
        }
    }


    private class TranslateRNA : BaseExecutor
    {
        private RNASequence? _a;
        private string? _result;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the first sequence");
            _a = new RNASequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            Output = _a.GetExpectedProteinString();
        }
    }
}