// AoC 2025 Day 2: ID Validation

var idRanges = File.ReadAllText(@"input.txt").Split(',');
var answer1 = 0L;
var answer2 = 0L;

foreach (var idRange in idRanges)
{
    var firstId = long.Parse(idRange.Split('-')[0]);
    var finalId = long.Parse(idRange.Split('-')[1]);

    for (var v = firstId; v <= finalId; v++)
    {
        answer1 += DoPart1(v);
        answer2 += DoPart2(v);
    }
}
Console.WriteLine($"Answer1: {answer1}");
Console.WriteLine($"Answer2: {answer2}");

static long DoPart1(long v)
{
    var idString = v.ToString();
    var len = idString.Length;
    // old habit.. &1 is faster than a division by 2. in C/C++ you can even write if (len & 1) :-) 
    // chances are the compiler will optimize it anyway, but hey..
    if ((len & 1) != 0) return 0L; // odd length
    // We could precalculate len / 2, but the compiler will probably optimize that too
    // Maybe using Substring would be faster, but I like the range operator..
    return idString[..(len / 2)].Equals(idString[^(len / 2)..]) ? v : 0L;
}

static long DoPart2(long v)
{
    var idString = v.ToString();
    var len = v.ToString().Length;

    for (int partlen = 1; partlen <= len/2; partlen++)
    {
        if (len % partlen != 0 || len / partlen < 2) continue; // not divisible or too few parts
        // leverage LINQ to chunk the string and find count of distinct chunks
        if (idString.Chunk(partlen).Select(x => new string(x)).Distinct().Count() == 1) // all parts are equal
            return v;
    }
    return 0L;
}

// I first used this one to chunk, but since the strings are small I 'optimised' it away 
// into the string.Chunk etc line above.
//static IEnumerable<string> GetStringChunks(string str, int chunkSize) 
//{
//    // strings in this problem are short and contain max 10 chunks, so this could have been done without 'yield'.
//    // But hey.. :-) now we have a reusable function.
//    for (int i = 0; i < str.Length; i += chunkSize)
//        yield return str.Substring(i, chunkSize);
//}

