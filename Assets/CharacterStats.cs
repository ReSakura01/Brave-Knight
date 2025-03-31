
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")]
    public Stat strength; // 1 点表示提升暴击伤害1%
    public Stat agility;  // 1 点表示闪避提高1%
    public Stat intelligence; // 1 点表示提高魔法伤害2 点
    public Stat vitality; // 1 点能提高生命上限1 点

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;

    public Stat damage;

    public int currentHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
    }

    public virtual void DoDamage(CharacterStats _targeStat)
    {
        if (TargetCanAvoidAttack(_targeStat))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        totalDamage = CheckTargetArmor(_targeStat, totalDamage);
        _targeStat.TakeDamage(totalDamage);
    }


    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;

        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {

    }

    private int CheckTargetArmor(CharacterStats _targeStat, int totalDamage)
    {
        totalDamage -= _targeStat.armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStats _targeStat)
    {
        int totalEvasion = _targeStat.evasion.GetValue() + _targeStat.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    }
}
