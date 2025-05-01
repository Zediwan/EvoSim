using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public class EnergyComponent : IComponent
{
    private float _energy;

    public float Energy
    {
        get => _energy;
        set
        {
#if DEBUG
            if (value > MaxEnergy)
                throw new ArgumentOutOfRangeException(nameof(Energy), "Energy cannot exceed MaxEnergy.");
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(Energy), "Energy cannot be negative.");
#endif
            _energy = Math.Clamp(value, 0, MaxEnergy);
        }
    }
    private float _maxEnergy;

    public float MaxEnergy
    {
        get => _maxEnergy;
        set
        {
#if DEBUG
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(MaxEnergy), "MaxEnergy cannot be negative.");
#endif
            _maxEnergy = Math.Max(0, value);
            if (Energy > MaxEnergy)
                Energy = MaxEnergy;
        }
    }

    public EnergyComponent(float maxEnergy = 0, float energy = 0)
    {
        MaxEnergy = maxEnergy;
        Energy = energy;
    }
}