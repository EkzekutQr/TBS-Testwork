using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : Ability, INonDamagingAbility, IStatusEffectAbility
{
    public int blockAmount = 5; // ��������� ���� �����

    public Barrier()
    {
        duration = 2; // ������������ �������
        cooldownTime = 4; // ����������� �������
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
        user.blockAmount = blockAmount;
    }

    public void ApplyStatusEffect(Unit user)
    {
        user.AddStatusEffect(new StatusEffect(StatusEffectType.Barrier, duration));
    }
}