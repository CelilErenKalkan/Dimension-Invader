using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float shootCooldown = 1.5f;

    private Transform player;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(ShootRoutine());
    }

    public void TakeDamage(float amount)
    {
        // Handle enemy health and death logic here
        Debug.Log($"{name} took {amount} damage.");
    }

    private IEnumerator ShootRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(shootCooldown);

        while (true)
        {
            if (player != null)
            {
                Vector3 direction = (player.position - transform.position).normalized;

                GameObject bullet = Pool.Instance.SpawnObject(transform.position, PoolItemType.Bullet, null);
                if (bullet.TryGetComponent(out Bullet bulletScript))
                {
                    bulletScript.SetDirection(direction);
                    bulletScript.SetDamage(GameMechanics.GiveDamage()); // Enemy uses same damage logic for now
                }
            }

            yield return wait;
        }
    }
}