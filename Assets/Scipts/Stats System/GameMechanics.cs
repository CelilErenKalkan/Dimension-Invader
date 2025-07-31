using UnityEngine;

public static class GameMechanics
{
    // --- DAMAGE ---

    /// <summary>
    /// Calculates the damage dealt by the player, considering critical hit chance and damage.
    /// </summary>
    public static float GiveDamage()
    {
        var stats = DataManager.playerStats;
        float baseDamage = stats.firePower;
        bool isCritical = Random.value < stats.critChance;
        float damage = isCritical ? baseDamage * stats.critDamage : baseDamage;
        return Mathf.Round(damage);
    }

    /// <summary>
    /// Applies incoming damage to the player's shield. Returns the remaining damage if the shield is depleted.
    /// </summary>
    public static void TakeDamage(float amount)
    {
        DataManager.playerStats.SetShieldPower(amount);
    }

    // --- SHOOTING ---

    /// <summary>
    /// Calculates time between shots based on fire speed.
    /// </summary>
    public static float GetShotCooldown()
    {
        return Mathf.Max(0.01f, DataManager.playerStats.FireSpeed);
    }

    /// <summary>
    /// Returns a randomized angle for a bullet based on bullet spread.
    /// </summary>
    public static float GetBulletSpread(float spread)
    {
        return DataManager.playerStats.BulletSpread;
    }

    // --- MAGNET / COLLECTIBLES ---

    /// <summary>
    /// Returns true if a collectible is within magnet range.
    /// </summary>
    public static bool IsWithinMagnetRange(Vector3 playerPos, Vector3 collectiblePos)
    {
        return Vector3.SqrMagnitude(playerPos - collectiblePos) <= DataManager.playerStats.MagnetRange;
    }

    // --- REGENERATION ---

    /// <summary>
    /// Regenerates shield power over time, respecting the maximum cap.
    /// </summary>
    public static void RegenerateShield()
    {
        var stats = DataManager.playerStats;
        
        DataManager.playerStats.SetShieldPower(stats.ShieldRegen);
        if (stats.ShieldPower > stats.MaxShieldPower)
            DataManager.playerStats.SetShieldPower(stats.MaxShieldPower);
    }

    // --- SCRAP BONUS ---

    /// <summary>
    /// Applies scrap bonus when collecting currency from enemy kills.
    /// </summary>
    public static int CalculateScrapGain(int baseScrap, float scrapBonus)
    {
        return Mathf.RoundToInt(DataManager.playerStats.ScrapBonus);
    }
    
    /// <summary>
    /// Determines how many enemies a bullet can pierce.
    /// </summary>
    public static int GetPierceCount(int pierce)
    {
        return Mathf.Max(0, pierce);
    }

    // --- FLUX SYSTEM ---

    /// <summary>
    /// Adds collected Flux to the total pool at the end of a run.
    /// </summary>
    public static void TransferFlux()
    {
        DataManager.playerStats.SetTotalFlux(DataManager.playerStats.flux);
    }
    
    /// <summary>
    /// Adds collected Flux to the total pool at the end of a run.
    /// </summary>
    public static void GainFlux(int amount)
    {
        DataManager.playerStats.SetFlux(amount);
        DataManager.playerStats.SetFlux(0);
    }
}
