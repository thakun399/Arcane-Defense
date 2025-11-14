using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class PlayerStats : MonoBehaviour
{
    public float Damage = 10f;
    public float SpeedAttack = 1f;
    public float Range = 5f;

    public Transform shootPoint;
    public Rigidbody2D bulletPrefab;
    public AudioClip shootSound;
    public SpriteRenderer fogSprite;

    private float nextFireTime = 0f;
    private AudioSource audioSource;
    private Material fogMat;

    // 🔹 เพิ่ม Event สำหรับแจ้ง UI
    public event Action OnStatsChanged;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (fogSprite != null)
            fogMat = fogSprite.material;
    }

    void Update()
    {
        HandleShooting();
        UpdateVision();
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + SpeedAttack;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                Vector2 targetPos = hit.point;
                Rigidbody2D shootBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                Vector2 velocity = CalculateProjectileVelocity(shootPoint.position, targetPos, 1f);
                shootBullet.velocity = velocity;

                Bullet bullet = shootBullet.GetComponent<Bullet>();
                if (bullet != null)
                    bullet.damage = Damage;

                if (audioSource != null && shootSound != null)
                    audioSource.PlayOneShot(shootSound);
            }
        }
    }

    void UpdateVision()
    {
        if (fogMat != null)
        {
            fogMat.SetVector("_PlayerPos", new Vector4(transform.position.x, transform.position.y, 0, 0));
            fogMat.SetFloat("_Radius", Range);
        }
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;
        return new Vector2(velocityX, velocityY);
    }

    // 🔹 ปรับฟังก์ชันบัฟ ให้เรียก Event ด้วย
    public void AddDamage(float value, bool isPercentage)
    {
        if (isPercentage) Damage *= 1 + value / 100f;
        else Damage += value;

        OnStatsChanged?.Invoke(); // แจ้ง UI
    }

    public void AddRange(float value, bool isPercentage)
    {
        if (isPercentage) Range *= 1 + value / 100f;
        else Range += value;

        OnStatsChanged?.Invoke();
    }

    public void AddSpeedAttack(float value, bool isPercentage)
    {
        if (isPercentage) SpeedAttack *= 1 - value / 100f; // ลดเวลา cooldown
        else SpeedAttack -= value;

        SpeedAttack = Mathf.Max(0.05f, SpeedAttack);

        OnStatsChanged?.Invoke();
    }
}

