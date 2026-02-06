using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;
using System.Diagnostics;

namespace EvoSim.ECS.Utilities;

public static class CombatUtility
{
    /// <summary>
    /// Calculates the Damage an Attacker deals to a Defender
    /// </summary>
    /// <param name="attacker">Entity which attacks</param>
    /// <param name="defender">Entity which defends</param>
    /// <returns>Amount of damage dealt</returns>
    public static float CalculateDamageDealt(Entity attacker, Entity defender)
    {
        Debug.Assert(attacker.HasComponent<CombatComponent>(), $"Entity {attacker.Id} does not have a {nameof(CombatComponent)}.");
        Debug.Assert(defender.HasComponent<CombatComponent>(), $"Entity {defender.Id} does not have a {nameof(CombatComponent)}.");

        // If the attacker does not have a CombatComponent, the attacker cannot deal damage
        if (!attacker.HasComponent<CombatComponent>()) return 0;
        var attackerCombatComponent = attacker.GetComponent<CombatComponent>();

        // If the defender does not have a CombatComponent, the attacker wins by default
        if (!defender.HasComponent<CombatComponent>()) return attackerCombatComponent.Attack;
        var defenderCombatComponent = defender.GetComponent<CombatComponent>();

        // Calculate damage dealt considering attacker's attack and defender's defense
        var damageDealt = attackerCombatComponent.Attack - defenderCombatComponent.Defense;

        // If damage dealt is less than or equal to zero, no damage is inflicted
        if (damageDealt <= 0) return 0;

        return damageDealt;
    }
}