using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class VelocitySystemTest
{
    public static IEnumerable<object[]> UpdateTestData =>
        new List<object[]>
        {
            new object[] { 1.0f, 
                new PositionComponent {  X = 10,     Y = 10 }, 
                new VelocityComponent { VX =  5.0f, VY =  0.0f }, 
                new PositionComponent {  X = 15,     Y = 10 }
            },
            new object[] { 1.0f, 
                new PositionComponent {  X = 20,     Y = 20 }, 
                new VelocityComponent { VX = -5.0f, VY =  0.0f }, 
                new PositionComponent {  X = 15,     Y = 20 }
            },
            new object[] { 1.0f, 
                new PositionComponent {  X = 10,     Y = 10 }, 
                new VelocityComponent { VX =  0.0f, VY =  5.0f }, 
                new PositionComponent {  X = 10,     Y = 15 }
            },
            new object[] { 1.0f, 
                new PositionComponent {  X = 20,     Y = 20 }, 
                new VelocityComponent { VX =  0.0f, VY =  -5.0f }, 
                new PositionComponent {  X = 20,     Y = 15 }
            },
            #region No velocity
            new object[] { 1.0f,
                new PositionComponent {  X = 10,     Y = 10 },
                new VelocityComponent { VX =  0.0f, VY =  0.0f },
                new PositionComponent {  X = 10,     Y = 10 }
            }, 
            new object[] { 1.0f,
                new PositionComponent {  X = 20,     Y = 20 },
                new VelocityComponent { VX =  0.0f, VY =  0.0f },
                new PositionComponent {  X = 20,     Y = 20 }
            },
            #endregion
            #region Zero delta time
            new object[] { 0.0f,
                new PositionComponent {  X = 10,     Y = 10 },
                new VelocityComponent { VX =  5.0f, VY =  0.0f },
                new PositionComponent {  X = 10,     Y = 10 }
            }, 
            new object[] { 0.0f,
                new PositionComponent {  X = 20,     Y = 20 },
                new VelocityComponent { VX = -5.0f, VY =  0.0f },
                new PositionComponent {  X = 20,     Y = 20 }
            }
            #endregion
        };

    [Theory]
    [MemberData(nameof(UpdateTestData))]
    public void UpdateTest(float deltaTime, PositionComponent positionComponent, VelocityComponent velocityComponent, PositionComponent expectedPositionComponent)
    {
        // Arrange
        var velocitySystem = new VelocitySystem();

        var ecsEngine = new EcsEngine();

        var entity = ecsEngine.CreateEntity();
        entity.AddComponent(positionComponent);
        entity.AddComponent(velocityComponent);

        // Act
        velocitySystem.Update(ecsEngine, deltaTime);

        // Assert
        Assert.Equal(expectedPositionComponent, positionComponent);
    }
}