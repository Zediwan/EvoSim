using EvoSim.ECS.Components;
using EvoSim.ECS.Utilities;

namespace EvoSim.Test.ECS.Utilities;

public class VelocityUtilityTest
{
    public static IEnumerable<object[]> ApplyVelocityToPositionTestData =>
        new List<object[]>
        {
            new object[] { 1.0f, 
                new PositionComponent {  X = 10,  Y = 20 }, 
                new VelocityComponent { VX =  5, VY =  5 }, 
                new PositionComponent {  X = 15,  Y = 25 }
            },
            new object[] { 1.0f,
                new PositionComponent {  X =  0,  Y =  0 }, 
                new VelocityComponent { VX = -3, VY = -4 }, 
                new PositionComponent {  X = -3,  Y = -4 }
            },
            new object[] { 1.0f, 
                new PositionComponent {  X = 100,  Y = 50 }, 
                new VelocityComponent { VX =   0, VY =  0 }, 
                new PositionComponent {  X = 100,  Y = 50 }
            },
            new object[] { 1.0f, 
                new PositionComponent {  X = 5,     Y = 5 }, 
                new VelocityComponent { VX = 2.5f, VY = 2.5f }, 
                new PositionComponent {  X = 7,     Y = 7 }
            },
            #region Zero delta time
            new object[] { 0.0f,
                new PositionComponent {  X = 10,  Y = 20 },
                new VelocityComponent { VX =  5, VY =  5 },
                new PositionComponent {  X = 10,  Y = 20 }
            },
            new object[] { 0.0f,
                new PositionComponent {  X =  0,  Y =  0 },
                new VelocityComponent { VX = -3, VY = -4 },
                new PositionComponent {  X =  0,  Y =  0 }
            },
            new object[] { 0.0f,
                new PositionComponent {  X = 5,     Y = 5 },
                new VelocityComponent { VX = 2.5f, VY = 2.5f },
                new PositionComponent {  X = 5,     Y = 5 }
            }
            #endregion
        };

    [Theory]
    [MemberData(nameof(ApplyVelocityToPositionTestData))]
    public void ApplyVelocityToPositionTest(float deltaTime, PositionComponent positionComponent, VelocityComponent velocityComponent, PositionComponent expectedPositionComponent)
    {
        // Act
        VelocityUtility.ApplyVelocityToPosition(deltaTime, positionComponent, velocityComponent);

        // Assert
        Assert.Equal(expectedPositionComponent, positionComponent);
    }

    public static IEnumerable<object[]> ClampVelocityToMaxTestData =>
        new List<object[]>
        {
            // MaxVelocity = 0, no clamping
            new object[]
            {
                new VelocityComponent { VX = 5, VY = 5, MaxVelocity = 0 }, 
                new VelocityComponent { VX = 5, VY = 5, MaxVelocity = 0 }
            }, 
            // MaxVelocity < 0, no clamping
            new object[]
            {
                new VelocityComponent { VX = 3, VY = 4, MaxVelocity = -1 }, 
                new VelocityComponent { VX = 3, VY = 4, MaxVelocity = 0 }
            }, 
            // TotalVelocity = 5, MaxVelocity = 5
            new object[]
            {
                new VelocityComponent { VX = 3, VY = 4, MaxVelocity = 5 }, 
                new VelocityComponent { VX = 3, VY = 4, MaxVelocity = 5 }
            }, 
            // TotalVelocity < MaxVelocity
            new object[]
            {
                new VelocityComponent { VX = 2, VY = 3, MaxVelocity = 5 }, 
                new VelocityComponent { VX = 2, VY = 3, MaxVelocity = 5 }
            }, 
            // TotalVelocity = 10, MaxVelocity = 5
            new object[]
            {
                new VelocityComponent { VX = 6, VY = 8, MaxVelocity = 5 }, 
                new VelocityComponent { VX = 3, VY = 4, MaxVelocity = 5 }
            } 
        };

    [Theory]
    [MemberData(nameof(ClampVelocityToMaxTestData))]
    public void ClampVelocityToMaxTest(VelocityComponent velocityComponent, VelocityComponent expectedVelocityComponent)
    {
        // Act
        VelocityUtility.ClampVelocityToMax(velocityComponent);

        // Assert
        Assert.Equal(expectedVelocityComponent, velocityComponent);
    }
}