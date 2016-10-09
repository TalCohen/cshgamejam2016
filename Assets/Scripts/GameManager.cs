using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject PlayerPrefab;
    private GameObject player;

    public GameObject RedEnemyPrefab;
    private GameObject redEnemy;

	// Use this for initialization
	void Start () {
        player = (GameObject)Instantiate(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        redEnemy = (GameObject)Instantiate(RedEnemyPrefab, new Vector3(1, 0, 0), Quaternion.identity);
        redEnemy.GetComponent<Enemy>().SetGameManager(this);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }
}
