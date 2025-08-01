using System;

/// <summary>
/// This can be reset on each run or carried over in a new game mode.
/// </summary>
[Serializable] // Required for JSON serialization
public struct PlayerStats
{
    // --- Core Stats ---

    /// <summary> Player maximum health represented by shield energy. </summary>
    public float maxShieldPower;
    
    /// <summary> Player health represented by shield energy. </summary>
    public float shieldPower;

    /// <summary> Damage dealt per shot. </summary>
    public float firePower;

    /// <summary> Time in seconds between each shot. Lower is faster. </summary>
    public float fireSpeed;

    /// <summary> Number of bullets fired per shot. </summary>
    public int projectileCount;

    /// <summary> How wide bullets spread when fired. </summary>
    public float bulletSpread;

    /// <summary> How many enemies a bullet can pierce. </summary>
    public int pierceCount;

    /// <summary> Chance to deal critical hit (0 to 1). </summary>
    public float critChance;

    /// <summary> Multiplier applied to damage on critical hits. </summary>
    public float critDamage;

    /// <summary> Radius in which collectibles are pulled to the player. </summary>
    public float magnetRange;

    /// <summary> Speed at which the player ship moves. </summary>
    public float moveSpeed;

    /// <summary> Shield regenerated per second. </summary>
    public float shieldRegen;

    /// <summary> % of enemy kill value added to currency gain. </summary>
    public float scrapBonus;

    /// <summary> Number of additional Power-Up choices shown. </summary>
    public int extraPowerUpChoices;

    /// <summary> Flux collected during the current run. </summary>
    public int flux;

    /// <summary> Total Flux available for hangar upgrades. Saved between runs. </summary>
    public int totalFlux;


    public PlayerStats(float test)
    {
        maxShieldPower = 100f;
        shieldPower = 100f;
        firePower = 10f;
        fireSpeed = 0.5f;
        projectileCount = 1;
        bulletSpread = 0f;
        pierceCount = 0;
        critChance = 0.05f;
        critDamage = 2f;
        magnetRange = 2f;
        moveSpeed = 5f;
        shieldRegen = 0f;
        scrapBonus = 0f;
        extraPowerUpChoices = 0;
        flux = 0;
        totalFlux = 0;
    }

    // --- Get Methods ---

    public float MaxShieldPower => maxShieldPower;
    public float ShieldPower => shieldPower;
    public float FirePower => firePower;
    public float FireSpeed => fireSpeed;
    public int ProjectileCount => projectileCount;
    public float BulletSpread => bulletSpread;
    public int PierceCount => pierceCount;
    public float CritChance => critChance;
    public float CritDamage => critDamage;
    public float MagnetRange => magnetRange;
    public float MoveSpeed => moveSpeed;
    public float ShieldRegen => shieldRegen;
    public float ScrapBonus => scrapBonus;
    public int ExtraPowerUpChoices => extraPowerUpChoices;
    public int Flux => flux;
    public int TotalFlux => totalFlux;

    // --- Set Methods ---

    public void SetMaxShieldPower(float value)
    {
        maxShieldPower += value;
        shieldPower += value;
    }

    public void SetShieldPower(float value) => shieldPower += value;
    public void SetFirePower(float value) => firePower += value;
    public void SetFireSpeed(float value) => fireSpeed += value;
    public void SetProjectileCount(int value) => projectileCount += value;
    public void SetBulletSpread(float value) => bulletSpread += value;
    public void SetPierceCount(int value) => pierceCount += value;
    public void SetCritChance(float value) => critChance += value;
    public void SetCritMultiplier(float value) => critDamage += value;
    public void SetMagnetRange(float value) => magnetRange += value;
    public void SetMoveSpeed(float value) => moveSpeed += value;
    public void SetShieldRegen(float value) => shieldRegen += value;
    public void SetScrapBonus(float value) => scrapBonus += value;
    public void SetExtraPowerUpChoices(int value) => extraPowerUpChoices += value;
    public void SetFlux(int value) => flux += value;
    public void SetTotalFlux(int value) => totalFlux += value;

    // --- Reset Method ---

    public void ResetStats()
    {
        shieldPower = 100f;
        firePower = 10f;
        fireSpeed = 0.5f;
        projectileCount = 1;
        bulletSpread = 0f;
        pierceCount = 0;
        critChance = 0.05f;
        critDamage = firePower + firePower * 0.5f;
        magnetRange = 2f;
        moveSpeed = 5f;
        shieldRegen = 0f;
        scrapBonus = 0f;
        extraPowerUpChoices = 0;
        flux = 0; // Only reset in-run Flux
        // TotalFlux stays persistent between runs
    }
}