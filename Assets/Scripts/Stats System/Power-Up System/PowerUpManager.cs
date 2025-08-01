using System;
using System.Collections.Generic;
using System.Linq;

public class PowerUpManager
{
    private readonly List<PowerUp> activePowerUps = new();
    private readonly List<Func<PowerUp>> powerUpPool;

    public PowerUpManager()
    {
        powerUpPool = new()
        {
            () => new PowerUp_AttackSpeed(),
            () => new PowerUp_CritChance(),
            () => new PowerUp_MoveSpeed(),
            () => new PowerUp_Shield(),
            () => new PowerUp_ShieldRegeneration(),
            () => new PowerUp_ExtraProjectile(),
        };
    }

    public void AddPowerUp(PowerUp powerUp)
    {
        powerUp.Apply();
        activePowerUps.Add(powerUp);
    }

    public List<PowerUp> GetRandomPowerUps(int count)
    {
        return powerUpPool
            .OrderBy(_ => UnityEngine.Random.value)
            .Take(count)
            .Select(p => p())
            .ToList();
    }

    public void ClearAll()
    {
        foreach (var powerUp in activePowerUps)
            powerUp.Remove();

        activePowerUps.Clear();
    }
    
    private void OnApplicationQuit()
    {
        ClearAll();   
    }

    public List<PowerUp> GetActivePowerUps() => new(activePowerUps);
}
