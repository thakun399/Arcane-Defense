using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 1;
    public int maxHealth = 40;
    private int currentHealth;

    public int scoreValue = 10;
    private GameManager GameManager;
    public AudioClip hitBaseSound;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();

    }
    
   
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage! Remaining: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");


        if (GameManager != null)
        {
            GameManager.AddScore(scoreValue);

            GameManager.PlayMonsterDeathSound();
        }


        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            Tower tower = collision.GetComponent<Tower>();
            if (tower != null)
            {
                tower.TakeDamage(damage);
            }

            if (hitBaseSound != null)
            {
                AudioSource.PlayClipAtPoint(hitBaseSound, transform.position, 2.5f);
            }

            Destroy(gameObject);
        }
    }
    

}
