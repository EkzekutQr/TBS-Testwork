using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability, IDamagingAbility, IStatusEffectAbility
{
    public int initialDamage = 5; // Начальный урон
    public int burnDamage = 1; // Урон от горения каждый ход
    [SerializeField] private Color statusEffectIconColor;

    public int Damage => initialDamage;
    public Color StatusEffectIconColor => statusEffectIconColor;

    public Fireball()
    {
        duration = 5; // Длительность горения
        cooldownTime = 6; // Перезарядка огненного шара
    }

    public override void Use(Unit user, Unit target)
    {
        if (IsOffCooldown())
        {
            ApplyDamage(target);
            StartCooldown();
            ApplyStatusEffect(target);
        }
    }

    public void ApplyDamage(Unit target)
    {
        target.ImpactHealth(initialDamage);
    }

    public void ApplyStatusEffect(Unit target)
    {
        var burnEffect = new StatusEffect(statusEffectIconColor, StatusEffectType.Burn, duration, burnDamage);
        target.AddStatusEffect(burnEffect);
    }
}
