using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour {
    private Utilities.ColorType p1Spell;
    private Utilities.ColorType p2Spell;

    private Vector2 aimDirection;

	// Use this for initialization
	void Start () {
        p1Spell = Utilities.ColorType.None;
        p2Spell = Utilities.ColorType.None;
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();

        if (Input.GetButtonDown("J1Blue"))
        {
            print("It's down");
        }
        print(p1Spell);
        print(p2Spell);
	}

    private void GetInput()
    {
        UpdateSpells();
        UpdateAimDirection();
        CheckCastSpell();
    }

    private void UpdateSpells()
    {
        // Update the spells of each player
        if (p1Spell == Utilities.ColorType.None)
        {
            print("ITS NONE");
            p1Spell = GetCurrentSpell("J1");
        }

        if (p2Spell == Utilities.ColorType.None)
        {
            p2Spell = GetCurrentSpell("J2");
        }
    }

    private Utilities.ColorType GetCurrentSpell(string joystickId)
    {
        print(joystickId + "Blue");
        // Check if any button is pressed and return the correct spell
        if (Input.GetButton(joystickId + "Blue"))
        {

            return Utilities.ColorType.Blue;
            print("BLUE!");
        }
        else if (Input.GetButtonDown(joystickId + "Yellow"))
        {
            return Utilities.ColorType.Yellow;
        }
        else if (Input.GetButtonDown(joystickId + "Red"))
        {
            return Utilities.ColorType.Red;
        }
        else
        {
            print("nothing");
            return Utilities.ColorType.None;
        }
       
    }

    private void UpdateAimDirection()
    {
        // Get the aim axis values
        float aimX = Utilities.GetAveragedAxis("AimX");
        float aimY = Utilities.GetAveragedAxis("AimY");
        
        // Set as the aim direction
        aimDirection = new Vector2(aimX, aimY);
    }

    private void CheckCastSpell()
    {
        // Check to see if either player is trying to cast the spell
        if (Input.GetButtonDown("J1CastSpell") || Input.GetButtonDown("J2CastSpell"))
        {
            // Check to see if either player doesn't have a spell selected
            if (p1Spell == Utilities.ColorType.None || p2Spell == Utilities.ColorType.None)
            {
                // Give some kind of feedback letting them know they need to choose their spells
            }
            else
            {
                CastSpell();
            }
        }
    }

    void CastSpell()
    {
        Utilities.ColorType spell = Utilities.GetCombinedColorType(p1Spell, p2Spell);
        print(string.Format("Casting {0}!", spell));
    }
}
