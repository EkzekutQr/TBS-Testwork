using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : Ability, INonDamagingAbility, IStatusEffectAbility
{
    [SerializeField] private Color statusEffectIconColor;
    public int blockAmount = 5; // ��������� ���� �����

    public Color StatusEffectIconColor => statusEffectIconColor;

    public Barrier()
    {
        duration = 2; // ������������ �������
        cooldownTime = 4; // ����������� �������
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