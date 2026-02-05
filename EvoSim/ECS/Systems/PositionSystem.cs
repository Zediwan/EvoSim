using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

/// <summary>
/// Represents a system that manages the positions of entities within a bounded 2D space.
/// </summary>
/// <remarks>The <see cref="PositionSystem"/> is responsible for updating the positions of entities that have a 
/// <see cref="PositionComponent"/> in an ECS (Entity Component System) engine. It ensures that entity positions  remain
/// within the defined boundaries of the system, wrapping around when necessary. The boundaries are defined  by the <see
/// cref="Width"/> and <see cref="Height"/> properties, which are set during initialization.</remarks>
public class PositionSystem : ISystem
{
    public int Height;
    public int Width;

    /// <summary>
    /// Initializes a new instance of the <see cref="PositionSystem"/> class with the specified dimensions.
    /// </summary>
    /// <param name="width">The width of the position system. Must be greater than 0.</param>
    /// <param name="height">The height of the position system. Must be greater than 0.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="width"/> is less than or equal to 0, or if <paramref name="height"/> is less than or
    /// equal to 0.</exception>
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
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");
        if (deltaTime <= 0) return;

        foreach (var entity in world.GetEntitiesWith(typeof(PositionComponent)))
        {
            PositionUtility.ApplyWraparound(entity.GetComponent<PositionComponent>(), Width, Height);
        }
    }
}