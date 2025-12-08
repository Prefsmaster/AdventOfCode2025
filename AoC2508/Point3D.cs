public class Point3D(ulong x, ulong y, ulong z)
{
    public ulong X { get; set; } = x;
    public ulong Y { get; set; } = y;
    public ulong Z { get; set; } = z;

    public float DistanceTo(Point3D other)
    {
        return MathF.Sqrt(
            (X - other.X) * (X - other.X) +
            (Y - other.Y) * (Y - other.Y) +
            (Z - other.Z) * (Z - other.Z)
        );
    }
}
