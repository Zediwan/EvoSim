namespace EvoSim.Model;

public class Organism
{


    #region Variables and Properties

    #region Health Variables and Properties

    internal int MaxHealth => 100;

    private int _health;

    public int Health => _health;

    public bool IsAlive => Health > 0;

    #endregion

    #region Energy Variables and Properties

    private int _energy;

    internal int MaxEnergy => 100;

    public int Energy => _energy;

    #endregion

    #endregion


    #region Constructors and Destructors

    public Organism(int startingHealth = 0, int startingEnergy = 0)
    {
        if (startingHealth < 0)
            throw new ArgumentOutOfRangeException(nameof(startingHealth), "Starting Health must be positive.");
        if (startingHealth > MaxHealth)
            throw new ArgumentOutOfRangeException(nameof(startingHealth),
                $"Starting Health must be smaller or equal to {nameof(MaxEnergy)}: {MaxEnergy}");
        if (startingEnergy < 0)
            throw new ArgumentOutOfRangeException(nameof(startingEnergy), "Starting Energy must be positive.");
        if (startingEnergy > MaxEnergy)
            throw new ArgumentOutOfRangeException(nameof(startingEnergy),
                $"Starting Energy must be smaller or equal to {nameof(MaxEnergy)}: {MaxEnergy}");

        SetHealth(startingHealth);
        SetEnergy(startingEnergy);
    }

    #endregion


    #region Methods

    #region Health

    internal void SetHealth(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be positive.");
        if (value > MaxHealth)
            throw new ArgumentOutOfRangeException(nameof(value), $"Value must be smaller or equal to {nameof(MaxHealth)}: {MaxHealth}");
        _health = value;
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Damage must be positive.");

        SetHealth(Math.Max(Health - amount, 0));
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Heal amount must be positive.");

        SetHealth(Math.Min(Health + amount, MaxHealth));
    }


    #endregion

    #region Energy

    internal void SetEnergy(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be positive.");
        if (value > MaxEnergy)
            throw new ArgumentOutOfRangeException(nameof(value), $"Energy must be smaller or equal to {nameof(MaxEnergy)}: {MaxEnergy}.");
        _energy = value;
    }

    public void UseEnergy(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Energy used must be positive.");

        var newEnergy = Energy - amount;

        if (newEnergy <= 0)
        {
            TakeDamage(-newEnergy);
        }

        SetEnergy(Math.Max(newEnergy, 0));
    }

    public void GainEnergy(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Energy gained must be positive.");

        SetEnergy(Math.Min(Energy + amount, MaxEnergy));
    }

    #endregion

    #endregion


}

