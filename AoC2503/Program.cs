// AoC 2025 Day 3: battery joltage
var batteryBanks = File.ReadAllLines(@"input.txt");
var answer1 = 0UL;
var answer2 = 0UL;

foreach (var batteryBank in batteryBanks)
{
    var leftIndex = GetHighestJoltageIndex(batteryBank[..^1]);
    var rightIndex = GetHighestJoltageIndex(batteryBank[(leftIndex + 1)..]) + leftIndex + 1;
    answer1 += (ulong)(batteryBank[leftIndex] - '0') * 10 + (ulong)(batteryBank[rightIndex] - '0');
}
Console.WriteLine($"Answer1: {answer1}\nAnswer2: {answer2}");

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