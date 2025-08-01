using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable] // Required for JSON serialization
public struct UpgradeStruct
{
    public string Name;            // Display Name
    public int Level;
    public int MaxLevel;
    public int BaseCost;
    public UpgradeType UpgradeType;
    public Upgrade Upgrade;
    
    public UpgradeStruct(string name, UpgradeType upgradeType)
    {
        Name = name;
        BaseCost = 10;
        MaxLevel = 10; 
        Level = 0;
        MaxLevel = 5;
        BaseCost = 10;
        UpgradeType = upgradeType;
        Upgrade = UpgradeManager.GetCertainUpgrade(upgradeType);
    }
    
    public bool CanUpgrade => Level < MaxLevel && DataManager.playerStats.TotalFlux >= GetCost();
    
    public int GetCost() => BaseCost + (Level * 10);

    public bool TryPurchase()
    {
        if (!CanUpgrade) return false;

        DataManager.playerStats.SetTotalFlux(GetCost());
        Level++;
        Apply();
        return true;
    }

    public string GetDescription()
    {
        Upgrade ??= UpgradeManager.GetCertainUpgrade(UpgradeType);
        return Upgrade.GetDescription();
    }

    public void Apply()
    {
        Upgrade ??= UpgradeManager.GetCertainUpgrade(UpgradeType);
        Upgrade.Apply();
    }
}


public static class UpgradeManager
{
    public static List<UpgradeStruct> AllUpgrades;
    public static List<UpgradeStruct> PurchasedUpgrades;

    public static void Init()
    {
        // Register all upgrades if empty.
        AllUpgrades ??= new List<UpgradeStruct>();
        PurchasedUpgrades ??= new List<UpgradeStruct>();
        
        if (AllUpgrades.Count <= 0)
            AllUpgrades = GetAllUpgradeInstances();

        foreach (var upgrade in AllUpgrades)
        {
            if (upgrade.Level >= 1)
            {
                PurchasedUpgrades.Add(upgrade);
            }
        }
    }

    public static void Purchase(int index)
    {
        if (AllUpgrades[index].TryPurchase()) PurchasedUpgrades.Add(AllUpgrades[index]);
        DataManager.SaveData();
        DataManager.SaveUpgrades();
    }

    public static Upgrade GetCertainUpgrade(UpgradeType upgradeType)
    {
        Type baseType = typeof(Upgrade);

        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(baseType) && !type.IsAbstract)
                {
                    // Try to create an instance of the Upgrade subclass
                    try
                    {
                        Upgrade instance = Activator.CreateInstance(type) as Upgrade;
                        if (instance != null)
                        {
                            if (instance.Type == upgradeType)
                            {
                                return instance;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"Could not create instance of {type.Name}: {e.Message}");
                    }
                }
            }
        }

        return null;
    }

    private static List<UpgradeStruct> GetAllUpgradeInstances()
    {
        List<UpgradeStruct> upgradeInstances = new List<UpgradeStruct>();
        Type baseType = typeof(Upgrade);

        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(baseType) && !type.IsAbstract)
                {
                    // Try to create an instance of the Upgrade subclass
                    try
                    {
                        Upgrade instance = Activator.CreateInstance(type) as Upgrade;
                        if (instance != null)
                        {
                            UpgradeStruct newUpgrade = new UpgradeStruct(instance.Name, instance.Type);
                            upgradeInstances.Add(newUpgrade);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"Could not create instance of {type.Name}: {e.Message}");
                    }
                }
            }
        }

        return upgradeInstances;
    }
}