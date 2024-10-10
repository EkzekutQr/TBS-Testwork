using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : Ability, INonDamagingAbility, IStatusEffectAbility
{
    public int healAmount = 2; // ��������������� ��������
    [SerializeField] private Color statusEffectIconColor;

    public Color StatusEffectIconColor => statusEffectIconColor;

    public Regeneration()
    {
        duration = 3; // ������������ �����������
        cooldownTime = 5; // ����������� �����������
    }

    public override void Use(Unit user, Unit target)
    {
        if (IsOffCooldown())
        {
            ApplyEffect(user, target);
            StartCooldown();
            StartDuration();
            ApplyStatusEffect(user);
        }
    }

    public void ApplyEffect(Unit user, Unit target)
    {
        user.ImpactHealth(-healAmount);
    }

    public void ApplyStatusEffect(Unit user)
    {
        user.AddStatusEffect(new StatusEffect(statusEffectIconColor, StatusEffectType.Regeneration, duration, -healAmount));
    }
}
