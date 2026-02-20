using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class AccelerationSystemTest
{
    // TODO: Write tests for the usage of health when not sufficent energy is available.

    public static IEnumerable<object[]> UpdateTestData => new List<object[]>
    {
        new object[] { 1f,
            new AccelerationComponent(AX: 1f, AY: 0f), new VelocityComponent(VX: 0, VY: 0), new EnergyComponent(Energy: 100),
            new AccelerationComponent(AX: 1f, AY: 0f), new VelocityComponent(VX: 1, VY: 0), new EnergyComponent(Energy: 99)
        },
        // Test with no acceleration - velocity and energy should remain unchanged
        new object[] { 1f,
            new AccelerationComponent(AX: 0f, AY: 0f), new VelocityComponent(VX: 0, VY: 0), new EnergyComponent(Energy: 100),
            new AccelerationComponent(AX: 0f, AY: 0f), new VelocityComponent(VX: 0, VY: 0), new EnergyComponent(Energy: 100)
        },
        #region No energy component
        // Velocity should still be updated
        new object[] { 1f,
            new AccelerationComponent(AX: 1f, AY: 0f), new VelocityComponent(VX: 0, VY: 0), null,
            new AccelerationComponent(AX: 1f, AY: 0f), new VelocityComponent(VX: 1, VY: 0), null
        },
        #endregion
        #region No velocity component
        // Acceleration and energy should remain unchanged
        new object[] { 1f,
            new AccelerationComponent(AX: 1f, AY: 0f), null, new EnergyComponent(Energy: 100),
            new AccelerationComponent(AX: 1f, AY: 0f), null, new EnergyComponent(Energy: 100)
        },
        #endregion
        #region No acceleration component
        // Velocity and energy should remain unchanged
        new object[] { 1f,
            null, new VelocityComponent(VX: 0, VY: 0), new EnergyComponent(Energy: 100),
            null, new VelocityComponent(VX: 0, VY: 0), new EnergyComponent(Energy: 100)
        },
        #endregion
        #region No delta time
        // No changes should be made to the components
        new object[] { 0f,
            new AccelerationComponent(AX: 1f, AY: 0f), new VelocityComponent(VX: 0, VY: 0), new EnergyComponent(Energy: 100),
            new AccelerationComponent(AX: 1f, AY: 0f), new VelocityComponent(VX: 0, VY: 0), new EnergyComponent(Energy: 100)
        },
        #endregion
    };

    [Theory, MemberData(nameof(UpdateTestData))]
    public void UpdateTest(float deltaTime, 
        AccelerationComponent? aC, VelocityComponent? vC, EnergyComponent? eC,
        AccelerationComponent expectedAc, VelocityComponent expectedVc, EnergyComponent expectedEc)
    {
        // Arrange
        // Disable random movement for testing by setting MaxRandomMovementRotationAngle and RandomMovementEnabled to false
        var accelerationSystem = new AccelerationSystem { MaxRandomMovementRotationAngle = 0, RandomMovementEnabled = false };

        var ecsEngine = new EcsEngine();

        var entity = ecsEngine.CreateEntity();
        if (aC != null) entity.AddComponent(aC);
        if (vC != null) entity.AddComponent(vC);
        if (eC != null) entity.AddComponent(eC);

        // Act
        accelerationSystem.Update(ecsEngine, deltaTime);

        // Assert
        if (aC != null) Assert.Equal(expectedAc, entity.GetComponent<AccelerationComponent>());
        if (vC != null) Assert.Equal(expectedVc, entity.GetComponent<VelocityComponent>());
        if (eC != null) Assert.Equal(expectedEc, entity.GetComponent<EnergyComponent>());
    }
}