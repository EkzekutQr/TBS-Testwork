using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffectAbility
{
    public Color StatusEffectIconColor { get; }
    void ApplyStatusEffect(Unit target);
}
