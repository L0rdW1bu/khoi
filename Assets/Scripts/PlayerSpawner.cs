using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public NPCController npcController;
    public Transform enemyTarget;

    private bool hasSpawned = false;

    void Update()
    {
    
        if (!hasSpawned && Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        npcController.SetTarget(enemyTarget);
        hasSpawned = true;
    }
}