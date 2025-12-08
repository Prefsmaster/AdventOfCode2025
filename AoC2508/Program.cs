var lines = File.ReadAllLines("input.txt");

var junctionBoxes = lines.Select(line => line.Split(',').Select(s => ulong.Parse(s)).ToArray())
    .Select(coords => new JunctionBox(coords[0], coords[1], coords[2]));

var pairsByDistance = CreatePairs([.. junctionBoxes]).OrderBy(p => p.Distance).ToArray();

JunctionBoxPair lastConnectedBoxPair = pairsByDistance[0];
var Groups = new List<List<JunctionBox>>();
for (var i=0; i<pairsByDistance.Length; i++)
{
    // Print Part 1 solution after 1000 (for real data) or 10 (testdata) are processed
    if ((i == 1000 && pairsByDistance.Length>1000) || (i == 10 && pairsByDistance.Length < 1000))
    {
        // sort groups by size descending, take top 3, multiply their sizes
        var ordered = Groups.OrderByDescending(g => g.Count).Take(3).ToArray();
        Console.WriteLine($"Part 1: {ordered[0].Count * ordered[1].Count * ordered[2].Count}");
    }

    var pair = pairsByDistance[i];

    // try find box 1 and box 2 of this pair in all groups
    var g1 = Groups.SingleOrDefault(g => g.Contains(pair.Boxes[0]));
    var g2 = Groups.SingleOrDefault(g => g.Contains(pair.Boxes[1]));

    // both not found? create new group containing the two boxes
    if (g1 == null && g2 == null)
    {
        Groups.Add([.. pair.Boxes]);
        lastConnectedBoxPair = pair; // remember last processed pair for Part 2
    }
    else
    {
        // both found? 
        if (g1 != null && g2 != null)
        {
            // if not equal, merge the groups.
            // Otherwise do nothing: boxes are already in 1 group
            if (g1 != g2)
            {
                g1.AddRange(g2);
                Groups.Remove(g2);
                lastConnectedBoxPair = pair; // remember last processed pair for Part 2
            }
        }
        else
        {
            // only one box found in one group. add the other box of the pair to that group.
            g1?.Add(pair.Boxes[1]);
            g2?.Add(pair.Boxes[0]);
            lastConnectedBoxPair = pair; // remember last processed pair for Part 2
        }
    }
}
// multiply X coordinates of last processed pair as solution for Part 2
Console.WriteLine($"Part2: {lastConnectedBoxPair.Boxes[0].X * lastConnectedBoxPair.Boxes[1].X}");

static List<JunctionBoxPair> CreatePairs(JunctionBox[] boxes)
{
    var boxPairs = new List<JunctionBoxPair>();
    for (var i = 0; i < boxes.Length-1; i++)
    {
        for (var j = i + 1; j < boxes.Length; j++)
        {
            boxPairs.Add(new JunctionBoxPair(boxes[i], boxes[j]));
        }
    }
    return boxPairs;
}
