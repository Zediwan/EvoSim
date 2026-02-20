using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public record ColorComponent(
    byte R = 255,
    byte G = 255,
    byte B = 255,
    byte A = 255
) : IComponent
{
    public static ColorComponent White => new() { R = 255, G = 255, B = 255, A = 255 };

    /// <summary>
    /// Red component of the Color, represented as a value between 0 and 255.
    /// </summary>
    public byte R { get; set; } = R;

    /// <summary>
    /// Green component of the Color, represented as a value between 0 and 255.
    /// </summary>
    public byte G { get; set; } = G;

    /// <summary>
    /// Blue component of the Color, represented as a value between 0 and 255.
    /// </summary>
    public byte B { get; set; } = B;

    /// <summary>
    /// Alpha component of the Color, represented as a value between 0 and 255.
    /// </summary>
    public byte A { get; set; } = A;

    /// <summary>
    /// Gets the color value as a 32-bit unsigned integer in ARGB format.
    /// </summary>
    public uint ARGB => (uint)((A << 24) | (R << 16) | (G << 8) | B);
}