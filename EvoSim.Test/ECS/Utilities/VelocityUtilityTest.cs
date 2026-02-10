using EvoSim.ECS.Components;
using EvoSim.ECS.Utilities;

namespace EvoSim.Test.ECS.Utilities;

public class VelocityUtilityTest
{
    [Theory]
    [InlineData(10, 20, 5, 5, 15, 25)]
    [InlineData(0, 0, -3, -4, -3, -4)]
    [InlineData(100, 50, 0, 0, 100, 50)]
    [InlineData(5, 5, 2.5f, 2.5f, 7, 7)]
    public void ApplyVelocityToPositionTest(int posX, int posY, float vX, float vY, int expX, int expY)
    {
        // Arrange
        var position = new PositionComponent { X = posX, Y = posY };
        var velocity = new VelocityComponent { VX = vX, VY = vY };

        // Act
        VelocityUtility.ApplyVelocityToPosition(position, velocity, 1);

        // Assert
        Assert.Equal(expX, position.X);
        Assert.Equal(expY, position.Y);
    }

    [Theory]
    [InlineData(5, 5, 0)] // MaxVelocity = 0, no clamping
    [InlineData(3, 4, -1)] // MaxVelocity < 0, no clamping
    public void ClampVelocityToMax_DoesNothing_WhenMaxVelocityIsZeroOrNegative(float vX, float vY, float maxVelocity)
    {
        var velocity = new VelocityComponent { VX = vX, VY = vY, MaxVelocity = maxVelocity };
        VelocityUtility.ClampVelocityToMax(velocity);
        Assert.Equal(vX, velocity.VX);
        Assert.Equal(vY, velocity.VY);
        Assert.Equal(maxVelocity, velocity.MaxVelocity);
    }

    [Theory]
    [InlineData(3, 4, 5)] // TotalVelocity = 5, MaxVelocity = 5
    [InlineData(2, 3, 5)] // TotalVelocity < MaxVelocity
    public void ClampVelocityToMax_DoesNothing_WhenCurrentSpeedIsLessThanOrEqualToMax(float vX, float vY,
        float maxVelocity)
    {
        var velocity = new VelocityComponent { VX = vX, VY = vY, MaxVelocity = maxVelocity };
        VelocityUtility.ClampVelocityToMax(velocity);
        Assert.Equal(vX, velocity.VX);
        Assert.Equal(vY, velocity.VY);
    }

    [Fact]
    public void ClampVelocityToMax_ScalesVelocity_WhenCurrentSpeedExceedsMax()
    {
        var velocity = new VelocityComponent { VX = 6, VY = 8, MaxVelocity = 5 };
        // TotalVelocity = 10
        VelocityUtility.ClampVelocityToMax(velocity);
        float scale = 5f / 10f;
        Assert.Equal(6 * scale, velocity.VX, 3);
        Assert.Equal(8 * scale, velocity.VY, 3);
    }
}