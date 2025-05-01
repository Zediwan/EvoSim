using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class EnergyComponentTest
{
    public class ConstructorTests
    {
        public class CoreTests
        {
            [Fact]
            public void Should_InitializeWithCorrectValues()
            {
                // Arrange
                float energy = 10, maxEnergy = 20;

                // Act
                var energyComponent = new EnergyComponent(maxEnergy: maxEnergy, energy: energy);

                // Assert
                Assert.Equal(energy, energyComponent.Energy);
                Assert.Equal(maxEnergy, energyComponent.MaxEnergy);
            }
        }

        public class DebugTests : DebugTest
        {
            // Empty for future extension
        }

        public class ReleaseTests : ReleaseTest
        {
            // Empty for future extension
        }
    }

    public class SetterTests
    {
        public class CoreTests
        {
            [Fact]
            public void Should_AllowUpdatingValues()
            {
                // Arrange
                var energyComponent = new EnergyComponent(maxEnergy: 20, energy: 10);
                // Act
                energyComponent.MaxEnergy = 25;
                energyComponent.Energy = 15;
                // Assert
                Assert.Equal(15, energyComponent.Energy);
                Assert.Equal(25, energyComponent.MaxEnergy);
            }

            [Fact]
            public void Should_ClampEnergy_When_MaxEnergySmallerThanEnergy()
            {
                // Arrange
                var energyComponent = new EnergyComponent(maxEnergy: 20, energy: 10);

                // Act
                energyComponent.MaxEnergy = 5;

                // Assert
                Assert.Equal(energyComponent.MaxEnergy, energyComponent.Energy);
            }
        }

        public class DebugTests : DebugTest
        {
            [SkippableFact]
            public void Should_ThrowException_WhenSettingNegativeMaxEnergy()
            {
                // Arrange
                var energyComponent = new EnergyComponent(maxEnergy: 20, energy: 10);
                // Act & Assert
                Assert.Throws<ArgumentOutOfRangeException>(() => energyComponent.MaxEnergy = -5);
            }

            [SkippableFact]
            public void Should_ThrowException_WhenSettingNegativeEnergy()
            {
                // Arrange
                var energyComponent = new EnergyComponent(maxEnergy: 20, energy: 10);
                // Act & Assert
                Assert.Throws<ArgumentOutOfRangeException>(() => energyComponent.Energy = -5);
            }

            [SkippableFact]
            public void Should_ThrowException_WhenSettingEnergyGreaterThanMaxEnergy()
            {
                // Arrange
                var energyComponent = new EnergyComponent(maxEnergy: 20, energy: 10);
                // Act & Assert
                Assert.Throws<ArgumentOutOfRangeException>(() => energyComponent.Energy = 25);
            }
        }

        public class ReleaseTests : ReleaseTest
        {
            [SkippableFact]
            public void Should_ClampEnergy_WhenSettingNegativeEnergy()
            {
                // Arrange
                var energyComponent = new EnergyComponent(maxEnergy: 20, energy: 10);
                // Act
                energyComponent.Energy = -5;
                // Assert
                Assert.Equal(0, energyComponent.Energy);
            }

            [SkippableFact]
            public void Should_ClampEnergy_WhenSettingEnergyGreaterThanMaxEnergy()
            {
                // Arrange
                var energyComponent = new EnergyComponent(maxEnergy: 20, energy: 10);
                // Act
                energyComponent.Energy = 25;
                // Assert
                Assert.Equal(energyComponent.MaxEnergy, energyComponent.Energy);
            }

            [SkippableFact]
            public void Should_ClampMaxEnergy_WhenSettingNegativeMaxEnergy()
            {
                // Arrange
                var energyComponent = new EnergyComponent(maxEnergy: 20, energy: 10);
                // Act
                energyComponent.MaxEnergy = -5;
                // Assert
                Assert.Equal(0, energyComponent.MaxEnergy);
            }
        }
    }
}
