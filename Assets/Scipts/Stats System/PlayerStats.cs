using System;

/// <summary>
/// This can be reset on each run or carried over in a new game mode.
/// </summary>
[Serializable] // Required for JSON serialization
public struct PlayerStats
{
    // --- Core Stats ---

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
    public float critMultiplier;

    /// <summary> Radius in which collectibles are pulled to the player. </summary>
    public float magnetRange;

    /// <summary> Speed at which the player ship moves. </summary>
    public float moveSpeed;

    /// <summary> Time between dashes. </summary>
    public float dashCooldown;

    /// <summary> Shield regenerated per second. </summary>
    public float shieldRegen;

    /// <summary> Max number of bounces bullets can make. </summary>
    public int bounceCount;

    /// <summary> How fast bullets travel. </summary>
    public float bulletSpeed;

    /// <summary> % of enemy kill value added to currency gain. </summary>
    public float scrapBonus;

    /// <summary> % chance to cause an explosion on hit. </summary>
    public float explosionChance;

    /// <summary> Duration of temporary overdrive effect (faster fire rate, etc.). </summary>
    public float overdriveDuration;

    /// <summary> Radius and force of gravity pull bullets may cause. </summary>
    public float singularityPower;

    /// <summary> Number of additional Power-Up choices shown. </summary>
    public int extraPowerUpChoices;

    /// <summary> Flux collected during the current run. </summary>
    public int flux;

    /// <summary> Total Flux available for hangar upgrades. Saved between runs. </summary>
    public int totalFlux;


    public PlayerStats(float test)
    {
        shieldPower = 100f;
        firePower = 10f;
        fireSpeed = 0.5f;
        projectileCount = 1;
        bulletSpread = 0f;
        pierceCount = 0;
        critChance = 0.05f;
        critMultiplier = 2f;
        magnetRange = 2f;
        moveSpeed = 5f;
        dashCooldown = 5f;
        shieldRegen = 0f;
        bounceCount = 0;
        bulletSpeed = 10f;
        scrapBonus = 0f;
        explosionChance = 0f;
        overdriveDuration = 0f;
        singularityPower = 0f;
        extraPowerUpChoices = 0;
        flux = 0;
        totalFlux = 0;
    }

    // --- Get Methods ---

    public float ShieldPower => shieldPower;
    public float FirePower => firePower;
    public float FireSpeed => fireSpeed;
    public int ProjectileCount => projectileCount;
    public float BulletSpread => bulletSpread;
    public int PierceCount => pierceCount;
    public float CritChance => critChance;
    public float CritMultiplier => critMultiplier;
    public float MagnetRange => magnetRange;
    public float MoveSpeed => moveSpeed;
    public float DashCooldown => dashCooldown;
    public float ShieldRegen => shieldRegen;
    public int BounceCount => bounceCount;
    public float BulletSpeed => bulletSpeed;
    public float ScrapBonus => scrapBonus;
    public float ExplosionChance => explosionChance;
    public float OverdriveDuration => overdriveDuration;
    public float SingularityPower => singularityPower;
    public int ExtraPowerUpChoices => extraPowerUpChoices;
    public int Flux => flux;
    public int TotalFlux => totalFlux;

    // --- Set Methods ---

    public void SetShieldPower(float value) => shieldPower += value;
    public void SetFirePower(float value) => firePower += value;
    public void SetFireSpeed(float value) => fireSpeed += value;
    public void SetProjectileCount(int value) => projectileCount += value;
    public void SetBulletSpread(float value) => bulletSpread += value;
    public void SetPierceCount(int value) => pierceCount += value;
    public void SetCritChance(float value) => critChance += value;
    public void SetCritMultiplier(float value) => critMultiplier += value;
    public void SetMagnetRange(float value) => magnetRange += value;
    public void SetMoveSpeed(float value) => moveSpeed += value;
    public void SetDashCooldown(float value) => dashCooldown += value;
    public void SetShieldRegen(float value) => shieldRegen += value;
    public void SetBounceCount(int value) => bounceCount += value;
    public void SetBulletSpeed(float value) => bulletSpeed += value;
    public void SetScrapBonus(float value) => scrapBonus += value;
    public void SetExplosionChance(float value) => explosionChance += value;
    public void SetOverdriveDuration(float value) => overdriveDuration += value;
    public void SetSingularityPower(float value) => singularityPower += value;
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
        critMultiplier = 2f;
        magnetRange = 2f;
        moveSpeed = 5f;
        dashCooldown = 5f;
        shieldRegen = 0f;
        bounceCount = 0;
        bulletSpeed = 10f;
        scrapBonus = 0f;
        explosionChance = 0f;
        overdriveDuration = 0f;
        singularityPower = 0f;
        extraPowerUpChoices = 0;
        flux = 0; // Only reset in-run Flux
        // TotalFlux stays persistent between runs
    }
}