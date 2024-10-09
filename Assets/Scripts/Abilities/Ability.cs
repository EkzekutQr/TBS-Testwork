using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public string abilityName;
    public int cooldownTime; // Перезарядка в базовом классе
    protected int currentCooldown;
    public int duration; // Длительность в базовом классе
    protected int remainingDuration;

    public abstract void Use(Unit user, Unit target);

    public void StartCooldown()
    {
        currentCooldown = cooldownTime;
    }

    public void ReduceCooldown()
    {
        if (currentCooldown > 0)
            currentCooldown--;
    }

    public void StartDuration()
    {
        remainingDuration = duration;
    }

    public void ReduceDuration()
    {
        if (remainingDuration > 0)
            remainingDuration--;
    }

    public bool IsOffCooldown()
    {
        return currentCooldown == 0;
    }

    public bool IsActive()
    {
        return remainingDuration > 0;
    }
}
