namespace GeometryLib.Tests;

public class Vector3Tests
{
    public Vector3Tests()
    {
    }

    [Theory]
    [MemberData(nameof(LengthTestData))]
    public void Can_get_length(Vector3 vector, double expectedLength)
    {
        Assert.Equal(expectedLength, vector.Length, 6);
    }

    public static TheoryData<Vector3, double> LengthTestData()
    {
        return new TheoryData<Vector3, double>
        {
            { new Vector3(0, 0, 0), 0 },
            { new Vector3(3, 0, 0), 3 },
            { new Vector3(-1, -2, -2), 3 },
            { new Vector3(1, 1, 1), Math.Sqrt(3) },
        };
    }

    [Theory]
    [MemberData(nameof(AddTestData))]
    public void Can_add_vectors(Vector3 a, Vector3 b, Vector3 expected)
    {
        Vector3 result = a.Add(b);
        Assert.Equal(expected, result);
    }

    public static TheoryData<Vector3, Vector3, Vector3> AddTestData()
    {
        return new TheoryData<Vector3, Vector3, Vector3>
        {
            { new Vector3(1, 2, 3), new Vector3(4, 5, 6), new Vector3(5, 7, 9) },
            { new Vector3(0, 0, 0), new Vector3(1, 1, 1), new Vector3(1, 1, 1) },
            { new Vector3(-1, -2, -3), new Vector3(1, 2, 3), new Vector3(0, 0, 0) },
        };
    }

    [Theory]
    [MemberData(nameof(DotProductTestData))]
    public void Can_calculate_dot_product(Vector3 a, Vector3 b, double expected)
    {
        Assert.Equal(expected, a.DotProduct(b), 6);
    }

    public static TheoryData<Vector3, Vector3, double> DotProductTestData()
    {
        return new()
        {
            { new Vector3(1, 0, 0), new Vector3(0, 1, 0), 0 },
            { new Vector3(2, 3, 4), new Vector3(5, 6, 7), 56 },
            { new Vector3(-1, -1, -1), new Vector3(1, 1, 1), -3 },
        };
    }

    [Fact]
    public void Can_normalize_zero_vector()
    {
        Vector3 v = new Vector3(0, 0, 0);
        Vector3 result = v.Normalize();
        Assert.Equal(v, result);
    }

    [Fact]
    public void Can_normalize_unit_vector()
    {
        Vector3 v = new Vector3(0, 1, 0);
        Vector3 result = v.Normalize();
        Assert.Equal(v, result);
    }

    [Fact]
    public void Can_normalize_another_vector()
    {
        Vector3 v = new Vector3(3, 4, 0);
        Vector3 result = v.Normalize();
        Assert.Equal(1, result.Length, 6);
        Assert.Equal(new Vector3(3.0 / 5, 4.0 / 5, 0), result);
    }

    [Fact]
    public void Can_detect_ortogonal_vectors()
    {
        Vector3 a = new Vector3(1, 0, 0);
        Vector3 b = new Vector3(0, 1, 0);
        Assert.True(Vector3.AreOrthogonal(a, b));
    }

    [Fact]
    public void Can_detect_non_ortogonal_vectors()
    {
        Vector3 a = new Vector3(1, 1, 0);
        Vector3 b = new Vector3(1, 0, 0);
        Assert.False(Vector3.AreOrthogonal(a, b));
    }

    [Fact]
    public void Zero_vector_is_ortogonal_to_any_vector()
    {
        Vector3 a = new Vector3(0, 0, 0);
        Vector3 b = new Vector3(1, 2, 3);
        Assert.True(Vector3.AreOrthogonal(a, b));
    }

    [Fact]
    public void Projection_to_zero_vector_is_zero_vector()
    {
        Vector3 a = new Vector3(1, 2, 3);
        Vector3 b = new Vector3(0, 0, 0);
        Vector3 result = a.Project(b);
        Assert.Equal(new Vector3(0, 0, 0), result);
    }

    [Fact]
    public void Projection_to_ortogonal_vector_is_zero_vector()
    {
        Vector3 a = new Vector3(1, 0, 0);
        Vector3 b = new Vector3(0, 1, 0);
        Vector3 result = a.Project(b);
        Assert.Equal(new Vector3(0, 0, 0), result);
    }

    [Fact]
    public void Projection_to_same_vector_is_same_vector()
    {
        Vector3 a = new Vector3(2, 3, 4);
        Vector3 b = new Vector3(2, 3, 4);
        Vector3 result = a.Project(b);
        Assert.Equal(a, result);
    }

    [Fact]
    public void Projection_to_another_vector_calculated_ok()
    {
        Vector3 a = new Vector3(2, 3, 4);
        Vector3 b = new Vector3(1, 0, 0);
        Vector3 expected = new Vector3(2, 0, 0);
        Vector3 result = a.Project(b);
        Assert.Equal(expected, result);
    }
}