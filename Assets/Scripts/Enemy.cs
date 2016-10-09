using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    private GameManager gameManager;

    private Vector3 playerPos;
    private Vector3 position;
    private Vector3 velocity;

    private float speed = 2;

    // Use this for initialization
    void Start()
    {
        playerPos = gameManager.GetPlayerPosition();
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = gameManager.GetPlayerPosition();
        velocity = UpdateVelocity(playerPos);
        position += velocity * Time.deltaTime;
        transform.position = position;
    }

    Vector3 UpdateVelocity(Vector3 playerPosition)
    {
        Vector3 velocity = playerPosition - transform.position;
        velocity = velocity.normalized * speed;
        return velocity;
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
