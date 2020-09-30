using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public static int Stat_Min = 0;
    public static int Stat_Max = 10;

    public enum Type
    {
        Wealth,
        Career,
        Health,
        Marriage,
        Relation,
    }

    private SingleStat _wealthStat;
    private SingleStat _careerStat;
    private SingleStat _healthStat;
    private SingleStat _marriageStat;
    private SingleStat _relationStat;

    public Stat(int wealthStatAmount, int careerStatAmount, int healthStatAmount, int marriageStatAmount, int relationStatAmount)
    {
        _wealthStat = new SingleStat(wealthStatAmount);
        _careerStat = new SingleStat(careerStatAmount);
        _healthStat = new SingleStat(healthStatAmount);
        _marriageStat = new SingleStat(marriageStatAmount);
        _relationStat = new SingleStat(relationStatAmount);
    }

    private SingleStat GetSingleStat(Type statType)
    {
        switch (statType)
        {
            default:
            case Type.Wealth: return _wealthStat;
            case Type.Career: return _careerStat;
            case Type.Health: return _healthStat;
            case Type.Marriage: return _marriageStat;
            case Type.Relation: return _relationStat;
        }
    }

    public void SetStatAmount(Type statType, int statAmount)
    {
        GetSingleStat(statType).SetStatAmount(statAmount);
    }

    public int GetStatAmount(Type statType)
    {
        return GetSingleStat(statType).GetStatAmount();
    }

    public float GetStatAmountNormalized(Type statType)
    {
        return GetSingleStat(statType).GetStatAmountNormalized();
    }

    private class SingleStat
    {
        private int _stat;

        public SingleStat(int statAmount)
        {
            SetStatAmount(statAmount);
        }

        public void SetStatAmount(int statAmount)
        {
            //check if the input stat value is within the min and max value
            _stat = Mathf.Clamp(statAmount, Stat_Min, Stat_Max);
        }

        public int GetStatAmount()
        {
            return _stat;
        }

        public float GetStatAmountNormalized()
        {
            return (float)_stat / Stat_Max;
        }
    }
}
