using UnityEngine;

/// <summary>
/// Static class to hold and manage player stats during runtime.
/// This can be reset on each run or carried over in a new game mode.
/// </summary>
public static class PlayerStats
{
    // --- Core Stats ---

    /// <summary> Player health represented by shield energy. </summary>
    public static float ShieldPower = 100f;

    /// <summary> Damage dealt per shot. </summary>
    public static float FirePower = 10f;

    /// <summary> Time in seconds between each shot. Lower is faster. </summary>
    public static float FireSpeed = 0.5f;

    /// <summary> Number of bullets fired per shot. </summary>
    public static int ProjectileCount = 1;

    /// <summary> How wide bullets spread when fired. </summary>
    public static float BulletSpread = 0f;

    /// <summary> How many enemies a bullet can pierce. </summary>
    public static int PierceCount = 0;

    /// <summary> Chance to deal critical hit (0 to 1). </summary>
    public static float CritChance = 0.05f;

    /// <summary> Multiplier applied to damage on critical hits. </summary>
    public static float CritMultiplier = 2f;

    /// <summary> Radius in which collectibles are pulled to the player. </summary>
    public static float MagnetRange = 2f;

    /// <summary> Speed at which the player ship moves. </summary>
    public static float MoveSpeed = 5f;

    /// <summary> Time between dashes. </summary>
    public static float DashCooldown = 5f;

    /// <summary> Shield regenerated per second. </summary>
    public static float ShieldRegen = 0f;

    /// <summary> Max number of bounces bullets can make. </summary>
    public static int BounceCount = 0;

    /// <summary> How fast bullets travel. </summary>
    public static float BulletSpeed = 10f;

    /// <summary> % of enemy kill value added to currency gain. </summary>
    public static float ScrapBonus = 0f;

    /// <summary> % chance to cause an explosion on hit. </summary>
    public static float ExplosionChance = 0f;

    /// <summary> Duration of temporary overdrive effect (faster fire rate, etc.). </summary>
    public static float OverdriveDuration = 0f;

    /// <summary> Radius and force of gravity pull bullets may cause. </summary>
    public static float SingularityPower = 0f;

    /// <summary> Number of additional Power-Up choices shown. </summary>
    public static int ExtraPowerUpChoices = 0;
    
    /// <summary> Flux collected during the current run. </summary>
    public static int Flux = 0;

    /// <summary> Total Flux available for hangar upgrades. Saved between runs. </summary>
    private static int TotalFlux = 0;
    
    // --- Get Methods ---

    public static int GetTotalFlux() => TotalFlux; 

    // --- Set Methods ---

    public static void SetTotalFlux(int fluxAmount)
    {
        TotalFlux -= fluxAmount;
    }

    // --- Reset Method ---

    public static void ResetStats()
    {
        ShieldPower = 100f;
        FirePower = 10f;
        FireSpeed = 0.5f;
        ProjectileCount = 1;
        BulletSpread = 0f;
        PierceCount = 0;
        CritChance = 0.05f;
        CritMultiplier = 2f;
        MagnetRange = 2f;
        MoveSpeed = 5f;
        DashCooldown = 5f;
        ShieldRegen = 0f;
        BounceCount = 0;
        BulletSpeed = 10f;
        ScrapBonus = 0f;
        ExplosionChance = 0f;
        OverdriveDuration = 0f;
        SingularityPower = 0f;
        ExtraPowerUpChoices = 0;
        
        Flux = 0; // Only reset in-run Flux
        // TotalFlux stays persistent between runs
    }
}
