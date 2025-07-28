using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class UpgradeManager
{
    private static List<Upgrade> _allUpgrades;
    private static List<Upgrade> _purchasedUpgrades;

    public static void Init()
    {
        // Register all upgrades
        _allUpgrades = GetAllUpgradeInstances();

        foreach (var upgrade in _allUpgrades)
        {
            if (upgrade.Level >= 1)
            {
                _purchasedUpgrades.Add(upgrade);
            }
        }
    }

    public static void Purchase(int index)
    {
        if (_allUpgrades[index].TryPurchase()) _purchasedUpgrades.Add(_allUpgrades[index]);
        
    }

    private static List<Upgrade> GetAllUpgradeInstances()
    {
        List<Upgrade> upgradeInstances = new List<Upgrade>();
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
                            upgradeInstances.Add(instance);
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