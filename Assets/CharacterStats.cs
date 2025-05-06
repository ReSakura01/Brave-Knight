
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")]
    public Stat strength; // 1 ���ʾ���������˺�1%
    public Stat agility;  // 1 ���ʾ�������1%
    public Stat intelligence; // 1 ���ʾ���ħ���˺�2 ��
    public Stat vitality; // 1 ���������������1 ��


    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;   // default value 150%

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;

    [SerializeField] public int currentHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = maxHealth.GetValue();
    }

    public virtual void DoDamage(CharacterStats _targeStat)
    {
        if (TargetCanAvoidAttack(_targeStat))      // ���ܼ��
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCrit())      // �������
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }


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

    private bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue();

        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }
        return false;
    }

    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f;

        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }
}
