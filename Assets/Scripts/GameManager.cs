using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public GameObject SpikesPrefab;

    private GameObject player;
    private int playerKillCount;

    private Sprite[] enemySprites;
    private Dictionary<string, int> enemySpritesMap;

    private RuntimeAnimatorController[] enemyAnimatorControllers;
    private Dictionary<string, int> enemyAnimatorControllersMap;

    private float levelTimer;
    private float nextEnemySpawnTime;

    private static float INITIAL_ENEMY_SPAWN_TIME = 2.0f;

	// Use this for initialization
	void Start () {
        player = (GameObject)Instantiate(PlayerPrefab, new Vector3(-10, 7, 0), Quaternion.identity);

        // Initialize enemy assets
        InitializeEnemySprites();
        InitializeEnemyAnimatorControllers();

        levelTimer = 0.0f;
        nextEnemySpawnTime = INITIAL_ENEMY_SPAWN_TIME;

        //SpawnEnemy(Utilities.ColorType.Red, new Vector3(5, 0, 0));

        Instantiate(SpikesPrefab, new Vector3(0, -5.0f, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
        // Update the timer
        levelTimer += Time.deltaTime;

        // Try to spawn enemies
        SpawnEnemies();

        if (playerKillCount > 10)
        {
            PersistentGameManager pgm = GameObject.Find("PersistentGameManager").GetComponent<PersistentGameManager>();
            pgm.GameOver(true);
        }
	}

    private void InitializeEnemySprites()
    {
        // Get the list of enemy sprites
        enemySprites = Resources.LoadAll<Sprite>("enemy");

        // Create the map of sprite names to index
        enemySpritesMap = new Dictionary<string, int>();
        enemySpritesMap.Add("Red", 0);
        enemySpritesMap.Add("Blue", 4);
        enemySpritesMap.Add("Yellow", 8);
        enemySpritesMap.Add("Purple", 12);
        enemySpritesMap.Add("Green", 16);
        enemySpritesMap.Add("Orange", 20);
    }

    private void InitializeEnemyAnimatorControllers()
    {
        // Get the list of enemy controllers
        enemyAnimatorControllers = Resources.LoadAll<RuntimeAnimatorController>("EnemyAnimatorControllers");

        // Create the map of animator controller names to index
        enemyAnimatorControllersMap = new Dictionary<string, int>();
        for (int i = 0; i < enemyAnimatorControllers.Length; i++)
        {
            enemyAnimatorControllersMap.Add(enemyAnimatorControllers[i].name, i);
        }
    }

    private void SpawnEnemy(Utilities.ColorType colorType, Vector3 pos)
    {
        string colorString = colorType.ToString();

        // Create the enemy object
        GameObject enemyObject = (GameObject)Instantiate(EnemyPrefab, pos, Quaternion.identity);

        // Set the correct sprite
        Sprite sprite = enemySprites[enemySpritesMap[colorString]];
        enemyObject.GetComponent<SpriteRenderer>().sprite = sprite;

        // Set the correct animator controller
        RuntimeAnimatorController controller = enemyAnimatorControllers[enemyAnimatorControllersMap[colorString]];
        enemyObject.GetComponent<Animator>().runtimeAnimatorController = controller;

        // Get the enemy component and set intial values
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetColorType(colorType);
        enemy.SetGameManager(this);
        enemy.SetMoveSpeed(Utilities.GetRandomEnemyMoveSpeed());
    }

    private void SpawnEnemies()
    {
        // If enough time has passed to spawn a new enemy
        if (levelTimer > nextEnemySpawnTime)
        {
            // Update the next spawn time
            nextEnemySpawnTime += Utilities.GetNextEnemySpawnTime();

            // Get a spawn point
            Vector3 pos = new Vector3(0, 0, 0);

            // Get a color type
            Utilities.ColorType colorType = Utilities.GetWeightedRandomColorType();

            // Spawn the new enemy
            SpawnEnemy(colorType, pos);
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }

    public void IncrementKillCount()
    {
        playerKillCount += 1;
    }
}
