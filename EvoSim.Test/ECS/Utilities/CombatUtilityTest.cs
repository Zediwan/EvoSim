using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.Test.ECS.Utilities;

public class CombatUtilityTest
{
    public class CalculateDamageDealtTest()
    {
        [Theory]
        [InlineData(10, 5, 5)]
        [InlineData(10, 0, 10)]
        public void Should_Hit_When_MoreAttackThanDefense(float attack, float defense, float expected)
        {
            // Arrange
            var ecsEngine = new EcsEngine();

            var attacker = ecsEngine.CreateEntity();
            attacker.AddComponent(new CombatComponent { Attack = attack });

            var defender = ecsEngine.CreateEntity();
            defender.AddComponent(new CombatComponent { Defense = defense });

            // Act
            var result = CombatUtility.CalculateDamageDealt(attacker, defender);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(0, 10)]
        public void Should_NotHit_When_LessOrEqualAttackThanDefense(float attack, float defense)
        {
            // Arrange
            var ecsEngine = new EcsEngine();

            var attacker = ecsEngine.CreateEntity();
            attacker.AddComponent(new CombatComponent { Attack = attack });

            var defender = ecsEngine.CreateEntity();
            defender.AddComponent(new CombatComponent { Defense = defense });

            // Act
            var result = CombatUtility.CalculateDamageDealt(attacker, defender);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Should_NotHit_When_AttackerNoCombatComponent()
        {
            // Arrange
            var ecsEngine = new EcsEngine();

            var attacker = ecsEngine.CreateEntity();
            // Attacker has no CombatComponent

            var defender = ecsEngine.CreateEntity();
            defender.AddComponent(new CombatComponent { Defense = 5 });

            // Act
            var result = CombatUtility.CalculateDamageDealt(attacker, defender);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Should_AutoHit_When_DefenderNoCombatComponent()
        {
            // Arrange
            var ecsEngine = new EcsEngine();

            var attacker = ecsEngine.CreateEntity();
            attacker.AddComponent(new CombatComponent { Attack = 10 });

            var defender = ecsEngine.CreateEntity();
            // Defender has no CombatComponent

            // Act
            var result = CombatUtility.CalculateDamageDealt(attacker, defender);

            // Assert
            Assert.Equal(10, result);
        }
    }
}