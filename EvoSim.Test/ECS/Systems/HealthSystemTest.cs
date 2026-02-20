using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class HealthSystemTest
{
    public static IEnumerable<object[]> UpdateTestData => new List<object[]>
    {
        new object[] { 1f, new HealthComponent(Health: 50, MaxHealth: 100), false },
        new object[] { 1f, new HealthComponent(Health:  0, MaxHealth: 100), true },
        #region No delta time
        new object[] { 0f, new HealthComponent(Health: 50, MaxHealth: 100), false },
        new object[] { 0f, new HealthComponent(Health:  0, MaxHealth: 100), false },
        #endregion
    };

    [Theory]
    [MemberData(nameof(UpdateTestData))]
    public void UpdateTest(float deltaTime, HealthComponent healthComponent, bool shouldBeRemoved)
    {
        // Arrange
        var healthSystem = new HealthSystem();

        var ecsEngine = new EcsEngine();

        var entity = ecsEngine.CreateEntity();
        entity.AddComponent(healthComponent);

        // Act
        healthSystem.Update(ecsEngine, deltaTime);

        // Assert
        if (shouldBeRemoved)
        {
            Assert.DoesNotContain(entity, ecsEngine.GetEntitiesWith<HealthComponent>());
        }
        else
        {
            Assert.Contains(entity, ecsEngine.GetEntitiesWith<HealthComponent>());
        }
    }
}