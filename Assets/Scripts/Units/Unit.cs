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

    [SerializeField] private List<StatusEffect> activeStatusEffects = new List<StatusEffect>(); // Список активных статус-эффектов

    public List<StatusEffect> ActiveStatusEffects { get => activeStatusEffects; set => activeStatusEffects = value; }

    public virtual void Start()
    {
        currentHealth = maxHealth;
        blockAmount = 0;
        turnCounter = 0; // Инициализация счетчика ходов
        //activeStatusEffects = new List<StatusEffect>(); // Инициализация списка статус-эффектов
    }
    public void Reset()
    {
        currentHealth = maxHealth;
        blockAmount = 0;
        turnCounter = 0;
        activeStatusEffects.Clear();
        foreach (Ability ability in abilities)
        {
            ability.ResetCooldown();
        }
    }

    public virtual void ImpactHealth(int damage)
    {
        if (damage < 0)
        {
            currentHealth = currentHealth - damage;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
            return;
        }
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
            Debug.Log(gameObject.name + " is used - " + abilities[abilityIndex]);
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " погиб");
        //Destroy(gameObject);
    }

    public void AddStatusEffect(StatusEffect effect)
    {
        activeStatusEffects.Add(effect);
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

    private void HandleStatusEffect(StatusEffect effect)
    {
        ImpactHealth(effect.Power);
        Debug.Log(gameObject.name + " эффект " + effect.Type.ToString() + " duration " + effect.Duration);

        //effect.Duration--;

    }
    public void EndTurn()
    {
        turnCounter++;

        List<StatusEffect> effectsToRemove = new List<StatusEffect>();

        foreach (StatusEffect effect in activeStatusEffects)
        {
            HandleStatusEffect(effect);

            effect.Duration--;
            if (effect.Duration <= 0)
            {
                effectsToRemove.Add(effect);
            }
        }

        foreach (StatusEffect effect in effectsToRemove)
        {
            activeStatusEffects.Remove(effect);
            Debug.Log("Removing effect : " + effect.Type.ToString());
        }

        foreach (Ability ability in abilities)
        {
            ability.ReduceCooldown();
            ability.ReduceDuration();
        }
    }
}