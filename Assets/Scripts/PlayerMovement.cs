using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private float MOVE_SPEED = 5.0f;

	private Vector2 velocity;
    private Vector2 acceleration;

    private GameObject camera;
	
	// Use this for initialization
	void Start () {
		velocity = Vector2.zero;
		acceleration = Vector2.zero;

        camera = GameObject.FindWithTag("MainCamera");
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

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			velocity.x -= MOVE_SPEED;
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			velocity.x += MOVE_SPEED;
		}
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			velocity.y += MOVE_SPEED;
		}
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			velocity.y -= MOVE_SPEED;
		}
	}
	
	private void UpdateMovement()
	{
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
}
