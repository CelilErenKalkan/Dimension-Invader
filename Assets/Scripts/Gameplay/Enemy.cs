using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float shootCooldown = 1.5f;
        [SerializeField] private float fireRange = 15f;

    private Transform player;
     private Coroutine shootCoroutine;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(ShootRoutine());

        if (shootCoroutine == null)
        {
            shootCoroutine = StartCoroutine(ShootRoutine());
        }
    }

     private void OnDisable()
    {
        
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
    }

     private void Update()
    {
        
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            transform.LookAt(player);
        }
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
            if (player != null && Vector3.Distance(transform.position, player.position) <= fireRange)
            {
                Vector3 direction = (player.position - transform.position).normalized;

                GameObject bullet = Pool.Instance.SpawnObject(transform.position, PoolItemType.Bullet, null);
                if (bullet != null && bullet.TryGetComponent(out Bullet bulletScript))
                {
                    bulletScript.SetDirection(direction);
                    bulletScript.SetDamage(GameMechanics.GiveDamage()); // Enemy uses same damage logic for now
                }
            }

            yield return wait;
        }
    }
}