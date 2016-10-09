using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private float MOVE_SPEED = 5.0f;

	private Vector2 velocity;
    private Vector2 acceleration;

    private GameObject camera;

    private Animator animator;

    private int direction;
    private bool directionChanged;

    private static int LEFT_DIRECTION = 0;
    private static int UP_DIRECTION = 1;
    private static int RIGHT_DIRECTION = 2;
    private static int DOWN_DIRECTION = 3;

	
	// Use this for initialization
	void Start () {
		velocity = Vector2.zero;
		acceleration = Vector2.zero;

        camera = GameObject.FindWithTag("MainCamera");
        animator = GetComponent<Animator>();

        direction = -1;
        directionChanged = false;
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		UpdateMovement();
        UpdateCamera();
	}
	
	private void GetInput()
	{
		// Get the move axis values
        float moveX = Utilities.GetAveragedAxis("MoveX") * MOVE_SPEED;
        float moveY = Utilities.GetAveragedAxis("MoveY") * -MOVE_SPEED;

        // Apply to the velocity
        velocity += new Vector2(moveX, moveY);

        // Get which direction is our dominant one
        int newDirection = GetDominantDirection(velocity);

        // Attempt to change the direction
        ChangeDirection(newDirection);

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			velocity.x -= MOVE_SPEED;
            ChangeDirection(LEFT_DIRECTION);
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			velocity.x += MOVE_SPEED;
            ChangeDirection(RIGHT_DIRECTION);
		}
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			velocity.y += MOVE_SPEED;
            ChangeDirection(UP_DIRECTION);
		}
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			velocity.y -= MOVE_SPEED;
            ChangeDirection(DOWN_DIRECTION);
		}
	}

    private int GetDominantDirection(Vector2 velocity)
    {
        if (velocity.x > velocity.y)
        {
            if (velocity.x > 0)
            {
                return RIGHT_DIRECTION;
            } else
            {
                return LEFT_DIRECTION;
            }
        } else
        {
            if (velocity.y > 0)
            {
                return UP_DIRECTION;
            } else
            {
                return DOWN_DIRECTION;
            }
        }
    }

    private void ChangeDirection(int newDirection)
    {
        if (newDirection != direction)
        {
            directionChanged = true;
            direction = newDirection;
            animator.SetInteger("Direction", direction);
            animator.SetTrigger("Direction Changed");
        }
    }
	
	private void UpdateMovement()
	{
        animator.SetFloat("Speed", velocity.magnitude);
        velocity += acceleration;
		gameObject.transform.position += (Vector3)velocity * Time.deltaTime;
		acceleration = Vector2.zero;
		velocity = Vector2.zero;
	}

    private void UpdateCamera()
    {
        camera.transform.position = new Vector3(
            this.gameObject.transform.position.x,
            this.gameObject.transform.position.y,
            camera.transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HandleTrigger(other);
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        HandleTrigger(other);
    }
    
    private void HandleTrigger(Collider2D other)
    {
        // Check to see if we're colliding with a wall
        if (other.tag == "Wall")
        {
            StayInBounds(other);
        }
    }

    private void StayInBounds(Collider2D wall)
    {
        float newX = transform.position.x;
        float newY = transform.position.y;

        // Handle colliding with each wall
        if (wall.name == "LeftWall")
        {
            newX = Mathf.Max(newX, wall.transform.position.x);
        }
        if (wall.name == "RightWall")
        {
            newX = Mathf.Min(newX, wall.transform.position.x);
        }
        if (wall.name == "TopWall")
        {
            newY = Mathf.Min(newY, wall.transform.position.y);
        }
        if (wall.name == "BottomWall")
        {
            newY = Mathf.Max(newY, wall.transform.position.y);
        }
        
        // Apply the new position
        transform.position = new Vector3(newX, newY, 0);
    }
}
