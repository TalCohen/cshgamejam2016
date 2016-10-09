using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    private float MOVE_SPEED = 3.0f;

    private Utilities.ColorType colorType;

    private Vector2 direction;
    private Vector2 velocity;

	// Use this for initialization
	void Start () {
        velocity = Vector2.zero;

        RotateObject();
	}
	
	// Update is called once per frame
	void Update () {
        velocity += direction * MOVE_SPEED;
        gameObject.transform.position += (Vector3)velocity * Time.deltaTime;
        velocity = Vector2.zero;
	}

    public void SetColorType(Utilities.ColorType colorType)
    {
        if (this.colorType == Utilities.ColorType.None)
        {
            this.colorType = colorType;
            this.GetComponent<SpriteRenderer>().color = Utilities.ColorTypeToColor(colorType);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    private void RotateObject()
    {
        // Rotate the object based on its direction
        float angle = Mathf.Atan2(this.direction.y, this.direction.x);
        this.gameObject.transform.Rotate(new Vector3(0, 0, 1), Mathf.Rad2Deg * angle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();

            // Check to see if the colors are correct
            if (this.colorType == enemy.GetColorType())
            {
                // Matching colors! Kill the enemy
                enemy.Die();

                // Destroy the spell
                Destroy(this.gameObject);
            }
            else
            {
                // Non-matching colors
                // Slow enemy down?
            }

            //Destroy(this.gameObject);
        }
    }
}
