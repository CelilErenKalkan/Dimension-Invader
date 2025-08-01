using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; 
    [SerializeField] private float spawnCooldown = 0.5f; 
    [SerializeField] private float spawnRange = 20f; 

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnCooldown);

        while (true)
        {
            yield return wait;

            if (player != null)
            {
                
                Vector2 randomDirection = Random.insideUnitCircle.normalized * spawnRange;
                
                
                Vector3 spawnPosition = new Vector3(randomDirection.x, 0f, randomDirection.y) + player.position;

                
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}