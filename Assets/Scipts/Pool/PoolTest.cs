using UnityEngine;

public class PoolTest : MonoBehaviour
{
    private Pool _pool;
    private GameObject spawnedObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (_pool == null)
            _pool = Pool.Instance;
        
        //spawnedObject = _pool.SpawnObject(Vector3.zero, PoolItemType.Obstacle, null);
    }

    private void OnDisable()
    {
        //if (spawnedObject != null)
            //_pool.DeactivateObject(spawnedObject, PoolItemType.Obstacle);
    }
}
