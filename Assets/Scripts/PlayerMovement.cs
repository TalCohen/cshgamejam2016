using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	Vector2 velocity;
	Vector2 acceleration;
	
	float MOVEMENT_SPEED = 0.3f;
	
	// Use this for initialization
	void Start() {
		velocity = Vector2.zero;
		acceleration = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update() {
		GetInput();
		UpdateMovement();
	}
	
	void GetInput()
	{
		// Get the move axis value
        float moveHorizontal = GetAxis("LHorizontal") * MOVEMENT_SPEED;
        float moveVertical = GetAxis("LVertical") * -MOVEMENT_SPEED;
        Debug.Log(string.Format("moveHorizontal {0}", moveHorizontal));
        Debug.Log(string.Format("moveVertical {0}", moveVertical));

		// Get the aim axis value
		float aimHorizontal = GetAxis("RHorizontal");
		float aimVertical = GetAxis("RVertical");

        velocity += new Vector2(moveHorizontal, moveVertical);

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			velocity.x -= MOVEMENT_SPEED;
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			velocity.x += MOVEMENT_SPEED;
		}
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			velocity.y += MOVEMENT_SPEED;
		}
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			velocity.y -= MOVEMENT_SPEED;
		}
	}

	float GetAxis(string axis)
	{
		// Get the two axes
        float j1Axis = Input.GetAxis ("J1" + axis);
		float j2Axis = Input.GetAxis ("J2" + axis);

		// Return their average
		return (j1Axis + j2Axis) / 2;
	}
	
	void UpdateMovement()
	{
		velocity += acceleration;
		gameObject.transform.position += (Vector3)velocity;
		acceleration = Vector2.zero;
		velocity = Vector2.zero;
	}
}
