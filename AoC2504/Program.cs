// Aoc Day 4: Roll the Paper
var paperGrid = File.ReadAllLines("input.txt");
var gridH = paperGrid.Length;
var gridW = paperGrid[0].Length;

// display start situation
Console.WriteLine("before:");
foreach (var paper in paperGrid) Console.WriteLine(paper);

var rollPositions = new byte[gridH+2, gridW+2];
for (var y = 0; y < gridH; y++)
    for (var x = 0; x < gridW; x++)
    {
        // 0 = no roll, 1 = roll present
        rollPositions[y + 1, x + 1] = (byte)((paperGrid[y][x] == '@') ? 1 : 0);
    }

// part 1
var accessibleRolls = 0L;
for (var y = 1; y < gridH + 1; y++)
    for (var x = 1; x < gridW + 1; x++)
    {
        if (rollPositions[y, x] == 0) continue;

        var neighbours = 0;
        for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
                if (!(dy == 00 && dx == 0) && rollPositions[y + dy, x + dx] == 1) neighbours++;
        if (neighbours < 4)
            accessibleRolls++;
    }
Console.WriteLine($"Part 1: {accessibleRolls}");

// part 2
// iteratively remove rolls that have less than 4 neighbours
var totalRemovedRolls = 0L;
int removedRolls;
int iterations = 0;
do
{
    iterations++;
    removedRolls = 0;
    for (var y = 1; y < gridH + 1; y++)
        for (var x = 1; x < gridW + 1; x++)
        {
            if (rollPositions[y, x] == 0) continue;

            var neighbours = 0;
            for (var dy = -1; dy <= 1; dy++)
                for (var dx = -1; dx <= 1; dx++)
                    if (!(dy == 0 && dx == 0) && rollPositions[y + dy, x + dx] == 1) neighbours++;

            if (neighbours < 4)
            {
                // to speed stuff up, during a pass we can immediately remove rolls that have less than 4 neighbours!
                // this is possible because removing a roll will always make things 'better' for its neighbours!
                // This insight reduces the 9 passes from the example to only 4 :-)
                // Now the phrasing in the description: "here is ONE WAY you could remove as many rolls of paper as possible"
                // makes more sense!
                rollPositions[y, x] = 0;
                removedRolls++;
            }
        }
    totalRemovedRolls += removedRolls;
} while (removedRolls > 0);

Console.WriteLine($"Part 2: {totalRemovedRolls} in {iterations} passes.");

// display end situation
Console.WriteLine("after:");
for (var y = 1; y < gridH + 1; y++)
{
    for (var x = 1; x < gridW + 1; x++)
    {
        Console.Write(rollPositions[y, x]==1?'@':'.');
    }
    Console.WriteLine();
}
