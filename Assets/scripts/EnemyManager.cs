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

    // ใช้เฉพาะ DMG เพราะ stat อื่นให้ Enemy อ่านจาก multipliers ตอน Spawn
    public void ApplyEnemyBuff(CardEffect effect)
    {
        foreach (var enemy in activeEnemies)
        {
            if (effect.stat == StatType.DMG)
            {
                enemy.damage += Mathf.RoundToInt(effect.value);
            }
        }
    }
}