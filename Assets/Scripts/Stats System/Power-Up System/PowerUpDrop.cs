using System.Collections.Generic;
using UnityEngine;

public class PowerUpDrop : MonoBehaviour
{
    public PowerUpManager manager;

    void OfferPowerUpsToPlayer()
    {
        List<PowerUp> options = manager.GetRandomPowerUps(3);

        foreach (PowerUp option in options)
        {
            Debug.Log($"Option: {option.Name} - {option.Description}");
            // Show to UI & add button:
            // onClick => manager.AddPowerUp(option);
        }
    }
}

