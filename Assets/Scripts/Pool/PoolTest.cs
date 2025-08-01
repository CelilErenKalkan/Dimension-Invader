using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    private Pool _pool;

    [Header("Spawner Ayarları")]
    [SerializeField] private Transform obstacleParent;
    [SerializeField] private float spawnInterval = 1.5f; 
    [SerializeField] private float spawnDistance = 50f;  
    [SerializeField] private float spawnWidth = 5f;
    [SerializeField] private float spwanHeight = 10f;

    private readonly PoolItemType[] _obstacleTypes =
    {
        PoolItemType.Obstacles
    };

    // Start is called before the first frame update
    void Start()
    {
        _pool = Pool.Instance;

        StartCoroutine(SpawnObstaclesRoutine());

    }
    

     private IEnumerator SpawnObstaclesRoutine()
    {

        while (true)
        {

            yield return new WaitForSeconds(spawnInterval);

            int randomIndex = Random.Range(0, _obstacleTypes.Length);
            PoolItemType randomTypeToSpawn = _obstacleTypes[randomIndex];



            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnWidth, spawnWidth),
                Random.Range(0, spwanHeight),
                spawnDistance
            );

            _pool.SpawnObject(spawnPosition, PoolItemType.Obstacles, obstacleParent);
        }
    }
}
