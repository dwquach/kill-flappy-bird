using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public GameObject boss;
    public float maxSpawnRate = 0.1f;
    public float heightMin = -2f;
    public float heightMax = 2.5f;
    public float maxEnemySpeed = -10f;
    public float spawnCap = 10;

    private float currentSpawnRate = 0.4f;
    private float currentSpawnCap = 6;
    private float currentMaxSpeed = -4f;
    private float timeSinceLastSpawned = 0;
    private float spawnXPosition = 11f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.instance.killGoal <= 0)
        {
            Instantiate(boss, new Vector2(15, 0.5f), Quaternion.identity);
            Destroy(gameObject);
        }

        timeSinceLastSpawned += Time.deltaTime;

        if (GameController.instance.gameOver == false && timeSinceLastSpawned >= currentSpawnRate && GameController.instance.spawnedEnemies < currentSpawnCap)
        {
            timeSinceLastSpawned = 0;
            float spawnYPosition = Random.Range(heightMin, heightMax);
            float enemySpeed = GameController.instance.scrollSpeed + Random.Range(maxEnemySpeed, -2f);

            GameObject newEnemy = Instantiate(enemy, new Vector2(spawnXPosition, spawnYPosition), Quaternion.identity);
            newEnemy.GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed, 0);

            GameController.instance.spawnedEnemies++;
        }
	}

    private void increaseDifficulty()
    {
        if (currentSpawnRate > maxSpawnRate)
        {
            currentSpawnRate = currentSpawnRate - 0.3f;
        }

        if (currentMaxSpeed > maxEnemySpeed)
        {
            maxEnemySpeed--;
        }

        if (currentSpawnCap < spawnCap)
        {
            currentSpawnCap += 2;
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameController.instance.gameStarted == true && other.gameObject.CompareTag("New Level"))
        {
            increaseDifficulty();
        }
    }
}
