using System.Collections.Generic;

public static class PowerUpManager
{
    private static Dictionary<string, float> dropRateBonuses = new();

    public static void AddDropRateBonus(string powerUpId, float amount)
    {
        if (dropRateBonuses.ContainsKey(powerUpId))
            dropRateBonuses[powerUpId] += amount;
        else
            dropRateBonuses[powerUpId] = amount;
    }

    public static float GetDropRateBonus(string powerUpId)
    {
        return dropRateBonuses.TryGetValue(powerUpId, out var value) ? value : 0f;
    }

    public static void Reset()
    {
        dropRateBonuses.Clear();
    }
}