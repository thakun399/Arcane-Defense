using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 1;
    public float maxHealth = 40;
    private float currentHealth;
    public float speed = 10f;
    public int scoreValue = 10;
    public AudioClip hitBaseSound;

    private GameManager gameManager;

    public System.Action<Enemy> OnEnemyDead;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        gameManager = GameManager.Instance;

        // ⭐ Load multipliers
        maxHealth *= gameManager.enemyHpMultiplier;
        speed *= gameManager.enemySpeedMultiplier;
        scoreValue = Mathf.RoundToInt(scoreValue * gameManager.enemyScoreMultiplier);

        currentHealth = maxHealth;

        EnemyManager.Instance.RegisterEnemy(this);
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnEnemyDead?.Invoke(this);
        EnemyManager.Instance.UnregisterEnemy(this);

        if (gameManager != null)
        {
            gameManager.AddScore(scoreValue);
            gameManager.PlayMonsterDeathSound();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower"))
        {
            Tower tower = collision.GetComponent<Tower>();
            if (tower != null) tower.TakeDamage(damage);

            if (hitBaseSound != null)
                AudioSource.PlayClipAtPoint(hitBaseSound, transform.position, 2.5f);

            Die();
        }
    }
}