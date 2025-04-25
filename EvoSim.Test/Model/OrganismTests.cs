using Xunit;
using EvoSim.Model;

namespace EvoSim.Test.Model;

public class OrganismTests
{
    [Fact]
    public void Should_InitializeWithDefaults_When_NoParametersGiven()
    {
        // Arrange

        // Act
        var organism = new Organism();

        // Assert
        Assert.Equal(0, organism.Health);
        Assert.Equal(0, organism.Energy);
    }

    [Fact]
    public void Should_InitializeWithExplicitParameters_When_Provided()
    {
        // Arrange
        var health = 10;
        var energy = 10;

        // Act
        var organism = new Organism(starting_health: health, starting_energy: energy);

        // Assert
        Assert.Equal(health, organism.Health);
        Assert.Equal(energy, organism.Energy);
    }

    [Theory]
    [InlineData(10,-10)]
    [InlineData(-10, 10)]
    public void Should_Throw_WhenInitializedWithNegativeParameters(int health, int energy)
    {
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Organism(starting_health: health, starting_energy: energy));
    }

    [Fact]
    public void Should_OnlyLooseEnergy_When_SufficientEnergy()
    {
        // Arrange
        var energy = 100;
        var health = 100;
        var energy_used = 10;
        var organism = new Organism(starting_health: health, starting_energy: energy);

        // Act
        organism.Energy -= energy_used;

        // Assert
        Assert.Equal(energy - energy_used, organism.Energy);
        Assert.Equal(health, organism.Health);
    }
}
