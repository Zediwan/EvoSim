using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class CombatComponentTest
{
    public class AttackTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        public void Should_SetCorrectly_When_SettingPositiveAttack(float attack)
        {
            // Arrange
            var component = new CombatComponent
            {
                // Act
                Attack = attack
            };
            // Assert
            Assert.Equal(attack, component.Attack);
        }

        [Fact]
        public void Should_ClampToZero_When_SettingNegativeAttack()
        {
            // Arrange
            var component = new CombatComponent
            {
                // Act
                Attack = -10
            };
            // Assert
            Assert.Equal(0, component.Attack);
        }

        [Fact]
        public void Should_InitialiseDefault_When_NoAttackGiven()
        {
            // Arrange & Act
            var component = new CombatComponent();
            // Assert
            Assert.Equal(0, component.Attack);
        }
    }

    public class DefenseTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        public void Should_SetCorrectly_When_SettingPositiveDefense(float defense)
        {
            // Arrange
            var component = new CombatComponent
            {
                // Act
                Defense = defense
            };
            // Assert
            Assert.Equal(defense, component.Defense);
        }

        [Fact]
        public void Should_ClampToZero_When_SettingNegativeDefense()
        {
            // Arrange
            var component = new CombatComponent
            {
                // Act
                Defense = -10
            };
            // Assert
            Assert.Equal(0, component.Defense);
        }

        [Fact]
        public void Should_InitialiseDefault_When_NoDefenseGiven()
        {
            // Arrange & Act
            var component = new CombatComponent();
            // Assert
            Assert.Equal(0, component.Defense);
        }
    }
}