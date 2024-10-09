using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Unit : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int blockAmount;
    public int turnCounter; // Счетчик ходов
    public Ability[] abilities; // Массив способностей

    private List<StatusEffect> activeStatusEffects; // Список активных статус-эффектов

    public virtual void Start()
    {
        currentHealth = maxHealth;
        blockAmount = 0;
        turnCounter = 0; // Инициализация счетчика ходов
        activeStatusEffects = new List<StatusEffect>(); // Инициализация списка статус-эффектов
    }

    public virtual void ImpactHealth(int damage)
    {
        if (blockAmount > 0)
        {
            int damageToBlock = Mathf.Min(damage, blockAmount);
            blockAmount -= damageToBlock;
            damage -= damageToBlock;
        }

        if (damage > 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Attack(Unit target)
    {
        if (abilities.Length > 0)
        {
            abilities[0].Use(this, target); // Применяем первую способность (атака)
        }
    }

    public void UseAbility(int abilityIndex, Unit target)
    {
        if (abilityIndex >= 0 && abilityIndex < abilities.Length)
        {
            abilities[abilityIndex].Use(this, target);
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " погиб");
        Destroy(gameObject);
    }

    public void AddStatusEffect(StatusEffect effect)
    {
        activeStatusEffects.Add(effect);
        StartCoroutine(HandleStatusEffect(effect));
    }

    public bool HasStatusEffect(StatusEffectType type)
    {
        return activeStatusEffects.Exists(effect => effect.Type == type);
    }

    public void RemoveStatusEffect(StatusEffect effect)
    {
        activeStatusEffects.Remove(effect);
    }

    public StatusEffect GetActiveStatusEffect(StatusEffectType type)
    {
        return activeStatusEffects.Find(effect => effect.Type == type);
    }

    private IEnumerator HandleStatusEffect(StatusEffect effect)
    {
        while (effect.Duration > 0)
        {
            switch (effect.Type)
            {
                case StatusEffectType.Burn:
                    ImpactHealth(effect.Power);
                    break;
                    // Другие типы статус-эффектов
            }

            effect.Duration--;
            yield return null; // Ожидание одного хода
        }

        RemoveStatusEffect(effect);
    }

    public void EndTurn()
    {
        turnCounter++;
        foreach (StatusEffect effect in activeStatusEffects)
        {
            effect.Duration--;
            if (effect.Duration <= 0)
            {
                RemoveStatusEffect(effect);
            }
        }

        foreach (Ability ability in abilities)
        {
            ability.ReduceCooldown();
            ability.ReduceDuration();
        }
    }
}