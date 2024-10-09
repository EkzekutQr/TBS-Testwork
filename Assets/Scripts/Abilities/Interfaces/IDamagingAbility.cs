using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagingAbility
{
    int Damage { get; }
    void ApplyDamage(Unit target);
}
