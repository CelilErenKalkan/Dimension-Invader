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
    }
}
