using System.Diagnostics;
using System.Text;

using Base.Algorithms;
using Base.DataStructures;

using Bio.Analysis.Types;
using Bio.IO;
using Bio.Sequence.Types;

using BioMath;

using Clients;

namespace DNAStore;

internal class InputProcessor
{
    public static IExecutor GetExecutor(string request)
    {
        return BaseExecutor.GetExecutorFromString(request);
    }

    public interface IExecutor
    {
        /// <summary>
        ///     Executes the request.
        /// </summary>
        void Run();
    }

    private abstract class BaseExecutor : IExecutor
    {
        private Stopwatch? _stopwatch;

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
            return input switch
            {
                "SequenceAnalysis" => new SequenceAnalysis(),
                "DNAToRNA" => new TranscribeDna(),
                "DNAComplement" => new DNAComplement(),
                "GCContent" => new GCContent(),
                "HammingDistance" => new HammingDistance(),
                "TranslateRNA" => new TranslateRNA(),
                "PercentDominant" => new PercentDominant(),
                "Permutations" => new Permutations(),
                "MotifFinder" => new MotifFinder(),
                "ProfileMatrix" => new ProfileMatrixExecutor(),
                "ProteinWeight" => new ProteinWeight(),
                "OverlapGraph" => new OverlapGraphExecutor(),
                "LongestCommonSubsequence" => new LongestCommonSubsequence(),
                "ProteinMotif" => new ProteinMotifFinder(),
                "ProteinToNumRNA" => new ProteinToNumRNACount(),
                "ClumpFinder" => new ClumpFinder(),
                "MinGCSkewLocation" => new MinGCSkewLocation(),
                "RestrictionSites" => new RestrictionSites(),
                "HammingSequenceMatch" => new HammingSequenceMatch(),
                "GenerateLexicographicKmers" => new GenerateLexicographicKmers(),
                "HammingFuzzyMatch" => new HammingFuzzyMatch(),
                "GenerateLexicographicKmersAndSubKmers" => new GenerateLexicographicKmersAndSubKmers(),
                "GenerateFrequencyArray" => new GenerateFrequencyArray(),
                "MaxKmersWithComplementFuzzy" => new HammingFuzzyMatchWithComplement(),
                "CandidateProteinsFromDNA" => new CandidateProteinsFromDNA(),
                "SplicedDNAToProtein" => new SplicedDNAToProtein(),
                "BinarySearchArray" => new BinarySearchArray(),
                "EdgeList" => new EdgeList(),
                "EdgeToMakeTree" => new EdgesToMakeTree(),
                "DistanceMatrix" => new DistanceMatrix(),
                "GetFirstSubsequenceIndices" => new GetFirstSubsequenceIndices(),
                "why" => new EasterEgg(),
                _ => new SequenceAnalysis() // probably safe to do it this way
            };
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
        /// </summary>
        protected abstract void OutputResult();

        private void ReportMetrics()
        {
            Console.WriteLine($"Calculation took: {_stopwatch.ElapsedMilliseconds}ms");
        }
    }


    private class HammingSequenceMatch : BaseExecutor
    {
        private SequenceMatchLocations? _matcher;


        private List<int>? output;

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
            output = _matcher.GetLocations();
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"{string.Join(' ', output)}");
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
            output = _matcher.GetKmers(_input);
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"{string.Join(' ', output)}");
        }
    }

    private class GetFirstSubsequenceIndices : BaseExecutor
    {
        private AnySequence? mainSequence;
        private AnySequence? subSequence;
        private List<int>? output;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter main sequence");
            mainSequence = new AnySequence(Console.ReadLine());

            Console.WriteLine("Please enter the subsequence to be analyzed");
            subSequence = new AnySequence(Console.ReadLine());

        }

        protected override void CalculateResult()
        {
            output = mainSequence.FindFirstPossibleSubSequence(subSequence);
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"{string.Join(' ', output)}");
        }
    }

    private class CandidateProteinsFromDNA : BaseExecutor
    {
        private List<ProteinSequence>? _proteins;

        private DNASequence? _sequence;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the DNAstring");
            _sequence = new DNASequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            _proteins = _sequence.GetCandidateProteinSequences();
        }

        protected override void OutputResult()
        {
            foreach (var protein in _proteins) Console.WriteLine(protein);
        }
    }

    // TODO: this should be a single fasta read
    private class SplicedDNAToProtein : BaseExecutor
    {
        private readonly List<AnySequence>? _introns = new();

        private DNASequence? _input;
        private string? _output;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the full sequence");
            _input = new DNASequence(Console.ReadLine());

            var input = "z";

            while (true)
            {
                input = Console.ReadLine();
                if (input.Equals("done")) break;
                _introns.Add(new DNASequence(input));
            }
        }

        protected override void CalculateResult()
        {
            // TODO: this is terrible, figure out how I want this later
            var splicedSequence = _input.RemoveIntrons(_introns);
            var dnaSequence = new DNASequence(splicedSequence.ToString());
            var rnaSequence = dnaSequence.TranscribeToRNA();
            _output = rnaSequence.GetExpectedProteinString();
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"{_output}");
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
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"{string.Join(' ', _matcher.HighestFrequencyKmers)}");
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
        }

        protected override void OutputResult()
        {
            Console.WriteLine(_anySequence?.Counts);
        }
    }
    private class BinarySearchArray : BaseExecutor
    {
        private List<int>? inputs;
        private List<int>? valuesToCheck;
        private List<int>? output;

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
            output = BinarySearch.GetIndices(inputs, valuesToCheck, true);
        }

        protected override void OutputResult()
        {
            Console.WriteLine(string.Join(" ", output));
        }
    }
    private class EdgeList : BaseExecutor
    {
        private List<Tuple<int, int>>? inputs = new();
        private Graph<int> graph = new();
        private List<int>? output;

        protected override void GetInputs()
        {
            Console.WriteLine("Please Input the string in question");
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
            foreach (var input in inputs)
            {
                graph.Insert(input.Item1, input.Item2);
            }
        }

        protected override void OutputResult()
        {
            var output = graph.GetEdgeList();
            var edgeCounts = new List<int>() { };
            foreach (var kvp in output)
            {
                edgeCounts.Add(kvp.Value.Count);
            }

            Console.WriteLine(string.Join(' ', edgeCounts));
        }
    }
    private class EdgesToMakeTree : BaseExecutor
    {
        private List<Tuple<int, int>>? inputs = new();
        private Graph<int>? graph;
        private List<int>? output;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input the size of the array");
            var inputString = Console.ReadLine();
            if (int.TryParse(inputString, out var size))
            {
                graph = new Graph<int>(size);
            }

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
            foreach (var input in inputs)
            {
                graph.Insert(input.Item1, input.Item2);
            }
        }

        protected override void OutputResult()
        {
            Console.WriteLine(graph.EdgesToMakeTree());
        }
    }

    private class ClumpFinder : BaseExecutor
    {
        private AnySequence? _a;
        private KmerClumpCounter? _clumpCounter;

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
            _clumpCounter = new KmerClumpCounter(_a, _windowSize, _kmerLength, _minCount);
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"{string.Join(' ', _clumpCounter.ValidKmers)}");
        }
    }

    private class DNAComplement : BaseExecutor
    {
        private DNASequence? _dnaSequence;
        private string? output;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input the DNA in question");
            var inputString = Console.ReadLine();
            if (inputString != null) _dnaSequence = new DNASequence(inputString);
        }

        protected override void CalculateResult()
        {
            output = _dnaSequence?.ToReverseComplement().ToString();
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"The complement is {output}");
        }
    }

    private class EasterEgg : BaseExecutor
    {
        /// <summary>
        ///     TODO: for a different day, let's make a new executor here to get rid of these ugly overrides
        /// </summary>
        protected override void CalculateResult()
        {
        }

        protected override void GetInputs()
        {
        }

        protected override void OutputResult()
        {
            StringBuilder theWhy = new();
            theWhy.Append("Welcome to DNA Store, a C# based implementation of everything bioinformatics related. ");
            theWhy.Append(
                "Today is 4/30/2025. At some point of analyzing file streams, I realized I want to focus on the life stream in my spare time. ");
            theWhy.Append("I really do miss Biology and Chemistry. They were my first loves for a reason.");

            Console.WriteLine(theWhy.ToString());
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
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"{largestGCContent?.Name}\n{largestGCContent?.GCContent * 100}");
        }
    }

    private class HammingDistance : BaseExecutor
    {
        private AnySequence? a;
        private AnySequence? b;
        private long result;

        protected override void GetInputs()
        {
            Console.WriteLine("Please enter the first sequence");
            a = new AnySequence(Console.ReadLine());
            Console.WriteLine("Please enter the second sequence");
            b = new AnySequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            result = AnySequence.HammingDistance(a, b);
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"The HammingDistance Distance between both sequences is: {result}");
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
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"A longest common subsequence is: \n{_result.GetAnyLongest()}");
        }
    }

    private class MinGCSkewLocation : BaseExecutor
    {
        private int[]? output;

        private NucleotideSequence? sequence;

        protected override void GetInputs()
        {
            Console.WriteLine("Enter Sequence To be analyzed");
            sequence = new DNASequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            output = sequence.CalculateMinPrefixGCSkew();
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"{string.Join(' ', output)}");
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
        }

        protected override void OutputResult()
        {
            var indexType = _isZeroIndex ? "Zero" : "One";
            Console.WriteLine($"The {indexType}-Index Locations are: \n{string.Join(" ", result)}");
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
        }

        protected override void OutputResult()
        {
            foreach (var possibleValue in result) Console.WriteLine(possibleValue);
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
        }

        protected override void OutputResult()
        {
            foreach (var possibleValue in result) Console.WriteLine(possibleValue);

            File.WriteAllText("./output.txt", string.Join('\n', result));
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
            result = _frequencyArray.GetFrequencyArrayInLexicographicOrder(_values, _length);
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"{string.Join(" ", result)}");
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
        }

        protected override void OutputResult()
        {
            foreach (var tuple in _overlapGraph.GetOverlaps())
                Console.WriteLine(tuple.Item1.Name + " " + tuple.Item2.Name);
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
            output = Probability.PercentDominant(k, m, n);
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"The percent Dominant is {output}");
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
        }

        protected override void OutputResult()
        {
            // Yes, double counting, not good but let's build there
            Console.WriteLine(values.Count());
            foreach (var row in values) Console.WriteLine(string.Join(" ", row));
        }
    }

    private class ProfileMatrixExecutor : BaseExecutor
    {
        private IList<Fasta>? fastas;
        private ProfileMatrix? matrix;

        protected override void GetInputs()
        {
            Console.WriteLine("Please input path to file");
            var location = Console.ReadLine();
            if (location != null) fastas = FastaParser.Read(location);
        }

        protected override void CalculateResult()
        {
            matrix = new ProfileMatrix(fastas);
        }

        protected override void OutputResult()
        {
            Console.WriteLine(matrix.GetProfileSequence());
            Console.WriteLine(matrix.FrequencyMatrix());
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
        }

        protected override void OutputResult()
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                Console.WriteLine(String.Join(" ", matrix[i]));
            }
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
                // TODO, create a generic non try catch for this to prevent nulls/io errors
                var input = Console.ReadLine();

                // TODO: I hate these-- get these as a rule
                if (input.Equals("complete", StringComparison.InvariantCultureIgnoreCase)) break;

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
        }

        protected override void OutputResult()
        {
            for (var i = 0; i < sequencesToCompare.Count; i++)
                if (output[i].Length > 0)
                {
                    Console.WriteLine($"{inputNames[i]}");
                    Console.WriteLine($"{string.Join(" ", output[i])}");
                }
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
            result = protein.NumberOfPossibleRNA();
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"{result}");
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
            _result = _protein.MolecularWeight;
        }

        protected override void OutputResult()
        {
            Console.WriteLine(_result);
        }
    }


    private class RestrictionSites : BaseExecutor
    {
        private List<Tuple<int, int>>? output;

        private DNASequence? sequence;

        protected override void GetInputs()
        {
            Console.WriteLine("Enter Sequence To be analyzed");
            sequence = new DNASequence(Console.ReadLine());
        }

        protected override void CalculateResult()
        {
            output = sequence.RestrictionSites();
        }

        protected override void OutputResult()
        {
            foreach (var tuple in output) Console.WriteLine($"{tuple.Item1} {tuple.Item2}");
        }
    }


    private class TranscribeDna : IExecutor
    {
        private DNASequence? _dnaSequence;

        public void Run()
        {
            GetInputs();
            OutputResult();
        }

        private void GetInputs()
        {
            Console.WriteLine("Please input the DNA in question");
            var inputString = Console.ReadLine();
            if (inputString != null) _dnaSequence = new DNASequence(inputString);
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
            _result = _a.GetExpectedProteinString();
        }

        protected override void OutputResult()
        {
            Console.WriteLine($"The translated protein between both sequences is:\n{_result}");
        }
    }
}