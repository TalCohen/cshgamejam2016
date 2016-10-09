using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    private float MOVE_SPEED = 2.0f;

    private Utilities.ColorType colorType;

    private Vector2 direction;
    private Vector2 velocity;

	// Use this for initialization
	void Start () {
        colorType = Utilities.ColorType.None;

        direction = Vector2.zero;
        velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {

        velocity += direction;
        gameObject.transform.position += (Vector3)velocity * Time.deltaTime;
        velocity = Vector2.zero;
	}

    public void SetColor(Utilities.ColorType colorType)
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
}
