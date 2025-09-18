using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

/// <summary>
/// Represents a system that updates the positions of entities within a bounded world, ensuring their positions wrap
/// around when they exceed the defined boundaries.
/// </summary>
/// <remarks>This system operates on entities that have a <see cref="PositionComponent"/> and ensures their
/// positions remain within the bounds defined by the specified width and height. It is designed to be used within an
/// ECS (Entity Component System) framework.</remarks>
/// <param name="width"></param>
/// <param name="height"></param>
public class PositionSystem : ISystem
{
    public int Width;
    public int Height;

    public PositionSystem(int width, int height)
    {
        if (width <= 0)
            throw new ArgumentOutOfRangeException(nameof(width), width, "Width must be larger than 0.");
        if (height <= 0)
            throw new ArgumentOutOfRangeException(nameof(height), height, "Height must be larger than 0.");

        Width = width;
        Height = height;
    }


    /// <summary>
    /// Updates the positions of all entities with a <see cref="PositionComponent"/> in the specified ECS engine, 
    /// ensuring their positions are wrapped around within the defined boundaries.
    /// </summary>
    /// <remarks>This method processes all entities in the provided ECS engine that have a <see
    /// cref="PositionComponent"/>  and adjusts their positions to ensure they remain within the bounds defined by the
    /// width and height of the world.</remarks>
    /// <param name="world">The ECS engine containing the entities to update. Must not be <see langword="null"/>.</param>
    /// <param name="deltaTime">The time elapsed since the last update, in seconds. This parameter is currently unused.</param>
    public void Update(EcsEngine world, float deltaTime)
    {
        foreach (var entity in world.GetEntitiesWith(typeof(PositionComponent)))
        {
            PositionUtility.ApplyWraparound(entity.GetComponent<PositionComponent>(), Width, Height);
        }
    }
}
