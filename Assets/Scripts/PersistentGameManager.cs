using UnityEngine;
using System.Collections;

public class PersistentGameManager : MonoBehaviour {
    public GameObject GameOverScreen;

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

        Sprite sprite;

        if (playerWon)
        {
            sprite = Resources.Load<Sprite>("youLose");
        } else
        {
            sprite = Resources.Load<Sprite>("youLose");
        }

        GameOverScreen.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
