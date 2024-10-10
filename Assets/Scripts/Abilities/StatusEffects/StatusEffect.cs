using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatusEffect
{
    public StatusEffectType Type { get; private set; }
    public int Duration { get; set; }
    public int Power { get; private set; } // Сила эффекта, например урон от горения
    public Color Color { get; }

    public StatusEffect(Color color, StatusEffectType type, int duration, int power = 0)
    {
        Type = type;
        Duration = duration;
        Power = power;
        Color = color;
    }
}
