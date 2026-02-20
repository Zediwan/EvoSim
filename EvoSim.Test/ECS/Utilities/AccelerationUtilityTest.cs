using EvoSim.ECS.Components;
using EvoSim.ECS.Utilities;

namespace EvoSim.Test.ECS.Utilities;

public class AccelerationUtilityTest
{
    public static IEnumerable<object[]> ApplyAccelerationTestData => new List<object[]>
    {
        new object[] { 1.0f, 
            new AccelerationComponent(AX: 0.0f, AY: 0.0f), 1.0f, 0.0f, 
            new AccelerationComponent(AX: 1.0f, AY: 0.0f)
        },
        new object[] { 1.0f, 
            new AccelerationComponent(AX: 0.0f, AY: 0.0f), 0.0f, 1.0f, 
            new AccelerationComponent(AX: 0.0f, AY: 1.0f)
        },
        new object[] { 1.0f, 
            new AccelerationComponent(AX: 0.0f, AY: 0.0f), 1.0f, 1.0f, 
            new AccelerationComponent(AX: 1.0f, AY: 1.0f)
        },
        new object[] { 1.0f, 
            new AccelerationComponent(AX: 1.0f, AY: 1.0f), 1.0f, 0.0f,
            new AccelerationComponent(AX: 2.0f, AY: 1.0f)
        },
        new object[] { 1.0f,
            new AccelerationComponent(AX: 1.0f, AY: 1.0f), 0.0f, 1.0f,
            new AccelerationComponent(AX: 1.0f, AY: 2.0f)
        },
        new object[] { 1.0f, 
            new AccelerationComponent(AX: 1.0f, AY: 1.0f), 1.0f, 1.0f,
            new AccelerationComponent(AX: 2.0f, AY: 2.0f)
        },
        #region No acceleration
        new object[] { 1.0f, 
            new AccelerationComponent(AX: 0.0f, AY: 0.0f), 0.0f, 0.0f,
            new AccelerationComponent(AX: 0.0f, AY: 0.0f)
        },
        new object[] { 1.0f, 
            new AccelerationComponent(AX: 1.0f, AY: 1.0f), 0.0f, 0.0f,
            new AccelerationComponent(AX: 1.0f, AY: 1.0f)
        },
        #endregion
        #region Zero delta time
        new object[] { 0.0f, 
            new AccelerationComponent(AX: 1.0f, AY: 1.0f), 1.0f, 1.0f,
            new AccelerationComponent(AX: 1.0f, AY: 1.0f)
        }, 
        new object[] { 0.0f, 
            new AccelerationComponent(AX: 0.0f, AY: 0.0f), 1.0f, 1.0f,
            new AccelerationComponent(AX: 0.0f, AY: 0.0f)
        },
        #endregion
        #region Negative delta time
        new object[] { -1.0f, 
            new AccelerationComponent(AX: 1.0f, AY: 1.0f), 1.0f, 1.0f,
            new AccelerationComponent(AX: 1.0f, AY: 1.0f)
        }, 
        new object[] { -1.0f,
            new AccelerationComponent(AX: 0.0f, AY: 0.0f), 1.0f, 1.0f,
            new AccelerationComponent(AX: 0.0f, AY: 0.0f)
        }
        #endregion
    };

    [Theory]
    [MemberData(nameof(ApplyAccelerationTestData))]
    public void ApplyAccelerationTest(float deltaTime, AccelerationComponent accelerationComponent, float aX, float aY,
        AccelerationComponent expectedAccelerationComponent)
    {
        // Act
        AccelerationUtility.ApplyAcceleration(deltaTime, accelerationComponent, aX, aY);

        // Assert
        Assert.Equal(expectedAccelerationComponent, accelerationComponent);
    }

    public static IEnumerable<object[]> ApplyAccelerationToVelocityTestData => new List<object[]>
    {
        new object[] {  1.0f, 
            new AccelerationComponent(), 
            new VelocityComponent(), 
            new VelocityComponent()
        },
        new object[] {  1.0f, 
            new AccelerationComponent(AX: 1.0f, AY: 0.0f), 
            new VelocityComponent(VX: 0.0f, VY: 0.0f),
            new VelocityComponent(VX: 1.0f, VY: 0.0f)
        },
        new object[] {  1.0f, 
            new AccelerationComponent(AX: 0.0f, AY: 1.0f),
            new VelocityComponent(VX: 0.0f, VY: 0.0f),
            new VelocityComponent(VX: 0.0f, VY: 1.0f)
        },
        new object[] {  1.0f, 
            new AccelerationComponent(AX: 1.0f, AY: 1.0f), 
            new VelocityComponent(VX: 0.0f, VY: 0.0f),
            new VelocityComponent(VX: 1.0f, VY: 1.0f)
        },
        new object[] {  1.0f, 
            new AccelerationComponent(AX: 1.0f, AY: 1.0f), 
            new VelocityComponent(VX: 2.0f, VY: 2.0f),
            new VelocityComponent(VX: 3.0f, VY: 3.0f)
        },
        #region Zero delta time
        new object[] {  0.0f, 
            new AccelerationComponent(AX: 1.0f, AY: 1.0f), 
            new VelocityComponent(), 
            new VelocityComponent()
        },
        #endregion
        #region Negative delta time
        new object[] { -1.0f,
            new AccelerationComponent(AX: 1.0f, AY: 1.0f),
            new VelocityComponent(),
            new VelocityComponent()
        },
        #endregion 
    };

    [Theory]
    [MemberData(nameof(ApplyAccelerationToVelocityTestData))]
    public void ApplyAccelerationToVelocityTest(float deltaTime, AccelerationComponent accelerationComponent,
        VelocityComponent velocityComponent, VelocityComponent expectedVelocityComponent)
    {
        // Act
        AccelerationUtility.ApplyAccelerationToVelocity(deltaTime, accelerationComponent, velocityComponent);

        // Assert
        Assert.Equal(expectedVelocityComponent, velocityComponent);
    }

    public static IEnumerable<object[]> ClampAccelerationToMaxTestData =>
        new List<object[]>
        {
            // MaxAcceleration = 0, no clamping
            new object[]
            {
                new AccelerationComponent ( AX: 5, AY: 5, MaxAcceleration: 0 ),
                new AccelerationComponent ( AX: 5, AY: 5, MaxAcceleration: 0 )
            }, 
            // MaxAcceleration < 0, no clamping
            new object[]
            {
                new AccelerationComponent ( AX: 3, AY: 4, MaxAcceleration: -1 ),
                new AccelerationComponent ( AX: 3, AY: 4, MaxAcceleration: 0 )
            }, 
            // TotalAcceleration = 5, MaxAcceleration = 5
            new object[]
            {
                new AccelerationComponent ( AX: 3, AY: 4, MaxAcceleration: 5 ),
                new AccelerationComponent ( AX: 3, AY: 4, MaxAcceleration: 5 )
            }, 
            // TotalAcceleration < MaxAcceleration
            new object[]
            {
                new AccelerationComponent ( AX: 2, AY: 3, MaxAcceleration: 5 ),
                new AccelerationComponent ( AX: 2, AY: 3, MaxAcceleration: 5 )
            }, 
            // TotalAcceleration = 10, MaxAcceleration = 5
            new object[]
            {
                new AccelerationComponent ( AX: 6, AY: 8, MaxAcceleration: 5 ),
                new AccelerationComponent ( AX: 3, AY: 4, MaxAcceleration: 5 )
            } 
        };


    [Theory]
    [MemberData(nameof(ClampAccelerationToMaxTestData))]
    public void ClampAccelerationToMaxTest(AccelerationComponent accelerationComponent,
        AccelerationComponent expectedAccelerationComponent)
    {
        // Act
        AccelerationUtility.ClampAccelerationToMax(accelerationComponent);

        // Assert
        Assert.Equal(expectedAccelerationComponent, accelerationComponent);
    }
}