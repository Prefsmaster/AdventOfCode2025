var lines = File.ReadAllLines("input.txt");

var junctionBoxes = new List<Point3D>();
foreach (var line in lines)
{
    var coords = line.Split(',').Select(ulong.Parse).ToArray();
    junctionBoxes.Add(new Point3D(coords[0], coords[1], coords[2]));
}
var pairsPerDistance = CreatePairs([.. junctionBoxes]).OrderBy(p => p.Distance).ToArray();

Point3DPair lastConnected = pairsPerDistance[0];
var Groups = new List<List<Point3D>>();
for (var i=0; i<pairsPerDistance.Length; i++)
{
    // Print Part 1 solution after 1000 (for real data) or 10 (testdata) are processed
    if ((i == 1000 && pairsPerDistance.Length>1000) || (i == 10 && pairsPerDistance.Length < 1000))
    {
        // sort groups by size descending, take top 3, multiply their sizes
        var ordered = Groups.OrderByDescending(g => g.Count).Take(3).ToArray();
        Console.WriteLine($"Part 1: {ordered[0].Count * ordered[1].Count * ordered[2].Count}");
    }

    var pair = pairsPerDistance[i];

    // try find box 1 and box 2 of this pair in all groups
    var g1 = Groups.SingleOrDefault(g => g.Contains(pair.Points[0]));
    var g2 = Groups.SingleOrDefault(g => g.Contains(pair.Points[1]));

    // both not found? create new group containing the two boxes
    if (g1 == null && g2 == null)
    {
        Groups.Add([.. pair.Points]);
        lastConnected = pair; // remember last processed pair for Part 2
    }
    else
    {
        // both found? 
        if (g1 != null && g2 != null)
        {
            // if equal, do nothing otherwise merge the groups
            if (g1 != g2)
            {
                // merge groups
                g1.AddRange(g2);
                Groups.Remove(g2);
                lastConnected = pair; // remember last processed pair for Part 2
            }
        }
        else
        {
            // only one box found in one group. add the other box of the pair to that same group.
            g1?.Add(pair.Points[1]);
            g2?.Add(pair.Points[0]);
            lastConnected = pair; // remember last processed pair for Part 2
        }
    }
}
// multiply X coordinates of last processed pair as solution for Part 2
Console.WriteLine($"Part2: {lastConnected.Points[0].X* lastConnected.Points[1].X}");

static List<Point3DPair> CreatePairs(Point3D[] points)
{
    var allPairs = new List<Point3DPair>();
    for (var i = 0; i < points.Length-1; i++)
    {
        for (var j = i + 1; j < points.Length; j++)
        {
            allPairs.Add(new Point3DPair(points[i], points[j]));
        }
    }
    return allPairs;
}
