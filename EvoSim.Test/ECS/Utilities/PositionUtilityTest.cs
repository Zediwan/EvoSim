using EvoSim.ECS.Components;
using EvoSim.ECS.Utilities;

namespace EvoSim.Test.ECS.Utilities;

public class PositionUtilityTest
{
    public static IEnumerable<object[]> ApplyWraparoundTestData => new List<object[]>
    {
        new object[] { 100, 100, 
            new PositionComponent { X =  50, Y =  50 }, 
            new PositionComponent { X =  50, Y =  50 }
        },
        new object[] { 100, 100, 
            new PositionComponent { X = -10, Y = -10 },
            new PositionComponent { X =  90, Y =  90 }
        },
        new object[] { 100, 100, 
            new PositionComponent { X =  10, Y = -10 }, 
            new PositionComponent { X =  10, Y =  90 }
        },
        new object[] { 100, 100, 
            new PositionComponent { X = -10, Y =  10 }, 
            new PositionComponent { X =  90, Y =  10 }
        },
        new object[] { 100, 100, 
            new PositionComponent { X = 110, Y = 110 }, 
            new PositionComponent { X =  10, Y =  10 }
        },
        new object[] { 100, 100, 
            new PositionComponent { X =  10, Y = 110 }, 
            new PositionComponent { X =  10, Y =  10 }
        },
        new object[] { 100, 100, 
            new PositionComponent { X = 110, Y =  10 }, 
            new PositionComponent { X =  10, Y =  10 }
        },
        new object[] { 100, 100, 
            new PositionComponent { X = -10, Y = 110 }, 
            new PositionComponent { X =  90, Y =  10 }
        },
        new object[] { 100, 100, 
            new PositionComponent { X = 110, Y = -10 }, 
            new PositionComponent { X =  10, Y =  90 }
        },
        new object[] { 100, 100, 
            new PositionComponent { X = 110, Y = 120 }, 
            new PositionComponent { X =  10, Y =  20 }
        },
    };

    public static IEnumerable<object[]> ApplyWraparoundDataInvalidWorldSizes => new List<object[]>
    {
        new object[] { 100,   0, new PositionComponent { X =  10, Y =   0 }, new PositionComponent { X =  10, Y =   0 } }, // Invalid world height (0), no wraparound
        new object[] { 100,   0, new PositionComponent { X = -10, Y =   0 }, new PositionComponent { X = -10, Y =   0 } }, // Invalid world height (0), negative out of bounds x but skip wraparound
        new object[] { 100,   0, new PositionComponent { X = 110, Y =   0 }, new PositionComponent { X = 110, Y =   0 } }, // Invalid world height (0), positive out of bounds x but skip wraparound
        new object[] { 100,   0, new PositionComponent { X =  10, Y = -10 }, new PositionComponent { X =  10, Y = -10 } }, // Invalid world height (0), negative y but skip wraparound
        new object[] { 100,   0, new PositionComponent { X =  10, Y =  10 }, new PositionComponent { X =  10, Y =  10 } }, // Invalid world height (0), positive out of bounds y but skip wraparound
        new object[] {   0, 100, new PositionComponent { X =   0, Y =  10 }, new PositionComponent { X =   0, Y =  10 } }, // Invalid world width (0), no wraparound
        new object[] {   0, 100, new PositionComponent { X = -10, Y =  10 }, new PositionComponent { X = -10, Y =  10 } }, // Invalid world height (0), negative out of bounds x but skip wraparound
        new object[] {   0, 100, new PositionComponent { X =  10, Y =  10 }, new PositionComponent { X =  10, Y =  10 } }, // Invalid world height (0), positive out of bounds x but skip wraparound
        new object[] {   0, 100, new PositionComponent { X =   0, Y = -10 }, new PositionComponent { X =   0, Y = -10 } }, // Invalid world height (0), negative out of bounds y but skip wraparound
        new object[] {   0, 100, new PositionComponent { X =   0, Y = 110 }, new PositionComponent { X =   0, Y = 110 } }, // Invalid world height (0), positive out of bounds y but skip wraparound
        new object[] {   0,   0, new PositionComponent { X =   0, Y =   0 }, new PositionComponent { X =   0, Y =   0 } }, // Invalid world width and height (0), no wraparound
        new object[] {   0,   0, new PositionComponent { X = -10, Y =   0 }, new PositionComponent { X = -10, Y =   0 } }, // Invalid world width and height (0), negative out of bounds x but skip wraparound
        new object[] {   0,   0, new PositionComponent { X =  10, Y =   0 }, new PositionComponent { X =  10, Y =   0 } }, // Invalid world width and height (0), positive out of bounds x but skip wraparound
        new object[] {   0,   0, new PositionComponent { X =   0, Y = -10 }, new PositionComponent { X =   0, Y = -10 } }, // Invalid world width and height (0), negative out of bounds y but skip wraparound
        new object[] {   0,   0, new PositionComponent { X =   0, Y =  10 }, new PositionComponent { X =   0, Y =  10 } }, // Invalid world width and height (0), positive out of bounds y but skip wraparound
        new object[] { 100, -10, new PositionComponent { X =  10, Y =   0 }, new PositionComponent { X =  10, Y =   0 } }, // Invalid (negative) world height, no wraparound
        new object[] { 100, -10, new PositionComponent { X = -20, Y =   0 }, new PositionComponent { X = -20, Y =   0 } }, // Invalid (negative) world height, negative out of bounds x but skip wraparound
        new object[] { 100, -10, new PositionComponent { X = 120, Y =   0 }, new PositionComponent { X = 120, Y =   0 } }, // Invalid (negative) world height, positive out of bounds x but skip wraparound
        new object[] { 100, -10, new PositionComponent { X =  10, Y = -10 }, new PositionComponent { X =  10, Y = -10 } }, // Invalid (negative) world height, negative out of bounds y but skip wraparound
        new object[] { 100, -10, new PositionComponent { X =  10, Y = 110 }, new PositionComponent { X =  10, Y = 110 } }, // Invalid (negative) world height, positive out of bounds y but skip wraparound
        new object[] { -10, 100, new PositionComponent { X =   0, Y =  10 }, new PositionComponent { X =   0, Y =  10 } }, // Invalid (negative) world width, no wraparound
        new object[] { -10, 100, new PositionComponent { X = -20, Y =  10 }, new PositionComponent { X = -20, Y =  10 } }, // Invalid (negative) world width, negative out of bounds x but skip wraparound
        new object[] { -10, 100, new PositionComponent { X = 120, Y =  10 }, new PositionComponent { X = 120, Y =  10 } }, // Invalid (negative) world width, positive out of bounds x but skip wraparound
        new object[] { -10, 100, new PositionComponent { X =   0, Y = -10 }, new PositionComponent { X =   0, Y = -10 } }, // Invalid (negative) world width, negative out of bounds y but skip wraparound
        new object[] { -10, 100, new PositionComponent { X =   0, Y = 110 }, new PositionComponent { X =   0, Y = 110 } }, // Invalid (negative) world width, positive out of bounds y but skip wraparound
        new object[] { -10, -10, new PositionComponent { X =   0, Y =   0 }, new PositionComponent { X =   0, Y =   0 } }, // Invalid (negative) world width and height, no wraparound
        new object[] { -10, -10, new PositionComponent { X = -10, Y =   0 }, new PositionComponent { X = -10, Y =   0 } }, // Invalid (negative) world width and height, negative out of bounds x but skip wraparound
        new object[] { -10, -10, new PositionComponent { X =  10, Y =   0 }, new PositionComponent { X =  10, Y =   0 } }, // Invalid (negative) world width and height, positive out of bounds x but skip wraparound
        new object[] { -10, -10, new PositionComponent { X =   0, Y = -10 }, new PositionComponent { X =   0, Y = -10 } }, // Invalid (negative) world width and height, negative out of bounds y but skip wraparound
        new object[] { -10, -10, new PositionComponent { X =   0, Y =  10 }, new PositionComponent { X =   0, Y =  10 } }, // Invalid (negative) world width and height, positive out of bounds y but skip wraparound
        new object[] { 100,   0, new PositionComponent { X =  10, Y =   0 }, new PositionComponent { X =  10, Y =   0 } }, // Invalid world height (0), no wraparound
        new object[] { 100,   0, new PositionComponent { X = -10, Y =   0 }, new PositionComponent { X = -10, Y =   0 } }, // Invalid world height (0), negative out of bounds x but skip wraparound
        new object[] { 100,   0, new PositionComponent { X = 110, Y =   0 }, new PositionComponent { X = 110, Y =   0 } }, // Invalid world height (0), positive out of bounds x but skip wraparound
        new object[] { 100,   0, new PositionComponent { X =  10, Y = -10 }, new PositionComponent { X =  10, Y = -10 } }, // Invalid world height (0), negative y but skip wraparound
        new object[] { 100,   0, new PositionComponent { X =  10, Y =  10 }, new PositionComponent { X =  10, Y =  10 } }, // Invalid world height (0), positive out of bounds y but skip wraparound
        new object[] {   0, 100, new PositionComponent { X =   0, Y =  10 }, new PositionComponent { X =   0, Y =  10 } }, // Invalid world width (0), no wraparound
        new object[] {   0, 100, new PositionComponent { X = -10, Y =  10 }, new PositionComponent { X = -10, Y =  10 } }, // Invalid world height (0), negative out of bounds x but skip wraparound
        new object[] {   0, 100, new PositionComponent { X =  10, Y =  10 }, new PositionComponent { X =  10, Y =  10 } }, // Invalid world height (0), positive out of bounds x but skip wraparound
        new object[] {   0, 100, new PositionComponent { X =   0, Y = -10 }, new PositionComponent { X =   0, Y = -10 } }, // Invalid world height (0), negative out of bounds y but skip wraparound
        new object[] {   0, 100, new PositionComponent { X =   0, Y = 110 }, new PositionComponent { X =   0, Y = 110 } }, // Invalid world height (0), positive out of bounds y but skip wraparound
        new object[] {   0,   0, new PositionComponent { X =   0, Y =   0 }, new PositionComponent { X =   0, Y =   0 } }, // Invalid world width and height (0), no wraparound
        new object[] {   0,   0, new PositionComponent { X = -10, Y =   0 }, new PositionComponent { X = -10, Y =   0 } }, // Invalid world width and height (0), negative out of bounds x but skip wraparound
        new object[] {   0,   0, new PositionComponent { X =  10, Y =   0 }, new PositionComponent { X =  10, Y =   0 } }, // Invalid world width and height (0), positive out of bounds x but skip wraparound
        new object[] {   0,   0, new PositionComponent { X =   0, Y = -10 }, new PositionComponent { X =   0, Y = -10 } }, // Invalid world width and height (0), negative out of bounds y but skip wraparound
        new object[] {   0,   0, new PositionComponent { X =   0, Y =  10 }, new PositionComponent { X =   0, Y =  10 } }, // Invalid world width and height (0), positive out of bounds y but skip wraparound
        new object[] { 100, -10, new PositionComponent { X =  10, Y =   0 }, new PositionComponent { X =  10, Y =   0 } }, // Invalid (negative) world height, no wraparound
        new object[] { 100, -10, new PositionComponent { X = -20, Y =   0 }, new PositionComponent { X = -20, Y =   0 } }, // Invalid (negative) world height, negative out of bounds x but skip wraparound
        new object[] { 100, -10, new PositionComponent { X = 120, Y =   0 }, new PositionComponent { X = 120, Y =   0 } }, // Invalid (negative) world height, positive out of bounds x but skip wraparound
        new object[] { 100, -10, new PositionComponent { X =  10, Y = -10 }, new PositionComponent { X =  10, Y = -10 } }, // Invalid (negative) world height, negative out of bounds y but skip wraparound
        new object[] { 100, -10, new PositionComponent { X =  10, Y = 110 }, new PositionComponent { X =  10, Y = 110 } }, // Invalid (negative) world height, positive out of bounds y but skip wraparound
        new object[] { -10, 100, new PositionComponent { X =   0, Y =  10 }, new PositionComponent { X =   0, Y =  10 } }, // Invalid (negative) world width, no wraparound
        new object[] { -10, 100, new PositionComponent { X = -20, Y =  10 }, new PositionComponent { X = -20, Y =  10 } }, // Invalid (negative) world width, negative out of bounds x but skip wraparound
        new object[] { -10, 100, new PositionComponent { X = 120, Y =  10 }, new PositionComponent { X = 120, Y =  10 } }, // Invalid (negative) world width, positive out of bounds x but skip wraparound
        new object[] { -10, 100, new PositionComponent { X =   0, Y = -10 }, new PositionComponent { X =   0, Y = -10 } }, // Invalid (negative) world width, negative out of bounds y but skip wraparound
        new object[] { -10, 100, new PositionComponent { X =   0, Y = 110 }, new PositionComponent { X =   0, Y = 110 } }, // Invalid (negative) world width, positive out of bounds y but skip wraparound
        new object[] { -10, -10, new PositionComponent { X =   0, Y =   0 }, new PositionComponent { X =   0, Y =   0 } }, // Invalid (negative) world width and height, no wraparound
        new object[] { -10, -10, new PositionComponent { X = -10, Y =   0 }, new PositionComponent { X = -10, Y =   0 } }, // Invalid (negative) world width and height, negative out of bounds x but skip wraparound
        new object[] { -10, -10, new PositionComponent { X =  10, Y =   0 }, new PositionComponent { X =  10, Y =   0 } }, // Invalid (negative) world width and height, positive out of bounds x but skip wraparound
        new object[] { -10, -10, new PositionComponent { X =   0, Y = -10 }, new PositionComponent { X =   0, Y = -10 } }, // Invalid (negative) world width and height, negative out of bounds y but skip wraparound
        new object[] { -10, -10, new PositionComponent { X =   0, Y =  10 }, new PositionComponent { X =   0, Y =  10 } }, // Invalid (negative) world width and height, positive out of bounds y but skip wraparound
    };

    [Theory]
    [MemberData(nameof(ApplyWraparoundTestData))]
    [MemberData(nameof(ApplyWraparoundDataInvalidWorldSizes))]
    public void ApplyWraparoundTest(int worldWidth, int worldHeight, PositionComponent positionComponent, PositionComponent expectedPositionComponent)
    {
        // Act
        PositionUtility.ApplyWraparound(positionComponent, worldWidth, worldHeight);
        // Assert
        Assert.Equal(expectedPositionComponent, positionComponent);
    }
}

