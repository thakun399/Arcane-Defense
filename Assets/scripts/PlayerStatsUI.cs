using UnityEngine;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    public PlayerStats playerStats;

    public TMP_Text dmgText;
    public TMP_Text speedText;
    public TMP_Text rangeText;

    void Update()
    {
        if (playerStats != null)
        {
            playerStats.OnStatsChanged += UpdateUI;
            UpdateUI(); // แสดงค่าครั้งแรก
        }
    }

    void OnDestroy()
    {
        if (playerStats != null)
            playerStats.OnStatsChanged -= UpdateUI;
    }

    void UpdateUI()
    {
        dmgText.text = $"DMG: {playerStats.Damage:F1}";
        speedText.text = $"Speed: {playerStats.SpeedAttack:F2}";
        rangeText.text = $"Range: {playerStats.Range:F1}";
    }
}