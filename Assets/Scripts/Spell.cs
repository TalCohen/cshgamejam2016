using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    private Utilities.ColorType colorType;

	// Use this for initialization
	void Start () {
        colorType = Utilities.ColorType.None;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetColor(Utilities.ColorType colorType)
    {
        if (this.colorType == Utilities.ColorType.None)
        {
            this.colorType = colorType;
            this.GetComponent<SpriteRenderer>().color = Utilities.ColorTypeToColor(colorType);
        }
    }
}
