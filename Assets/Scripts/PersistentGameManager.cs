using UnityEngine;
using System.Collections;

public class PersistentGameManager : MonoBehaviour {
    public GameObject GameOverScreenPrefab;

	// Use this for initialization
	void Start () {
        // Persist this object
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GameOver(bool playerWon)
    {
        Application.LoadLevel("GameOver");

        GameObject gameOverScreen = (GameObject)Instantiate(GameOverScreenPrefab, new Vector3(), Quaternion.identity);

        Sprite sprite;

        if (playerWon)
        {
            sprite = Resources.Load<Sprite>("youWin");
        } else
        {
            sprite = Resources.Load<Sprite>("youLose");
            print(sprite);
        }

        gameOverScreen.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
