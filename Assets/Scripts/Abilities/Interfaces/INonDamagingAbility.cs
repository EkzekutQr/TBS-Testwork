using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INonDamagingAbility
{
    void ApplyEffect(Unit user, Unit target);
}
