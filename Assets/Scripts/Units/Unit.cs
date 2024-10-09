using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Unit : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int blockAmount;
    public int turnCounter; // ������� �����
    public Ability[] abilities; // ������ ������������

    private List<StatusEffect> activeStatusEffects; // ������ �������� ������-��������

    public virtual void Start()
    {
        currentHealth = maxHealth;
        blockAmount = 0;
        turnCounter = 0; // ������������� �������� �����
        activeStatusEffects = new List<StatusEffect>(); // ������������� ������ ������-��������
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
            abilities[0].Use(this, target); // ��������� ������ ����������� (�����)
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
        Debug.Log(gameObject.name + " �����");
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
                    // ������ ���� ������-��������
            }

            effect.Duration--;
            yield return null; // �������� ������ ����
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