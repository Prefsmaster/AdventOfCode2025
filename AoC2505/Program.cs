// AoC day 5: Getting Fresh with ID ranges 

const string fileName = "input.txt";

// Part 1
var file = new StreamReader(fileName);
string? line;
var freshItems = new List<(ulong,ulong)>();
while (!string.IsNullOrEmpty(line = file.ReadLine()))
{
    var parts = line.Split('-');
    freshItems.Add(new(ulong.Parse(parts[0]), ulong.Parse(parts[1])));
}
var freshItemCount = 0; 
while (!string.IsNullOrEmpty(line = file.ReadLine()))
{
    var item = ulong.Parse(line);
    // find item in ranges
    if (freshItems.Where(x => x.Item1 <= item && x.Item2 >= item).Any()) freshItemCount++;
}
Console.WriteLine($"Part1: {freshItemCount}");
file.Close();

// Part 2
file = new StreamReader(fileName);
var rangeDelimiters = new List<(ulong, char)>();
// create a list of start and end values, remembering which is which
// Use 'B' for begin, 'E' for end. We're going to sort them later.
// When 2 values are equal, we want 'B' to appear BEFORE 'E'
while (!string.IsNullOrEmpty(line = file.ReadLine()))
{
    var parts = line.Split('-');
    rangeDelimiters.Add(new(ulong.Parse(parts[0]), 'B'));
    rangeDelimiters.Add(new(ulong.Parse(parts[1]), 'E'));
}
// Sort the values. Sort order is first on Item1, then Item2
// 1234,'B' will end up before 1234,'E'
rangeDelimiters.Sort();

var nestlevel = 0;
var freshIDsCount = 0UL;
ulong rangestart = 0;
// loop through the list. nestlevel 0 indicates start of a range.
// When encountering a new 'B' increment nesting level. If level > 1 this range starts inside another, so startof current range does not change.
// if level == 1 then remember start of range.
// When encountering an 'E', decrement nesting level if level == 0, it ends a unique range. Add it to the total.
// If level != 0 the 'E' closed an embedded range, so keep going!
for (int i = 0; i < rangeDelimiters.Count; i++)
{
    if ((rangeDelimiters[i].Item2 == 'B')&&(++nestlevel == 1))
        rangestart = rangeDelimiters[i].Item1;
    if ((rangeDelimiters[i].Item2 == 'E')&&(--nestlevel == 0))
        freshIDsCount += (rangeDelimiters[i].Item1 - rangestart) + 1UL;
}
Console.WriteLine($"Part2: {freshIDsCount}");
file.Close();
