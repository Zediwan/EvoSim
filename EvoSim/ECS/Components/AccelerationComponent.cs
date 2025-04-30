using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public class AccelerationComponent : IComponent
{
    public float AX { get; set; }
    public float AY { get; set; }

    public float TotalAccelerationSquared => AX * AX + AY * AY;
}

