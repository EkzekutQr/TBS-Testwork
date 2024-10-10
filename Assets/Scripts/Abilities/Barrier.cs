using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : Ability, INonDamagingAbility, IStatusEffectAbility
{
    [SerializeField] private Color statusEffectIconColor;
    public int blockAmount = 5; // Суммарный блок урона

    public Color StatusEffectIconColor => statusEffectIconColor;

    public Barrier()
    {
        duration = 2; // Длительность барьера
        cooldownTime = 4; // Перезарядка барьера
    }

    public override void Use(Unit user, Unit target)
    {
        if (IsOffCooldown())
        {
            ApplyEffect(user, user);
            StartCooldown();
            StartDuration();
            ApplyStatusEffect(user);
        }
    }

    public void ApplyEffect(Unit user, Unit target)
    {
        user.blockAmount = blockAmount;
    }

    public void ApplyStatusEffect(Unit user)
    {
        user.AddStatusEffect(new StatusEffect(statusEffectIconColor, StatusEffectType.Barrier, duration));
    }
}