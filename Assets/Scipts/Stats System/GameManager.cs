using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DataManager.LoadData();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Flux: " + DataManager.playerStats.TotalFlux);
        Debug.Log("Fire Power: " + DataManager.playerStats.FirePower);
        Debug.Log(UpgradeManager.AllUpgrades[0].GetDescription());
        Debug.Log("Level: " + UpgradeManager.AllUpgrades[0].Level);
        DataManager.playerStats.SetTotalFlux(20);
        UpgradeManager.Purchase(0);
    }
}
