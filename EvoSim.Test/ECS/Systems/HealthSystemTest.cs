using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class HealthSystemTest
{
    [Theory]

    #region Tests with no drain rate set (using default)

    [InlineData(1.0f, 50.0f, 100.0f, false)]
    [InlineData(1.0f, 0.0f, 100.0f, true)]
    [InlineData(1.0f, 50.0f, 50.0f, false)]

    #endregion

    #region Tests with zero delta Time

    [InlineData(0.0f, 50.0f, 100.0f, false)]
    [InlineData(0.0f, 0.0f, 100.0f,
        false)] // Entity should not be removed immediately since Update is not called with a positive deltaTime

    #endregion

    public void UpdateTest(float deltaTime, float initialHealth, float initialMaxHealth, bool shouldBeRemoved)
    {
        // Arrange
        var healthSystem = new HealthSystem();

        var ecsEngine = new EcsEngine();

        var entity1 = ecsEngine.CreateEntity();
        entity1.AddComponent(new HealthComponent(health: initialHealth, maxHealth: initialMaxHealth));

        var entity2 = ecsEngine.CreateEntity();
        entity2.AddComponent(new HealthComponent(health: initialHealth, maxHealth: initialMaxHealth));

        // Act
        healthSystem.Update(ecsEngine, deltaTime);

        // Assert
        if (shouldBeRemoved)
        {
            Assert.DoesNotContain(entity1, ecsEngine.GetEntitiesWith<HealthComponent>());
            Assert.DoesNotContain(entity2, ecsEngine.GetEntitiesWith<HealthComponent>());
        }
        else
        {
            Assert.Contains(entity1, ecsEngine.GetEntitiesWith<HealthComponent>());
            Assert.Contains(entity2, ecsEngine.GetEntitiesWith<HealthComponent>());
        }
    }
}