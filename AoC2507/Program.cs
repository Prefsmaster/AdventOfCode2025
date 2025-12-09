// AoC Day7: Beam Splitter
var lines = File.ReadAllLines("input.txt");
var lineLen = lines[0].Length;
// Part1

// create an array of characters
var characterarray = new List<char[]>();
foreach (var line in lines)
    characterarray.Add(line.ToCharArray());
// part2: remember how many paths lead to each node
var pathsleadingtoposition = new ulong[lineLen];

// initialisation. 
// place a | under the S
for (var i = 0; i < lineLen; i++)
{
    if (characterarray[0][i] == 'S')
    {
        // Part1: starting beam
        characterarray[1][i] = '|';
        // Part2: only one path leads here...
        pathsleadingtoposition[i] = 1;
        break;
    }
}
// then for each line, place 2 | symbols under each ^ that has a | above it, replace ^ with *.
// count all * with a | above it.
var splitCount = 0;
for (var line = 2; line<characterarray.Count; line += 2)
{
    for (var i = 0; i < lineLen; i++)
    {
        if (characterarray[line-1][i] == '|')
        {
            if (characterarray[line][i] == '^')
            {
                // Part1 count # of splits.
                splitCount++;
                // split the beam left and right downward
                characterarray[line + 1][i - 1] = '|';
                characterarray[line + 1][i + 1] = '|';
                // For part 2:
                // propagate number of paths to reach this posiotion
                // down to left and right, joining beams by adding them 
                pathsleadingtoposition[i - 1] += pathsleadingtoposition[i];
                pathsleadingtoposition[i + 1] += pathsleadingtoposition[i];
                // propagate down zero ways to reach this position
                // beam was split!
                pathsleadingtoposition[i] = 0;
            }
            else
            {
                // just propagate down
                characterarray[line][i] = '|';
                characterarray[line + 1][i] = '|';
            }
        }
    }
}
Console.WriteLine(splitCount);

// part 2 sum all paths leading to the final nodes
var ans2 = 0UL;
for (var i = 0; i < lineLen; i++)
{
    ans2 += pathsleadingtoposition[i];
}

Console.WriteLine(ans2);
// show resulting path diagram
foreach (var res in characterarray)
    Console.WriteLine(new string(res));