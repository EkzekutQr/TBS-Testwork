using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purify : Ability, INonDamagingAbility
{
    public Purify()
    {
        cooldownTime = 5; // ����������� ��������
    }

    public override void Use(Unit user, Unit target)
    {
        if (IsOffCooldown())
        {
            ApplyEffect(user, user);
            StartCooldown();
        }
    }

    public void ApplyEffect(Unit user, Unit target)
    {
        var burnEffect = target.GetActiveStatusEffect(StatusEffectType.Burn);
        if (burnEffect != null)
        {
            target.RemoveStatusEffect(burnEffect);
        }
    }
}