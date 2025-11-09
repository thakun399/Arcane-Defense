using UnityEngine;

public class ItemSpawnToggle : MonoBehaviour
{
    public GameObject wallPrefab;  
    private GameObject currentWall;
    public float respawnTime = 20f;

    void Start()
    {
        SpawnWall(); 
    }

    void SpawnWall()
    {
      
        currentWall = Instantiate(wallPrefab, transform.position, Quaternion.identity);

       
        SuperPower wallScript = currentWall.GetComponent<SuperPower>();
        if (wallScript != null)
        {
            wallScript.respawner = this; 
        }
    }

    public void OnWallDestroyed()
    {
        
        Invoke(nameof(SpawnWall), respawnTime); 
    }
}