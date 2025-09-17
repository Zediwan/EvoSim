using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

/// <summary>
/// Represents a color component with red, green, and blue channels, each defined as a value between 0 and 255.
/// </summary>
public class ColorComponent : IComponent
{
    /// <summary>
    /// Gets or sets the red component of the color, represented as a value between 0 and 255..
    /// </summary>
    public byte R { get; set; }
    /// <summary>
    /// Gets or sets the green component of a color, represented as a value between 0 and 255.
    /// </summary>
    public byte G { get; set; }
    /// <summary>
    /// Gets or sets the value of the byte property, represented as a value between 0 and 255..
    /// </summary>
    public byte B { get; set; }
    /// <summary>
    /// Gets the color value as a 32-bit unsigned integer in ARGB format.
    /// </summary>
    public uint ARGB => (uint)((255 << 24) | (R << 16) | (G << 8) | B);
}
