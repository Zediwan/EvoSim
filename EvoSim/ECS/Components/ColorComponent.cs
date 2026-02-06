using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

/// <summary>
/// Represents a color component with red, green, and blue channels, each defined as a value between 0 and 255.
/// </summary>
public class ColorComponent : IComponent
{
    public static ColorComponent White => new() { R = 255, G = 255, B = 255, A = 255 };

    /// <summary>
    /// Red component of the Color, represented as a value between 0 and 255.
    /// </summary>
    public byte R { get; set; } = 255;

    /// <summary>
    /// Green component of the Color, represented as a value between 0 and 255.
    /// </summary>
    public byte G { get; set; } = 255;

    /// <summary>
    /// Blue component of the Color, represented as a value between 0 and 255.
    /// </summary>
    public byte B { get; set; } = 255;

    /// <summary>
    /// Alpha component of the Color, represented as a value between 0 and 255.
    /// </summary>
    public byte A { get; set; } = 255;

    /// <summary>
    /// Gets the color value as a 32-bit unsigned integer in ARGB format.
    /// </summary>
    public uint ARGB => (uint)((A << 24) | (R << 16) | (G << 8) | B);
}
