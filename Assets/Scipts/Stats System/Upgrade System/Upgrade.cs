using System;

public abstract class Upgrade
{
    public string Id;              // Unique ID
    public string Name;            // Display Name
    public string Description;
    public int Level = 0;
    public int MaxLevel = 5;
    public int BaseCost = 10;

    public Upgrade(string id, string name, string desc, int baseCost, int maxLevel = 5)
    {
        Id = id;
        Name = name;
        Description = desc;
        BaseCost = baseCost;
        MaxLevel = maxLevel;
    }

    public bool CanUpgrade => Level < MaxLevel && PlayerStats.GetTotalFlux() >= GetCost();

    public int GetCost() => BaseCost + (Level * 10);

    public bool TryPurchase()
    {
        if (!CanUpgrade) return false;

        PlayerStats.SetTotalFlux(GetCost());
        Level++;
        Apply();
        return true;
    }

    public abstract void Apply();
}

public class Upgrade_FirePower : Upgrade
{
    private const float AdditionRate = 2.0f;

    public Upgrade_FirePower() : base("fire_power", "Overcharged Core", $"Increases fire power from <color=#FFD700>{PlayerStats.FirePower}</color> to <color=#FFD700>{PlayerStats.FirePower + AdditionRate}</color>", 20) { }

    public override void Apply()
    {
        PlayerStats.FirePower += AdditionRate;
    }
}

public class Upgrade_FireSpeed : Upgrade
{
    private const float DecreaseRate = 0.05f;
    
    public Upgrade_FireSpeed() : base("fire_speed", "Rapid Firing", $"Decreases time between shots from <color=#FFD700>{PlayerStats.BulletSpeed}</color> seconds to <color=#FFD700>{PlayerStats.BulletSpeed - DecreaseRate}</color> seconds.", 25) { }

    public override void Apply()
    {
        PlayerStats.FireSpeed -= DecreaseRate;
    }
}

/*public class Upgrade_ProjectileCount : Upgrade
{
    public Upgrade_ProjectileCount() : base("proj_count", "Extra Shells", "Adds 1 projectile per shot.", 30, 3) { }

    public override void Apply()
    {
        PlayerStats.ProjectileCount += 1;
    }
}*/

public class Upgrade_ShieldPower : Upgrade
{
    private const float ShieldIncreaseRate = 15.0f;
    
    public Upgrade_ShieldPower() : base("shield_power", "Shield Buffer", $"Increases max shield from <color=#FFD700>{PlayerStats.ShieldPower}</color> to <color=#FFD700>{PlayerStats.ShieldPower + ShieldIncreaseRate}</color>.", 15) { }

    public override void Apply()
    {
        PlayerStats.ShieldPower += ShieldIncreaseRate;
    }
}

public class Upgrade_MagnetRange : Upgrade
{
    private const float RangeIncreaseRate = 1.0f;
    
    public Upgrade_MagnetRange() : base("magnet_range", "Core Magnetizer", $"Increases pickup range from <color=#FFD700>{PlayerStats.MagnetRange}</color> to <color=#FFD700>{PlayerStats.MagnetRange + RangeIncreaseRate}</color>.", 20) { }

    public override void Apply()
    {
        PlayerStats.MagnetRange += RangeIncreaseRate;
    }
}

public class Upgrade_MoveSpeed : Upgrade
{
    private const float MoveSpeedRate = 0.5f;
    
    public Upgrade_MoveSpeed() : base("move_speed", "Momentum Core", $"Increases ship movement speed from <color=#FFD700>{PlayerStats.MoveSpeed}</color> to <color=#FFD700>{PlayerStats.MoveSpeed + MoveSpeedRate}</color>.", 20) { }

    public override void Apply()
    {
        PlayerStats.MoveSpeed += MoveSpeedRate;
    }
}

/*public class Upgrade_DashCooldown : Upgrade
{
    public Upgrade_DashCooldown() : base("dash_cooldown", "Reinforced Coil", "Reduces dash cooldown.", 25) { }

    public override void Apply()
    {
        PlayerStats.DashCooldown -= 0.3f;
    }
}*/

public class Upgrade_CritChance : Upgrade
{
    private const float CritChanceRate = 0.03f;
    
    public Upgrade_CritChance() : base("crit_chance", "Critical Systems", $"Increases critical hit chance from <color=#FFD700>{PlayerStats.CritChance}%</color> to <color=#FFD700>{PlayerStats.CritChance + CritChanceRate}%</color>.", 30) { }

    public override void Apply()
    {
        PlayerStats.CritChance += CritChanceRate;
    }
}


public class Upgrade_PiercingChance : Upgrade
{
    public Upgrade_PiercingChance() : base("pierce_drop", "Pierce Protocol", "Increases Piercing Round drop rate.", 15) { }

    public override void Apply()
    {
        PowerUpManager.AddDropRateBonus("PiercingRounds", 5f);
    }
}

public class Upgrade_RapidFireChance : Upgrade
{
    public Upgrade_RapidFireChance() : base("rapid_drop", "Reflex Chip", "Increases Rapid Fire PowerUp chance.", 15) { }

    public override void Apply()
    {
        PowerUpManager.AddDropRateBonus("RapidFire", 5f);
    }
}

