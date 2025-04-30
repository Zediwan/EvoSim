using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public class VelocityComponent : IComponent
{
    public float DX { get; set; }
    public float DY { get; set; }

    public float TotalVelocitySquared => DX * DX + DY * DY;
}


