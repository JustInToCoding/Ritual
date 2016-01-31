using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;                // The enemy prefabs to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    private int spawnedEnemies = 0;
    private List<GameObject> aliveEnemies;
    private int aliveEnemiesTotal = 0;
    public int maxAliveEnemies;
    public int maxSpawnedEnemies;


    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        aliveEnemies = new List<GameObject>();
    }

    void Update()
    {
        List<GameObject> deadEnemies = new List<GameObject>();
        foreach( GameObject enemy in aliveEnemies)
        {
            if(enemy == null)
            {
                deadEnemies.Add(enemy);
            }
        }
        foreach( GameObject enemy in deadEnemies)
        {
            aliveEnemies.Remove(enemy);
        }
    }

    void Spawn()
    {
        if(aliveEnemies.Count < maxAliveEnemies && spawnedEnemies < maxSpawnedEnemies)
        {
            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            // Find a random index between zero and one less than the number of enemy prefabs.
            int enemyIndex = Random.Range(0, enemies.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            aliveEnemies.Add( Instantiate(enemies[enemyIndex], 
                new Vector2(spawnPoints[spawnPointIndex].position.x, spawnPoints[spawnPointIndex].position.y), 
                spawnPoints[spawnPointIndex].rotation) as GameObject);
            spawnedEnemies++;
            Debug.Log(spawnedEnemies);
        }
    }
}