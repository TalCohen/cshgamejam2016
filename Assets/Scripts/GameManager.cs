using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public GameObject PlayerPrefab;
    private GameObject player;

    public GameObject EnemyPrefab;

    private Sprite[] enemySprites;
    private Dictionary<string, int> enemySpritesMap;

    private RuntimeAnimatorController[] enemyAnimatorControllers;
    private Dictionary<string, int> enemyAnimatorControllersMap;

	// Use this for initialization
	void Start () {
        player = (GameObject)Instantiate(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // Initialize enemy assets
        InitializeEnemySprites();
        InitializeEnemyAnimatorControllers();

        SpawnEnemy("Red", new Vector3(5, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {

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

    private void SpawnEnemy(string colorString, Vector3 pos)
    {
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
        enemy.SetColorType(Utilities.StringToColorType(colorString));
        enemy.SetGameManager(this);
    }

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }
}
