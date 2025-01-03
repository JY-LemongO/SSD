using UnityEngine;

public class SAM_Stat
{
    public int CurrentHP;
    public int MaxHP;
    public float CurrentEXP;
    public float MaxEXP;
    public float MaxEXPPerLevel => MaxEXP * Level;
    public int Level = 1;

    public SAM_Stat(SAM_StatSO statSO)
    {
        MaxHP = statSO.MaxHP;
        CurrentHP = MaxHP;
        MaxEXP = statSO.MaxEXP;
        CurrentEXP = 0;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        if(CurrentHP <= 0)
        {
            int remained = Mathf.Abs(CurrentHP);
            CurrentHP = 0;
            LoseEXP((float)remained);
        }
    }

    public void Heal(int heal)//
    {
        CurrentHP += heal;
        if (CurrentHP >= MaxHP)
            CurrentHP = MaxHP;
    }



    public void GetEXP(float exp)
    {
        CurrentEXP += exp;
        if(CurrentEXP >= MaxEXPPerLevel)
        {
            LevelUP(CurrentEXP - MaxEXPPerLevel);
        }
    }    

    public void LoseEXP(float exp)
    {
        CurrentEXP -= exp;
        if(CurrentEXP <= 0f)
        {
            LevelDown(Mathf.Abs(CurrentEXP));
        }
    }

    public void LevelUP(float exp)
    {
        ++Level;
        CurrentEXP = exp;
        CurrentHP = MaxHP;
    }

    public void LevelDown(float exp)
    {
        --Level;
        if (Level <= 1)
            Level = 1;
        CurrentEXP = MaxEXPPerLevel - exp;
    }
}
