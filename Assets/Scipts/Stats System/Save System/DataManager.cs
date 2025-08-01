using System;
using UnityEngine;

public static class DataManager
{
    public static PlayerStats playerStats;

    /// <summary>
    /// Loads all the data from the files with error handling.
    /// </summary>
    public static void LoadData()
    {
        try
        {
            playerStats = FileHandler.ReadFromJson<PlayerStats>("PlayerStats.json");
            if (playerStats.FirePower <= 0)
            {
                playerStats = new PlayerStats(0);
                SaveData();
            }
            
            RefactorUpgradeList();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load data: {ex.Message}");
            playerStats = new PlayerStats(0);
            SaveData();
        }
    }

    /// <summary>
    /// Saves all the data to the files with error handling.
    /// </summary>
    public static void SaveData()
    {
        try
        {
            FileHandler.SaveToJson(playerStats, "PlayerStats.json");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save data: {ex.Message}");
        }
    }

    /// <summary>
    /// Saves all the data to the files with error handling.
    /// </summary>
    public static void SaveUpgrades()
    {
        try
        {
            FileHandler.SaveListToJson(UpgradeManager.AllUpgrades, "Upgrades.json");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save upgrades: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads the Upgrades from the files with error handling.
    /// </summary>
    private static void RefactorUpgradeList()
    {
        try
        {
            UpgradeManager.AllUpgrades = FileHandler.ReadListFromJson<UpgradeStruct>("Upgrades.json");
            UpgradeManager.Init();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load data: {ex.Message}");
            UpgradeManager.Init();
            SaveUpgrades();
        }
    }
}