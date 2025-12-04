// AoC 2025 Day 3: battery joltage
var batteryBanks = File.ReadAllLines(@"input.txt");
var answer1 = 0UL;
var answer1b = 0UL;
var answer2 = 0UL;

foreach (var batteryBank in batteryBanks)
{
    // original solution for part 1
    // of course, now we can use the generic solution for the problem
    // needed for part 2 to calculate the Joltage for part 1 as well :-)
    var leftIndex = GetHighestJoltageIndex(batteryBank[..^1]);
    var rightIndex = GetHighestJoltageIndex(batteryBank[(leftIndex + 1)..]) + leftIndex + 1;
    answer1 += (ulong)(batteryBank[leftIndex] - '0') * 10 + (ulong)(batteryBank[rightIndex] - '0');

    answer2 += findHighestNJoltage(batteryBank, 12);

    answer1b += findHighestNJoltage(batteryBank, 2); // :-)
}
Console.WriteLine($"Answer1 original solution: {answer1}");
Console.WriteLine($"Answer1 part 2 algorithm : {answer1b}");
Console.WriteLine($"Answer2: {answer2}");

static ulong findHighestNJoltage(string batteryBank, int N)
{
    var selectedDigits = new bool[batteryBank.Length];

    // N times: find and add most significant unselected, highest digit in least significant available section
    for (var digit = 0; digit < N; digit++)
    {
        // find start of least significant available section
        var i = batteryBank.Length-1;
        while (selectedDigits[i]==true) i--;

        // find highest, most significant digit in that section
        var maxDigit = '0';
        var maxIndex = 0;
        while (i>=0 && !selectedDigits[i])
        {
            if (batteryBank[i]>=maxDigit)
            {
                maxDigit = batteryBank[i];
                maxIndex = i;
            }
            i--;
        }
        // add it to the number
        selectedDigits[maxIndex] = true;
    }
    // convert selected digits to ulong
    // LINQ is so nice for this kind of stuff :-)
    return batteryBank.Where((c, j) => selectedDigits[j]).Aggregate(0UL, (acc, c) => acc * 10 + c - '0');
}

static int GetHighestJoltageIndex(string batteryBank)
{
    char maxJoltage = batteryBank[0];
    var maxIndex = 0;

    for (int i = 1; i < batteryBank.Length; i++)
    {
        if (batteryBank[i]>maxJoltage)
        {
            maxJoltage = batteryBank[i];
            maxIndex = i;
        }
    }
    return maxIndex;
}