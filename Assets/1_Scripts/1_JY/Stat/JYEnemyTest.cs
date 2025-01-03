using System;
using UnityEngine;

public class JYEnemyTest : MonoBehaviour
{
    public event Action<float> onEnemyHPChanged;

    [SerializeField] private float maxHp;
    [SerializeField] private float hp;

    public float MaxHP => maxHp;    
    public float HP
    {
        get => hp;
        set 
        { 
            hp = value;
            onEnemyHPChanged?.Invoke(hp);
        }
    }

    public void GetDamage(float damage)
    {
        HP -= damage;
    }
}
