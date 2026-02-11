using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class AccelerationSystemTest
{
    // NOTE: The following test cases are commented out because the update method uses a utility Function that is randomly changing acceleration therefore making it impossible to predict the expected velocity values.
    //[Theory]
    //[InlineData(1f, 1f, 0f, 0f, 1f, 1f, 0f, 0f, 1f)]
    //[InlineData(0f, 1f, 0f, 0f, 1f, 0f, 0f, 0f, 0f)] // Zero delta time should result in no change in velocity
    //[InlineData(1f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f)] // No acceleration should result in no change in velocity
    //public void UpdateTest(float deltaTime, float ax1, float ay1, float ax2, float ay2, float expectedVx1,
    //    float expectedVy1, float expectedVx2, float expectedVy2)
    //{
    //    // Arrange
    //    var accelerationSystem = new AccelerationSystem();

    //    var ecsEngine = new EcsEngine();

    //    var entity1 = ecsEngine.CreateEntity();
    //    entity1.AddComponent(new AccelerationComponent { AX = ax1, AY = ay1 });
    //    entity1.AddComponent(new VelocityComponent());

    //    var entity2 = ecsEngine.CreateEntity();
    //    entity2.AddComponent(new AccelerationComponent { AX = ax2, AY = ay2 });
    //    entity2.AddComponent(new VelocityComponent());

    //    // Act
    //    accelerationSystem.Update(ecsEngine, deltaTime);

    //    // Assert
    //    var velocityComponent1 = entity1.GetComponent<VelocityComponent>();
    //    Assert.Equal(expectedVx1, velocityComponent1.VX);
    //    Assert.Equal(expectedVy1, velocityComponent1.VY);

    //    var velocityComponent2 = entity2.GetComponent<VelocityComponent>();
    //    Assert.Equal(expectedVx2, velocityComponent2.VX);
    //    Assert.Equal(expectedVy2, velocityComponent2.VY);
    //}

    [Fact]
    public void UpdateWithoutVelocityComponentTest()
    {
        // Arrange
        var accelerationSystem = new AccelerationSystem();

        var ecsEngine = new EcsEngine();

        var entity = ecsEngine.CreateEntity();
        entity.AddComponent(new AccelerationComponent() { AX = 10, AY = 10 });

        // Act
        accelerationSystem.Update(ecsEngine, deltaTime: 1.0f);

        // Assert
        var accelerationComponent = entity.GetComponent<AccelerationComponent>();
        Assert.Equal(10, accelerationComponent.AX);
        Assert.Equal(10, accelerationComponent.AY);
    }

    [Fact]
    public void UpdateWithoutAccelerationComponentTest()
    {
        // Arrange
        var velocitySystem = new VelocitySystem();

        var ecsEngine = new EcsEngine();

        var entity = ecsEngine.CreateEntity();
        entity.AddComponent(new VelocityComponent() { VX = 10, VY = 10 });

        // Act
        velocitySystem.Update(ecsEngine, deltaTime: 1.0f);

        // Assert
        var velocityComponent = entity.GetComponent<VelocityComponent>();
        Assert.Equal(10, velocityComponent.VX);
        Assert.Equal(10, velocityComponent.VY);
    }
}