using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private LayerMask hitMask;

    private Vector3 direction;
    private int pierceRemaining;
    private float damage;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        pierceRemaining = DataManager.playerStats.PierceCount;
        damage = GameMechanics.GiveDamage();
    }

    public void SetDamage(float value)
    {
        damage = value;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        // Raycast ahead to avoid tunneling
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, speed * Time.deltaTime, hitMask))
        {
            GameObject target = hit.collider.gameObject;

            if (target.CompareTag("Enemy"))
            {
                if (target.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(damage);
                }
            }
            else if (target.CompareTag("Player"))
            {
                GameMechanics.TakeDamage(damage);
            }

            pierceRemaining--;

            if (pierceRemaining < 0)
            {
                Pool.Instance.DeactivateObject(gameObject, PoolItemType.Bullet);
            }
        }
    }
}