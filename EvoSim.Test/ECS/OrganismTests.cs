using EvoSim.ECS;
using Xunit;

namespace EvoSim.Test.Model;

public class OrganismTests
{
    public class ConstructorTests
    {
        [Fact]
        public void Should_InitializeWithDefaults_When_NoParametersGiven()
        {
            // Arrange
            const int expectedHealthValue = 0;
            const int expectedEnergyValue = 0;

            // Act
            var organism = new Organism();

            // Assert
            Assert.Equal(expectedHealthValue, organism.Health);
            Assert.Equal(expectedEnergyValue, organism.Energy);
        }

        [Fact]
        public void Should_InitializeWithExplicitParameters_When_Provided()
        {
            // Arrange
            const int health = 10;
            const int energy = 10;
            const int expectedHealthValue = health;
            const int expectedEnergyValue = energy;

            // Act
            var organism = new Organism(startingHealth: health, startingEnergy: energy);

            // Assert
            Assert.Equal(expectedHealthValue, organism.Health);
            Assert.Equal(expectedEnergyValue, organism.Energy);
        }

        [Theory]
        [InlineData(10, -10)]
        [InlineData(-10, 10)]
        public void Should_Throw_WhenInitializedWithNegativeParameters(int health, int energy)
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Organism(startingHealth: health, startingEnergy: energy));
        }
    }

    public class HealthTests
    {

    }

    public class TakeDamageTests
    {
        private const int Health = 100;
        private const int Energy = 100;
        Organism _organism = new Organism(startingHealth: Health, startingEnergy: Energy);

        [Fact]
        public void Should_TakeDamage_When_ValidParameters()
        {
            // Arrange
            const int damageTaken = 10;
            var expectedResultingHealth = _organism.Health - damageTaken;
            var expectedResultingEnergy = _organism.Energy;

            // Act
            _organism.TakeDamage(damageTaken);

            // Assert
            Assert.Equal(expectedResultingHealth, _organism.Health);
            Assert.Equal(expectedResultingEnergy, _organism.Energy);
        }

        [Fact]
        public void Should_ClampHealth_When_DamageBiggerThanHealth()
        {
            // Arrange
            const int damageTaken = 200;
            const int expectedResultingHealth = 0;
            int expectedResultingEnergy = _organism.Energy;
            // Act
            _organism.TakeDamage(damageTaken);
            // Assert
            Assert.Equal(expectedResultingHealth, _organism.Health);
            Assert.Equal(expectedResultingEnergy, _organism.Energy);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void Should_Throw_When_ZeroOrNegativeParameters(int damageTaken)
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _organism.TakeDamage(damageTaken));
        }
    }

    public class HealTests
    {
        private const int Health = 100;
        private const int Energy = 100;
        Organism _organism = new Organism(startingHealth: Health, startingEnergy: Energy);

        [Fact]
        public void Should_HealToMax_When_HealingExceedsOrMatchesMax()
        {
            // Arrange
            const int healAmount = 10;
            _organism.SetHealth(_organism.MaxHealth - (healAmount - 1));
            var expectedResultingHealth = _organism.MaxHealth;
            var expectedResultingEnergy = _organism.Energy;
            // Act
            _organism.Heal(healAmount);
            // Assert
            Assert.Equal(expectedResultingHealth, _organism.Health);
            Assert.Equal(expectedResultingEnergy, _organism.Energy);
        }

        [Fact]
        public void Should_Heal_When_ValidParameters()
        {
            // Arrange
            const int healAmount = 10;
            _organism.SetHealth(_organism.MaxHealth - (healAmount + 1));
            var expectedResultingHealth = _organism.Health + healAmount;
            var expectedResultingEnergy = _organism.Energy;
            // Act
            _organism.Heal(healAmount);
            // Assert
            Assert.Equal(expectedResultingHealth, _organism.Health);
            Assert.Equal(expectedResultingEnergy, _organism.Energy);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void Should_Throw_When_ZeroOrNegativeParameters(int healAmount)
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _organism.Heal(healAmount));
        }
    }

    public class GainEnergyTests
    {
        private const int Health = 100;
        private const int Energy = 100;
        Organism _organism = new Organism(startingHealth: Health, startingEnergy: Energy);

        [Fact]
        public void Should_GainEnergyToMax_When_EnergyExceedsOrMatchesMax()
        {
            // Arrange
            const int energyGained = 10;
            _organism.SetEnergy(_organism.MaxEnergy - (energyGained - 1));
            var expectedResultingHealth = _organism.Health;
            var expectedResultingEnergy = _organism.MaxEnergy;
            // Act
            _organism.GainEnergy(energyGained);
            // Assert
            Assert.Equal(expectedResultingHealth, _organism.Health);
            Assert.Equal(expectedResultingEnergy, _organism.Energy);
        }


        [Fact]
        public void Should_GainEnergy_When_ValidParameters()
        {
            // Arrange
            const int energyGained = 10;
            _organism.SetEnergy(_organism.MaxEnergy - (energyGained + 1));
            var expectedResultingHealth = _organism.Health;
            var expectedResultingEnergy = _organism.Energy + energyGained;
            // Act
            _organism.GainEnergy(energyGained);
            // Assert
            Assert.Equal(expectedResultingHealth, _organism.Health);
            Assert.Equal(expectedResultingEnergy, _organism.Energy);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void Should_Throw_When_ZeroOrNegativeParameters(int energyGained)
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _organism.GainEnergy(energyGained));
        }

    }

    public class UseEnergyTests
    {
        private const int Health = 100;
        private const int Energy = 100;
        Organism _organism = new Organism(startingHealth: Health, startingEnergy: Energy);

        [Fact]
        public void Should_UseEnergy_When_ValidParameters()
        {
            // Arrange
            const int energyUsed = 10;
            var expectedResultingHealth = _organism.Health;
            var expectedResultingEnergy = Energy - energyUsed;
            // Act
            _organism.UseEnergy(energyUsed);
            // Assert
            Assert.Equal(expectedResultingHealth, _organism.Health);
            Assert.Equal(expectedResultingEnergy, _organism.Energy);
        }

        [Fact]
        public void Should_ClampEnergyAndUseHealth_When_EnergyUsedExceedsEnergy()
        {
            // Arrange
            const int energyUsed = 200;
            var expectedResultingHealth = Health - (energyUsed - Energy);
            var expectedResultingEnergy = 0;
            // Act
            _organism.UseEnergy(energyUsed);
            // Assert
            Assert.Equal(expectedResultingHealth, _organism.Health);
            Assert.Equal(expectedResultingEnergy, _organism.Energy);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void Should_Throw_When_ZeroOrNegativeParameters(int energyUsed)
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _organism.UseEnergy(energyUsed));
        }
    }

}
