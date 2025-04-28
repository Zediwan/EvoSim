using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public class ColorComponent : IComponent
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public uint ARGB => (uint)((255 << 24) | (R << 16) | (G << 8) | B);
}
