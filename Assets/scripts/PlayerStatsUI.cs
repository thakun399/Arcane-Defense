using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [Header("Player References")]
    public PlayerVision playerVision;
    public Projectile2D playerProjectile;
    public Bullet bulletPrefab; // ใช้เพื่อดึงค่า damage

    [Header("UI Elements")]
    public TMP_Text dmgText;
    public TMP_Text speedText;
    public TMP_Text rangeText;

    public Image dmgIcon;
    public Image speedIcon;
    public Image rangeIcon;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (bulletPrefab != null && dmgText != null)
            dmgText.text = $"DMG: {bulletPrefab.damage}";

        if (playerProjectile != null && speedText != null)
            speedText.text = $"Speed: {playerProjectile.SpeedAttack:F2}";

        if (playerVision != null && rangeText != null)
            rangeText.text = $"Range: {playerVision.Range:F1}";
    }
}