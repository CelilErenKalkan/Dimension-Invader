using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is a data container for defining a wave's properties.
// It cannot be attached to a GameObject because it doesn't derive from MonoBehaviour.
[System.Serializable] 
public class Wave
{
    public string waveName = "New Wave";
    public int enemyCount = 10;
    public float spawnInterval = 1f;
    public float timeToNextWave = 5f;
}

// This is the main script you attach to your "EnemySpawner" GameObject.
// The class name "EnemySpawner" must exactly match the file name "EnemySpawner.cs".
public class EnemySpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    public List<Wave> waves; 

    [Header("Pool Settings")]
    public PoolItemType enemyPoolType; 

    [Header("Spawning Settings")]
    public float spawnDistance = 20f;

    [Header("Hierarchy Organization")]
    [SerializeField] private Transform enemyContainer; 

    // Private variables for internal logic
    private Pool _pool;
    private Transform _playerTransform;
    private int _currentWaveIndex = 0;

    void Start()
    {
        // Get references to essential components
        _pool = Pool.Instance;
        _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Start the main coroutine that handles the wave logic
        StartCoroutine(WaveRoutine());
    }

    private IEnumerator WaveRoutine()
    {
        // Loop through all the waves defined in the list
        while (_currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[_currentWaveIndex];
            
            // Spawn all enemies for the current wave
            for (int i = 0; i < currentWave.enemyCount; i++)
            {
                SpawnEnemy();
                // Wait for the specified interval before spawning the next enemy
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }

            // Wait for a cooldown period before starting the next wave
            yield return new WaitForSeconds(currentWave.timeToNextWave);

            _currentWaveIndex++;
        }

        Debug.Log("All waves completed!");
    }

    private void SpawnEnemy()
    {
        if (_playerTransform == null) return; // Safety check if player is not found
        
        // Calculate a random spawn position in a circle around the player
        Vector2 randomDirection = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 spawnPosition = _playerTransform.position + new Vector3(randomDirection.x, 0, randomDirection.y);

        // Request an enemy from the pool and set its parent to the container
        _pool.SpawnObject(spawnPosition, enemyPoolType, enemyContainer);
    }
}