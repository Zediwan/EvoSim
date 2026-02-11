using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class VelocitySystemTest
{
    private const float NoMaxVelocity = float.MaxValue;

    [Theory]
    [InlineData(1.0f, 10, 10, 5.0f, 0.0f, 20, 20, -5.0f, 0.0f, 15, 10, 15, 20)]
    [InlineData(1.0f, 10, 10, 0.0f, 5.0f, 20, 20, 0.0f, -5.0f, 10, 15, 20, 15)]
    [InlineData(1.0f, 10, 10, 0.0f, 0.0f, 20, 20, 0.0f, 0.0f, 10, 10, 20,
        20)] // No velocity should not change positions
    [InlineData(0.0f, 10, 10, 5.0f, 0.0f, 20, 20, -5.0f, 0.0f, 10, 10, 20,
        20)] // Update with zero delta time should not change positions
    public void UpdateTest(float deltaTime, int x1, int y1, float vx1, float vy1, int x2, int y2, float vx2, float vy2,
        int expectedPositionX1PostUpdate, int expectedPositionY1PostUpdate, int expectedPositionX2PostUpdate,
        int expectedPositionY2PostUpdate)
    {
        // Arrange
        var velocitySystem = new VelocitySystem();

        var ecsEngine = new EcsEngine();

        var entity1 = ecsEngine.CreateEntity();
        entity1.AddComponent(new PositionComponent { X = x1, Y = y1 });
        entity1.AddComponent(new VelocityComponent { VX = vx1, VY = vy1, MaxVelocity = NoMaxVelocity });

        var entity2 = ecsEngine.CreateEntity();
        entity2.AddComponent(new PositionComponent { X = x2, Y = y2 });
        entity2.AddComponent(new VelocityComponent { VX = vx2, VY = vy2, MaxVelocity = NoMaxVelocity });

        // Act
        velocitySystem.Update(ecsEngine, deltaTime);

        // Assert
        var positionComponent1 = entity1.GetComponent<PositionComponent>();
        Assert.Equal(expectedPositionX1PostUpdate, positionComponent1.X);
        Assert.Equal(expectedPositionY1PostUpdate, positionComponent1.Y);

        var positionComponent2 = entity2.GetComponent<PositionComponent>();
        Assert.Equal(expectedPositionX2PostUpdate, positionComponent2.X);
        Assert.Equal(expectedPositionY2PostUpdate, positionComponent2.Y);
    }

    [Fact]
    public void UpdateWithoutVelocityComponentTest()
    {
        // Arrange
        var velocitySystem = new VelocitySystem();

        var ecsEngine = new EcsEngine();

        var entity = ecsEngine.CreateEntity();
        entity.AddComponent(new PositionComponent { X = 10, Y = 10 });

        // Act
        velocitySystem.Update(ecsEngine, deltaTime: 1.0f);

        // Assert
        var positionComponent = entity.GetComponent<PositionComponent>();
        Assert.Equal(10, positionComponent.X);
        Assert.Equal(10, positionComponent.Y);
    }
}