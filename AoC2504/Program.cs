// Aoc Day 4: Roll the Paper
var paperGrid = File.ReadAllLines("test.txt");
var H = paperGrid.Length;
var W = paperGrid[0].Length;

var rollPositions = new byte[H+2, W+2];
for (var y = 0; y < H; y++)
    for (var x = 0; x < W; x++)
    {
        // 0 = no roll, 1 = roll present, 2 = roll removed
        rollPositions[y + 1, x + 1] = (byte)((paperGrid[y][x] == '@') ? 1 : 0);
    }

// part 1
var accessibleRolls = 0L;
for (var y = 1; y < H + 1; y++)
    for (var x = 1; x < W + 1; x++)
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
    for (var y = 1; y < H + 1; y++)
        for (var x = 1; x < W + 1; x++)
        {
            if (rollPositions[y, x] != 1) continue;

            var neighbours = 0;
            for (var dy = -1; dy <= 1; dy++)
                for (var dx = -1; dx <= 1; dx++)
                    if (!(dy == 0 && dx == 0) && rollPositions[y + dy, x + dx] == 1) neighbours++;

            if (neighbours < 4)
            {
                rollPositions[y, x] = 2;
                // to speed stuff up, during a pass we can immediately remove rolls that have less than 4 neighbours!
                // this is possible because removing a roll will always make things 'better' for its neighbours!
                // This approach takes 4 passes instead of the 9 uesed in the example :-)
                removedRolls++;
            }
        }
    totalRemovedRolls += removedRolls;
} while (removedRolls > 0);

Console.WriteLine($"Part 2: {totalRemovedRolls} in {iterations} passes!");
