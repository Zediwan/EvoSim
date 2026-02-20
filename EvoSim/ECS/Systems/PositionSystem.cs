using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class PositionSystem : ISystem
{
    public readonly int Height;
    public readonly int Width;

    public PositionSystem(int width, int height)
    {
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width), width, "Width must be larger than 0.");
        }

        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(height), height, "Height must be larger than 0.");
        }

        Width = width;
        Height = height;
    }

    public void Update(EcsEngine world, float deltaTime)
    {
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");
        if (deltaTime <= 0) return;

        foreach (var entity in world.GetEntitiesWith(typeof(PositionComponent)))
        {
            PositionUtility.ApplyWraparound(entity.GetComponent<PositionComponent>(), Width, Height);
        }
    }
}