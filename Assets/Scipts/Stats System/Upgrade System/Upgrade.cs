using UnityEngine;

public enum UpgradeType
{
    FirePower,
    FireSpeed,
    ProjectileCount,
    ShieldPower,
    MagnetRange,
    MoveSpeed,
    DashCooldown,
    CritChance,
    PiercingChance,
    RapidFireChance
}

public abstract class Upgrade
{
    public string Name;            // Display Name
    public UpgradeType Type;

    public Upgrade(string name, UpgradeType type)
    {
        Name = name;
        Type = type;
    }

    public virtual string GetDescription() => "";

    public abstract void Apply();
}

public class Upgrade_FirePower : Upgrade
{
    private const float AdditionRate = 2.0f;

    public Upgrade_FirePower() : base("Overcharged Core", UpgradeType.FirePower) { }

    public override string GetDescription()
    {
        return
            $"Increases fire power from <color=#FFD700>{DataManager.playerStats.FirePower}</color> to <color=#FFD700>{DataManager.playerStats.FirePower + AdditionRate}</color>";
    }

    public override void Apply()
    {
        DataManager.playerStats.SetFirePower(AdditionRate);
    }
}

public class Upgrade_FireSpeed : Upgrade
{
    private const float DecreaseRate = 0.05f;
    
    public Upgrade_FireSpeed() : base( "Rapid Firing", UpgradeType.FireSpeed) { }

    public override string GetDescription()
    {
        return
            $"Decreases time between shots from <color=#FFD700>{DataManager.playerStats.BulletSpeed}</color> seconds to <color=#FFD700>{DataManager.playerStats.BulletSpeed - DecreaseRate}</color> seconds.";
    }
    
    public override void Apply()
    {
        DataManager.playerStats.SetFireSpeed(DecreaseRate * -1);
    }
}

/*public class Upgrade_ProjectileCount : Upgrade
{
    public Upgrade_ProjectileCount() : base("Extra Shells", UpgradeType.ProjectileCount, "Adds 1 projectile per shot.") { }
    
    

    public override void Apply()
    {
        PlayerStats.ProjectileCount += 1;
    }
}*/

public class Upgrade_ShieldPower : Upgrade
{
    private const float ShieldIncreaseRate = 15.0f;
    
    public Upgrade_ShieldPower() : base( "Shield Buffer", UpgradeType.ShieldPower) { }

    public override string GetDescription()
    {
        return
            $"Increases max shield from <color=#FFD700>{DataManager.playerStats.ShieldPower}</color> to <color=#FFD700>{DataManager.playerStats.ShieldPower + ShieldIncreaseRate}</color>.";
    }
    
    public override void Apply()
    {
        DataManager.playerStats.SetShieldPower(ShieldIncreaseRate);
    }
}

public class Upgrade_MagnetRange : Upgrade
{
    private const float RangeIncreaseRate = 1.0f;
    
    public Upgrade_MagnetRange() : base( "Core Magnetizer", UpgradeType.MagnetRange) { }

    public override string GetDescription()
    {
        return
            $"Increases pickup range from <color=#FFD700>{DataManager.playerStats.MagnetRange}</color> to <color=#FFD700>{DataManager.playerStats.MagnetRange + RangeIncreaseRate}</color>.";
    }
    
    public override void Apply()
    {
        DataManager.playerStats.SetMagnetRange(RangeIncreaseRate);
    }
}

public class Upgrade_MoveSpeed : Upgrade
{
    private const float MoveSpeedRate = 0.5f;
    
    public Upgrade_MoveSpeed() : base( "Momentum Core", UpgradeType.MoveSpeed) { }

    public override string GetDescription()
    {
        return
            $"Increases ship movement speed from <color=#FFD700>{DataManager.playerStats.MoveSpeed}</color> to <color=#FFD700>{DataManager.playerStats.MoveSpeed + MoveSpeedRate}</color>.";
    }
    
    public override void Apply()
    {
        DataManager.playerStats.SetMoveSpeed(MoveSpeedRate);
    }
}

/*public class Upgrade_DashCooldown : Upgrade
{
    public Upgrade_DashCooldown() : base("Reinforced Coil", UpgradeType.DashCooldown, "Reduces dash cooldown.") { }

    public override void Apply()
    {
        PlayerStats.DashCooldown -= 0.3f;
    }
}*/

public class Upgrade_CritChance : Upgrade
{
    private const float CritChanceRate = 0.03f;
    
    public Upgrade_CritChance() : base("Critical Systems", UpgradeType.CritChance) { }

    public override string GetDescription()
    {
        return
            $"Increases critical hit chance from <color=#FFD700>{DataManager.playerStats.CritChance}%</color> to <color=#FFD700>{DataManager.playerStats.CritChance + CritChanceRate}%</color>.";
    }
    
    public override void Apply()
    {
        DataManager.playerStats.SetCritChance(CritChanceRate);
    }
}


public class Upgrade_PiercingChance : Upgrade
{
    public Upgrade_PiercingChance() : base( "Pierce Protocol", UpgradeType.PiercingChance) { }

    public override string GetDescription()
    {
        return
            "Increases Piercing Round drop rate.";
    }
    
    public override void Apply()
    {
        PowerUpManager.AddDropRateBonus("PiercingRounds", 5f);
    }
}

public class Upgrade_RapidFireChance : Upgrade
{
    public Upgrade_RapidFireChance() : base( "Reflex Chip", UpgradeType.RapidFireChance) { }

    public override string GetDescription()
    {
        return
            "Increases Rapid Fire PowerUp chance.";
    }
    
    public override void Apply()
    {
        PowerUpManager.AddDropRateBonus("RapidFire", 5f);
    }
}

