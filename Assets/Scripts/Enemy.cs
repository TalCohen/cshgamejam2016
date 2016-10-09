using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject player;


    private Vector3 playerPos;
    private Vector3 position;
    private Vector3 velocity;

    public float speed = 2;

    // Use this for initialization
    void Start()
    {
        playerPos = player.transform.position;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
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
    
}
