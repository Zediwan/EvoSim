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
}