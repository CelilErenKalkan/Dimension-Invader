using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 10f;

    
    private float deactivationZPoint = -10f;
    
    void Update()
    {
        
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

       
        if (transform.position.z < deactivationZPoint)
        {
            
            Pool.Instance.DeactivateObject(gameObject, PoolItemType.Obstacles);
            
        }
    }
}