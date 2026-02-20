namespace EvoSim.ECS.Utilities;

public static class VectorUtility
{
    public static (float, float) Rotate(double x, double y, double angle)
    {
        if (x == 0 && y == 0) return (0, 0);
        if (angle == 0) return ((float)x, (float)y);

        // If angle is negative convert to positive equivalent
        if (angle < 0) angle = 360 + angle;

        // Convert angle to radians
        var radians = angle * (Math.PI / 180);
        var cos = Math.Cos(radians);
        var sin = Math.Sin(radians);
        var newX = (float)(x * cos - y * sin);
        var newY = (float)(x * sin + y * cos);
        return (newX, newY);
    }

    public static (float, float) Scale(double x, double y, double scalar)
    {
        if (scalar == 0) return ((float)x, (float)y);

        return ((float)(x * scalar), (float)(y * scalar));
    }

    public static (float, float) Clamp(double x, double y, double magnitude)
    {
        if (x == 0 && y == 0) return (0, 0);
        if (magnitude == 0) return ((float)x, (float)y);

        var length = MathF.Sqrt((float)x * (float)x + (float)y * (float)y);

        // If the length of the vector is less than or equal to the specified magnitude, return the original vector as a tuple of single-precision floating-point values.
        if (length <= magnitude) return ((float)x, (float)y);

        var scale = magnitude / length;
        return ((float)(x * scale), (float)(y * scale));
    }

    public static (float, float) GetRandomUnitRotationVector(double x, double y, double rotation)
    {
        var (rotatedX, rotatedY) = Rotate(x, y, rotation);
        var (clampedX, clampedY) = Clamp(rotatedX, rotatedY, 1);
        return (clampedX, clampedY);
    }
}