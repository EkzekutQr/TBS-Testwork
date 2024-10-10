using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability, IDamagingAbility, IStatusEffectAbility
{
    public int initialDamage = 5; // ��������� ����
    public int burnDamage = 1; // ���� �� ������� ������ ���
    [SerializeField] private Color statusEffectIconColor;

    public int Damage => initialDamage;
    public Color StatusEffectIconColor => statusEffectIconColor;

    public Fireball()
    {
        duration = 5; // ������������ �������
        cooldownTime = 6; // ����������� ��������� ����
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
