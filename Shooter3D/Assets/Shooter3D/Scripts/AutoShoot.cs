using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    public float shootRange = 5f;
    public float shootCooldown = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;

    private float lastShootTime = 0f;

    void Update()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy == null) return;

        float distance = Vector3.Distance(transform.position, nearestEnemy.transform.position);

        if (distance <= shootRange)
        {
            // Xoay mặt về enemy
            Vector3 dir = (nearestEnemy.transform.position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 10f);

            // Bắn nếu đủ thời gian
            if (Time.time - lastShootTime >= shootCooldown)
            {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float shortestDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < shortestDist)
            {
                shortestDist = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }
}
