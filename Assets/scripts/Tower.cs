using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public int maxHP = 3;
    private int currentHP;

    public Image[] heartImages; 
    public Sprite heartFull;
    public Sprite heartEmpty;

    void Start()
    {
        currentHP = maxHP;
        UpdateHearts();
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHearts();

        if (currentHP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            int reverseIndex = heartImages.Length - 1 - i;

            if (i < currentHP)
                heartImages[reverseIndex].sprite = heartFull;
            else
                heartImages[reverseIndex].sprite = heartEmpty;
        }
    }

}