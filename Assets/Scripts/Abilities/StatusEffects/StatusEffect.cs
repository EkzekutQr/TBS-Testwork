using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    public StatusEffectType Type { get; private set; }
    public int Duration { get; set; }
    public int Power { get; private set; } // Сила эффекта, например урон от горения

    public StatusEffect(StatusEffectType type, int duration, int power = 0)
    {
        Type = type;
        Duration = duration;
        Power = power;
    }
}
