// Projectile2D.cs (ปรับจากของคุณ)
using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject target;
    [SerializeField] Rigidbody2D bulletPrefab; // prefab ของกระสุน (มักมี Bullet component ด้วย)

    public float SpeedAttack = 1f;
    private float nextFireTime = 0f;

    [SerializeField] AudioClip shootSound;
    private AudioSource audioSource;

    // ใหม่: อ้างอิง PlayerStats เพื่อดึงค่า Damage, SpeedAttack ฯลฯ
    public PlayerStats playerStats;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + SpeedAttack;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                target.transform.position = hit.point;

                Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);

                // สร้าง Rigidbody2D (prefab)
                Rigidbody2D shootBulletRb = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

                // เซ็ตความเร็ว
                shootBulletRb.velocity = projectileVelocity;

                // ถ้า prefab มาพร้อมกับ Bullet component ให้เซ็ต damage จาก playerStats
                Bullet bulletComp = shootBulletRb.GetComponent<Bullet>();
                if (bulletComp != null && playerStats != null)
                {
                    bulletComp.damage = playerStats.Damage;
                }

                // เล่นเสียง
                if (audioSource != null && shootSound != null)
                    audioSource.PlayOneShot(shootSound);
            }
        }
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;
        return new Vector2(velocityX, velocityY);
    }
}

