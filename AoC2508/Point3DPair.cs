public class Point3DPair(Point3D p1, Point3D p2)
{
    public Point3D[] Points { get; set; } = [p1, p2];
    public float Distance { get; set; } = p1.DistanceTo(p2);

    // For debugging, helps to see the pair and distance
    public override string ToString()
        => $"({Points[0].X},{Points[0].Y},{Points[0].Z})+({Points[1].X},{Points[1].Y},{Points[1].Z}) Dist: {Distance}";
}
