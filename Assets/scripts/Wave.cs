using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemyPrefabs;
    public int enemyCount;
    public float spawnInterval = 1.5f;
    public float waveInterval = 5f;
}
