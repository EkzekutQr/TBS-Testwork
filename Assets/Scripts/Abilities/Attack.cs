using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability, IDamagingAbility
{
    public int damage = 8; // Урон атаки
    public int Damage => damage;

    public override void Use(Unit user, Unit target)
    {
        if (IsOffCooldown())
        {
            ApplyDamage(target);
            StartCooldown();
        }
    }

    public void ApplyDamage(Unit target)
    {
        target.ImpactHealth(damage);
    }
}
