namespace EvoSim.ECS.Utilities;

public static class VectorUtility
{
    /// <summary>
    /// Given a point (x, y) and an angle in degrees, returns the coordinates of the point after rotation around the origin (0,0).
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="angle">Angle in degrees to rotate counterclockwise</param>
    /// <returns></returns>
    public static (float, float) Rotate(double x, double y, double angle)
    {
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

    /// <summary>
    /// Given a vector (x, y) and an angle in degrees, return the vector that needs to be applied to achieve that rotation.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static (float, float) GetRotationVector(double x, double y, double angle)
    {
        var (rotatedX, rotatedY) = Rotate(x, y, angle);
        return ((float)(rotatedX - x), (float)(rotatedY - y));
    }
}
