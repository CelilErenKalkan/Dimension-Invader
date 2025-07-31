using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float shootCooldown = 1.5f;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private Transform shootParent;

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
                Vector3 direction = (player.position - shootOrigin.position).normalized;

                GameObject bullet = Pool.Instance.SpawnObject(shootOrigin.position, PoolItemType.Bullet, shootParent);
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