using System.Collections;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private Transform shootParent;
    [SerializeField] private float detectionRadius = 10f;

    private float shootTimer;

    private void OnEnable()
    {
        StartCoroutine(ShieldRegenRoutine());
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Transform closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                Shoot(closestEnemy.position);
                shootTimer = GameMechanics.GetShotCooldown();
            }
        }
    }

    private void Shoot(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - shootOrigin.position).normalized;

        // Apply spread
        float spreadAngle = GameMechanics.GetBulletSpread(DataManager.playerStats.BulletSpread);
        direction = Quaternion.Euler(0, Random.Range(-spreadAngle, spreadAngle), 0) * direction;

        // Spawn bullet
        GameObject bullet = Pool.Instance.SpawnObject(shootOrigin.position, PoolItemType.Bullet, shootParent);

        // Set bullet direction, damage, etc. if needed
        if (bullet.TryGetComponent(out Bullet bulletScript))
        {
            bulletScript.SetDirection(direction);
            bulletScript.SetDamage(GameMechanics.GiveDamage());
        }
    }

    private Transform FindClosestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, LayerMask.GetMask("Enemy"));
        Transform closest = null;
        float minDistSqr = float.MaxValue;

        foreach (Collider hit in hits)
        {
            float distSqr = (hit.transform.position - transform.position).sqrMagnitude;
            if (distSqr < minDistSqr)
            {
                minDistSqr = distSqr;
                closest = hit.transform;
            }
        }

        return closest;
    }

    private IEnumerator ShieldRegenRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        while (true)
        {
            var stats = DataManager.playerStats;
            if (stats.ShieldPower < stats.MaxShieldPower)
            {
                GameMechanics.RegenerateShield();
            }

            yield return wait;
        }
    }
}
