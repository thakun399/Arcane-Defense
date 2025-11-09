using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject target; 
    [SerializeField] Rigidbody2D bulletPrefab;

    [SerializeField] float cooldownTime = 1f; 
    private float nextFireTime = 0f; 
    
    [SerializeField] AudioClip shootSound;//กั้ง
    private AudioSource audioSource;
    
    void Start()//ตรงนี้เพิ่มโดยกั้ง
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            
            nextFireTime = Time.time + cooldownTime;

            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 5f);

            
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            
            if (hit.collider != null)
            {
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log("hit " + hit.collider.name);

                
                Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);

                
                Rigidbody2D shootBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

                
                shootBullet.velocity = projectileVelocity; 
                audioSource.PlayOneShot(shootSound);//ตรงนี้เพิ่มโดยกั้ง
            }
            
        }
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        
        Vector2 projectileVelocity = new Vector2(velocityX, velocityY);

        return projectileVelocity;
    }
    
}