using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isGameOver = false;

    [Header("UI Elements")]
    public GameObject gameOverUI;
    public GameObject victoryUI;
    public TMP_Text finalScoreText;
    public TMP_Text victoryScoreText;

    [Header("Audio")]
    public AudioClip monsterDeathSound;
    public AudioClip gameOverSound;
    public AudioClip victorySound;
    public AudioClip backgroundMusic;

    private AudioSource audioSource;
    private AudioSource musicSource;

    [Range(0f, 1f)] public float musicVolume = 0.5f;

    [Header("Score")]
    public int score = 0;
    public TMP_Text scoreText;

    [Header("Win Condition")]
    public int scoreToWin = 2000;

    [Header("Player & Enemy References")]
    public PlayerStats playerStats; // <== เพิ่มช่องนี้ไว้โยง Player
    public EnemyManager enemyManager; // <== ใช้สำหรับอัปเดตค่า Enemy ทั้งหมด

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreText();
        victoryUI.SetActive(false);
        gameOverUI.SetActive(false);

        audioSource = GetComponent<AudioSource>();

        // จัดการเพลงพื้นหลัง
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0f;

        if (musicSource != null)
            musicSource.Stop();

        if (gameOverSound != null && audioSource != null)
            audioSource.PlayOneShot(gameOverSound);

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = "Final Score: " + score.ToString();

        Debug.Log("Game Over!");
    }

    public void WinGame()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0f;

        if (musicSource != null)
            musicSource.Stop();

        if (victorySound != null && audioSource != null)
            audioSource.PlayOneShot(victorySound);

        if (victoryUI != null)
            victoryUI.SetActive(true);

        if (victoryScoreText != null)
            victoryScoreText.text = "\nFinal Score: " + score.ToString();

        Debug.Log("You Win!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    public void PlayMonsterDeathSound()
    {
        if (monsterDeathSound != null && audioSource != null)
            audioSource.PlayOneShot(monsterDeathSound);
    }

    // =====================================================
    // 🎴 ฟังก์ชันใหม่: ใช้ปรับ Buff/Debuff จากการ์ด
    // =====================================================
    public void ApplyBuffs(CardEffect[] effects)
    {
        foreach (var effect in effects)
        {
            if (effect.target == TargetType.Player && playerStats != null)
            {
                switch (effect.stat)
                {
                    case StatType.DMG:
                        playerStats.AddDamage(effect.value, effect.isPercentage);
                        break;

                    case StatType.SpeedAttack:
                        playerStats.AddSpeedAttack(effect.value, effect.isPercentage);
                        break;

                    case StatType.Range:
                        playerStats.AddRange(effect.value, effect.isPercentage);
                        break;
                }
            }
            else if (effect.target == TargetType.Enemies && enemyManager != null)
            {
                enemyManager.ApplyEnemyBuff(effect);
            }
        }

        Debug.Log("Buffs applied from selected card!");
    }

}
