using EvoSim.ECS.Components;
using EvoSim.ECS.Utilities;

namespace EvoSim.Test.ECS.Utilities;

public class HealthUtilityTest
{
    public static IEnumerable<object[]> TakeDamageTestData => new List<object[]>
    {
        // Test with no MaxHealth set - health should decrease
        new object[]
        {
            1f,
            new HealthComponent(Health: 100),
            new HealthComponent(Health: 99)
        },
        // Test with MaxHealth set - health should decrease
        new object[]
        {
            1f,
            new HealthComponent(Health: 100, MaxHealth: 100),
            new HealthComponent(Health: 99, MaxHealth: 100)
        },
        // Test with damage amount greater than current health - health should be set to 0
        new object[]
        {
            10f,
            new HealthComponent(Health: 5, MaxHealth: 100),
            new HealthComponent(Health: 0, MaxHealth: 100)
        },
        // Test with damage amount equal to current health - health should be set to 0
        new object[]
        {
            10f,
            new HealthComponent(Health: 10, MaxHealth: 100),
            new HealthComponent(Health: 0, MaxHealth: 100)
        },
        // Test with zero amount to take - health should remain unchanged
        new object[]
        {
            0f,
            new HealthComponent(Health: 99),
            new HealthComponent(Health: 99)
        },
        // Test with negative amount to take - health should remain unchanged
        new object[]
        {
            -10f,
            new HealthComponent(Health: 99),
            new HealthComponent(Health: 99)
        }
    };

    
    [Theory]
    [MemberData(nameof(TakeDamageTestData))]
    public void TakeDamageTest(float damageToTake, HealthComponent healthComponent,
        HealthComponent expectedHealthComponent)
    {
        // Act
        HealthUtility.TakeDamage(healthComponent, damageToTake);

        // Assert
        Assert.Equal(expectedHealthComponent, healthComponent);
    }

    public static IEnumerable<object[]> HealTestData => new List<object[]>
    {
        new object[]
        {
            1f,
            new HealthComponent(Health: 99),
            new HealthComponent(Health: 100)
        },
        new object[]
        {
            1f,
            new HealthComponent(Health: 99, MaxHealth: 100),
            new HealthComponent(Health: 100, MaxHealth: 100)
        },
        // Test with amount to gain that would exceed MaxHealth - health should be capped at MaxHealth
        new object[]
        {
            1f,
            new HealthComponent(Health: 99, MaxHealth: 99),
            new HealthComponent(Health: 99, MaxHealth: 99)
        },
        // Test with amount to gain that would exceed MaxHealth - health should be capped at MaxHealth
        new object[]
        {
            10f,
            new HealthComponent(Health: 90, MaxHealth: 99),
            new HealthComponent(Health: 99, MaxHealth: 99)
        },
        // Test with zero amount to take - health should remain unchanged
        new object[]
        {
            0f,
            new HealthComponent(Health: 99),
            new HealthComponent(Health: 99)
        },
        // Test with negative amount to take - health should remain unchanged
        new object[]
        {
            -10f,
            new HealthComponent(Health: 99),
            new HealthComponent(Health: 99)
        }
    };


    [Theory]
    [MemberData(nameof(HealTestData))]
    public void HealTest(float healAmount, HealthComponent healthComponent,
        HealthComponent expectedHealthComponent)
    {
        // Act
        HealthUtility.Heal(healthComponent, healAmount);

        // Assert
        Assert.Equal(expectedHealthComponent, healthComponent);
    }
}