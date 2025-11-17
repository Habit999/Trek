using UnityEngine;
using System.IO;
using System;

public class PlayerStatManager : MonoBehaviour
{
    private string StatDataPath = Application.dataPath + "/StatData.json";

    public PlayerStats Stats { get; private set; }

    public event Action<float> OnVitalityChanged;

    [SerializeField] private int vitalityModifier;
    [SerializeField] private int attunementModifier;
    [SerializeField] private int enduranceModifier;
    [SerializeField] private int strengthModifier;
    [SerializeField] private int dexterityModifier;
    [SerializeField] private int resistanceModifier;
    [SerializeField] private int intelligenceModifier;

    private static float firstLevelExperienceTarget = 100;
    private static float experinceTargetMultiplier = 0.05f;

    private float experienceTarget;

    private void Awake()
    {
        LoadStatData();
        CalculateNextExperienceTarget();
    }

    private void CalculateNextExperienceTarget()
    {
        experienceTarget = firstLevelExperienceTarget * (1 + (experinceTargetMultiplier * Stats.Level));
    }

    public void AddExperience(float experience)
    {
        Stats.Experience += experience;
        if (Stats.Experience >= experienceTarget)
        {
            Stats.Level++;
            Stats.Experience -= experienceTarget;
            CalculateNextExperienceTarget();
        }
        SaveStatData();
    }

    #region Set Stats
    public void SetVitality(int amount)
    {
        Stats.Vitality = amount;
        OnVitalityChanged?.Invoke(100 * (1 + (vitalityModifier * Stats.Vitality)));
    }

    public void SetAttunement(int amount)
    {
        Stats.Attunement = amount;
    }

    public void SetEndurance(int amount)
    {
        Stats.Endurance = amount;
    }

    public void SetStrength(int amount)
    {
        Stats.Strength = amount;
    }

    public void SetDexterity(int amount)
    {
        Stats.Dexterity = amount;
    }

    public void SetResistance(int amount)
    {
        Stats.Resistance = amount;
    }

    public void SetIntelligence(int amount)
    {
        Stats.Intelligence = amount;
    }
    #endregion

    private void LoadStatData()
    {
        if (!File.Exists(StatDataPath))
        {
            Stats = new PlayerStats();
        }
        else
        {
            string json = File.ReadAllText(StatDataPath);
            PlayerStats loadedStats = JsonUtility.FromJson<PlayerStats>(json);
            SetVitality(loadedStats.Vitality);
            SetAttunement(loadedStats.Attunement);
            SetEndurance(loadedStats.Endurance);
            SetStrength(loadedStats.Strength);
            SetDexterity(loadedStats.Dexterity);
            SetResistance(loadedStats.Resistance);
            SetIntelligence(loadedStats.Intelligence);
        }
    }

    private void SaveStatData()
    {
        if (File.Exists(StatDataPath))
        {
            File.Delete(StatDataPath);
        }
        string json = JsonUtility.ToJson(Stats);
        File.WriteAllText(StatDataPath, json);
    }
}

public class PlayerStats
{
    public int Level = 1;
    public float Experience = 0;

    public int Vitality = 1;
    public int Attunement = 1;
    public int Endurance = 1;
    public int Strength = 1;
    public int Dexterity = 1;
    public int Resistance = 1;
    public int Intelligence = 1;
}
