namespace GeometryLib;

public struct Vector3(double x, double y, double z)
{
    public double X { get; set; } = x;

    public double Y { get; set; } = y;

    public double Z { get; set; } = z;

    public double Length => Math.Sqrt((X * X) + (Y * Y) + (Z * Z));

    public Vector3 Normalize()
    {
        double length = Length;
        if (length <= double.Epsilon)
        {
            return new Vector3(0, 0, 0);
        }

        return new Vector3(X / length, Y / length, Z / length);
    }

    public Vector3 Add(Vector3 o)
    {
        return new Vector3(X + o.X, Y + o.Y, Z + o.Z);
    }

    public double DotProduct(Vector3 o)
    {
        return X * o.X + Y * o.Y + Z * o.Z;
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    public static bool AreOrthogonal(Vector3 x, Vector3 y)
    {
        return Math.Abs(x.DotProduct(y)) <= double.Epsilon;
    }

    public Vector3 Project(Vector3 o)
    {
        double oLengthSquared = o.DotProduct(o);
        if (oLengthSquared == 0)
        {
            return new Vector3(0, 0, 0);
        }

        double scale = DotProduct(o) / oLengthSquared;
        return new Vector3(o.X * scale, o.Y * scale, o.Z * scale);
    }
}