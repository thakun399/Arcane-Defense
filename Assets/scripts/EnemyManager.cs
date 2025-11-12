using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private List<Enemy> activeEnemies = new List<Enemy>();

    void Awake()
    {
        Instance = this;
    }

    public void RegisterEnemy(Enemy e)
    {
        if (!activeEnemies.Contains(e))
            activeEnemies.Add(e);
    }

    public void UnregisterEnemy(Enemy e)
    {
        if (activeEnemies.Contains(e))
            activeEnemies.Remove(e);
    }

    public void ApplyEnemyBuff(CardEffect effect)
    {
        foreach (var enemy in activeEnemies)
        {
            switch (effect.stat)
            {
                case StatType.HP:
                    enemy.maxHealth += enemy.maxHealth * (effect.value / 100f);
                    break;

                case StatType.Speed:
                    enemy.speed += enemy.speed * (effect.value / 100f);
                    break;

                case StatType.ScoreValue:
                    // ✅ แปลง float → int ป้องกัน error
                    enemy.scoreValue += Mathf.RoundToInt(enemy.scoreValue * (effect.value / 100f));
                    break;

                case StatType.DMG:
                    // ถ้า enemy.Damage เป็น int → ใช้ Mathf.RoundToInt
                    enemy.damage += Mathf.RoundToInt(effect.value);
                    break;
            }
        }
    }

}