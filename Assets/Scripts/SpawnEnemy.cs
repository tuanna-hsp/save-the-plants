using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wave {
	public GameObject[] enemyPrefabs;
	public float spawnInterval = 2;
	public int maxEnemies = 20;

    public GameObject getNextEnemyPrefab()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }

    public void increasePower()
    {
        spawnInterval /= 2;
        maxEnemies = (int) (maxEnemies * 1.5f);
    }

    public void decreasePower()
    {
        spawnInterval *= 2;
        maxEnemies = (int) (maxEnemies / 1.5f);
    }
}

public class SpawnEnemy : MonoBehaviour {

	public GameObject[] waypoints;
	public GameObject testEnemyPrefab;

	public Wave[] waves;
	public int timeBetweenWaves = 5;
	
	private GameManagerBehavior gameManager;
	
	private float lastSpawnTime;
	private int enemiesSpawned = 0;

	// Use this for initialization
	void Start () {
		lastSpawnTime = Time.time;
		gameManager =
			GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        gameManager.WaveCount = waves.Length;

        Difficulty difficulty = PersistantManager.GetDifficulty();
        switch (difficulty)
        {
            case Difficulty.EASY:
                foreach (Wave wave in waves)
                {
                    wave.decreasePower();
                }
                break;
            case Difficulty.HARD:
                foreach (Wave wave in waves)
                {
                    wave.increasePower();
                }
                break;
        }

        // Update target position according to current map
        GameObject target = GameObject.Find("Target");
        GameObject pos = GameObject.Find("TargetPosition");
        target.transform.position = pos.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// 1
		int currentWave = gameManager.Wave;
		if (currentWave < waves.Length) {
			// 2
			float timeInterval = Time.time - lastSpawnTime;
			float spawnInterval = waves[currentWave].spawnInterval;
			if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
			     timeInterval > spawnInterval) && 
			    enemiesSpawned < waves[currentWave].maxEnemies) {
				// 3  
				lastSpawnTime = Time.time;
				GameObject newEnemy = Instantiate(waves[currentWave].getNextEnemyPrefab());
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
				enemiesSpawned++;
			}
			// 4 
			if (enemiesSpawned == waves[currentWave].maxEnemies &&
			    GameObject.FindGameObjectWithTag("Enemy") == null) {
                gameManager.Wave++;
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
			// 5 
		} else {
            gameManager.doGameWon();
		}	
	}
}
