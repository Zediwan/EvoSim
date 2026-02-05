using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.Test.ECS.Utilities;

public class EnergyUtilityTest
{
    public class UseEnergyTests()
    {
        [Fact]
        public void Should_UseEnergy_When_ValidParameters()
        {
            // Arrange
            var ecsEngine = new EcsEngine();
            var entity = ecsEngine.CreateEntity();
            entity.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
            const float amountToUse = 10;
            // Act
            EnergyUtility.UseEnergy(entity, amountToUse);
            // Assert
            var energyComponent = entity.GetComponent<EnergyComponent>();
            Assert.Equal(40, energyComponent.Energy);
        }

        [Fact]
        public void Should_OnlyUseEnergy_When_EntityHasHealthComponentAndSufficientEnergy()
        {
            // Arrange
            var ecsEngine = new EcsEngine();
            var entity = ecsEngine.CreateEntity();
            entity.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
            entity.AddComponent(new HealthComponent(maxHealth: 100, health: 50));
            const float amountToUse = 20;
            // Act
            EnergyUtility.UseEnergy(entity, amountToUse);
            // Assert
            var energyComponent = entity.GetComponent<EnergyComponent>();
            var healthComponent = entity.GetComponent<HealthComponent>();
            Assert.Equal(30, energyComponent.Energy);
            Assert.Equal(50, healthComponent.Health);
        }

        [Fact]
        public void Should_UseEnergyAndHealth_When_EntityHasHealthComponentAndInsufficientEnergy()
        {
            // Arrange
            var ecsEngine = new EcsEngine();
            var entity = ecsEngine.CreateEntity();
            entity.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
            entity.AddComponent(new HealthComponent(maxHealth: 100, health: 50));
            const float amountToUse = 60;
            // Act
            EnergyUtility.UseEnergy(entity, amountToUse);
            // Assert
            var energyComponent = entity.GetComponent<EnergyComponent>();
            var healthComponent = entity.GetComponent<HealthComponent>();
            Assert.Equal(0, energyComponent.Energy);
            Assert.Equal(40, healthComponent.Health);
        }

        public class ReleaseTests() : ReleaseTest
        {
            [SkippableFact]
            public void Should_DoNothing_When_EntityDoesNotHaveEnergyComponent()
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                const float amountToUse = 10;
                // Act
                EnergyUtility.UseEnergy(entity, amountToUse);
                // Assert
                Assert.True(true);
            }

            [SkippableTheory]
            [InlineData(0)]
            [InlineData(-10)]
            public void Should_DoNothing_When_AmountToUseIsNegativeOrZero(int amountToUse)
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                var energyComponent = new EnergyComponent(maxEnergy: 100, energy: 50);
                entity.AddComponent(energyComponent);
                // Act
                EnergyUtility.UseEnergy(entity, amountToUse);
                // Assert
                Assert.Equal(50, energyComponent.Energy);
            }
        }

    }

    public class GainEnergyTests()
    {
        [Fact]
        public void Should_GainEnergy_When_ValidParameters()
        {
            // Arrange
            var ecsEngine = new EcsEngine();
            var entity = ecsEngine.CreateEntity();
            entity.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
            const float amountToGain = 20;
            // Act
            EnergyUtility.GainEnergy(entity, amountToGain);
            // Assert
            var energyComponent = entity.GetComponent<EnergyComponent>();
            Assert.Equal(70, energyComponent.Energy);
        }

        public class ReleaseTests() : ReleaseTest
        {
            [SkippableFact]
            public void Should_DoNothing_When_EntityDoesNotHaveEnergyComponent()
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                const float amountToGain = 10;
                // Act
                EnergyUtility.GainEnergy(entity, amountToGain);
                // Assert
                Assert.True(true);
            }

            [SkippableTheory]
            [InlineData(0)]
            [InlineData(-10)]
            public void Should_DoNothing_When_AmountToGainIsNegativeOrZero(int amountToGain)
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                var energyComponent = new EnergyComponent(maxEnergy: 100, energy: 50);
                entity.AddComponent(energyComponent);
                // Act
                EnergyUtility.GainEnergy(entity, amountToGain);
                // Assert
                Assert.Equal(50, energyComponent.Energy);
            }
        }
    }
}

