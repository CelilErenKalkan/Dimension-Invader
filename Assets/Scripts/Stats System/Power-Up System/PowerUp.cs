public abstract class PowerUp
{
    public string ID { get; private set; }
    public string Name { get; protected set; }
    public string Description { get; protected set; }

    public PowerUp(string id, string name, string description)
    {
        ID = id;
        Name = name;
        Description = description;
    }

    public abstract void Apply();   // What happens when you get this power-up
    public abstract void Remove();  // Optional: called when power-up is removed, if needed
}

public class PowerUp_AttackSpeed : PowerUp
{
    private float bonus;

    public PowerUp_AttackSpeed() : base("attack_speed", "Adrenaline Surge", "Increases attack speed.")
    {
        bonus = 0.25f;
    }

    public override void Apply() => DataManager.playerStats.SetFireSpeed(bonus);
    public override void Remove() => DataManager.playerStats.SetFireSpeed(bonus * -1);
}

public class PowerUp_CritChance : PowerUp
{
    private float bonus;

    public PowerUp_CritChance() : base("crit_chance", "Deadeye", "Boosts critical hit chance.")
    {
        bonus = 0.15f;
    }

    public override void Apply() => DataManager.playerStats.SetCritChance(bonus);
    public override void Remove() => DataManager.playerStats.SetFireSpeed(bonus * -1);
}

public class PowerUp_MoveSpeed : PowerUp
{
    private float bonus;

    public PowerUp_MoveSpeed() : base("move_speed", "Swift Step", "Gotta go fast!")
    {
        bonus = 1.5f;
    }

    public override void Apply() => DataManager.playerStats.SetMoveSpeed(bonus);
    public override void Remove() => DataManager.playerStats.SetMoveSpeed(bonus * -1);
}

public class PowerUp_Shield : PowerUp
{
    private float bonus;
    
    public PowerUp_Shield() : base("shield", "Barrier", "Boosts shields maximum capacity.")
    {
        bonus = 5.0f;
    }

    public override void Apply() => DataManager.playerStats.SetShieldPower(bonus);
    public override void Remove() => DataManager.playerStats.SetShieldPower(bonus * -1);
}

public class PowerUp_ShieldRegeneration : PowerUp
{
    private float bonus;

    public PowerUp_ShieldRegeneration() : base("shield_regeneration", "Remote Repairment", "Get shield regeneration.")
    {
        bonus = 0.2f;
    }

    public override void Apply() => DataManager.playerStats.SetShieldRegen(bonus);
    public override void Remove() => DataManager.playerStats.SetShieldRegen(bonus * -1);
}

public class PowerUp_ExtraProjectile : PowerUp
{
    public PowerUp_ExtraProjectile() : base("extra_projectile", "Fork Shot", "Fire one extra projectile.") {}

    public override void Apply() => DataManager.playerStats.SetProjectileCount(1);
    public override void Remove() => DataManager.playerStats.SetProjectileCount(-1);
}

