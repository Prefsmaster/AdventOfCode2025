public class JunctionBoxPair(JunctionBox p1, JunctionBox p2)
{
    public JunctionBox[] Boxes { get; private set; } = [p1, p2];
    public float Distance { get; private set; } = p1.DistanceTo(p2);

    // For debugging, helps to see the pair and distance
    public override string ToString()
        => $"({Boxes[0].X},{Boxes[0].Y},{Boxes[0].Z})<- {Distance} ->({Boxes[1].X},{Boxes[1].Y},{Boxes[1].Z})";
}
