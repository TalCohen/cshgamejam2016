using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    private GameManager gameManager;

    private Vector3 playerPos;
    private Vector3 position;
    private Vector3 velocity;

    private float moveSpeed;

    private Utilities.ColorType colorType;

    public int damage { get; private set; }

    private static int DEATH_ANIMATION_FRAMES = 20;

    // Use this for initialization
    void Start()
    {
        playerPos = gameManager.GetPlayerPosition();
        position = transform.position;
        damage = 34;
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
        velocity = velocity.normalized * moveSpeed;
        return velocity;
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void SetColorType(Utilities.ColorType colorType)
    {
        if (this.colorType == Utilities.ColorType.None)
        {
            this.colorType = colorType;
        }
    }

    public Utilities.ColorType GetColorType()
    {
        return this.colorType;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public void Die()
    {
        // Tell the Game Manager
        gameManager.IncrementKillCount();

        // Remove the box collider
        Destroy(GetComponent<BoxCollider2D>());

        // Start fading out over time
        StartCoroutine(Utilities.FadeOut(this.gameObject, DEATH_ANIMATION_FRAMES));
    }
}
