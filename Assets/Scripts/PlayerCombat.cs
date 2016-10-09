using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour {
    public GameObject SpellPrefab;

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
	}

    private void GetInput()
    {
        UpdateSpells();
        UpdateAimDirection();
        CheckCastSpell();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            p1Spell = Utilities.ColorType.Red;
            p2Spell = Utilities.ColorType.Blue;
            CastSpell();
            ResetSpells();
        }
    }

    private void UpdateSpells()
    {
        // Update the spells of each player
        Utilities.ColorType spell = GetCurrentSpell("J1");
        if (spell != Utilities.ColorType.None)
        {
            p1Spell = spell;
        }

        spell = GetCurrentSpell("J2");
        if (spell != Utilities.ColorType.None)
        {
            p2Spell = spell;
        }
    }

    private Utilities.ColorType GetCurrentSpell(string joystickId)
    {
        // Check if any button is pressed and return the correct spell
        if (Input.GetButton(joystickId + "Blue"))
        {
            print(string.Format("{0} set to Blue", joystickId));
            return Utilities.ColorType.Blue;
        }
        else if (Input.GetButtonDown(joystickId + "Yellow"))
        {
            print(string.Format("{0} set to Yellow", joystickId));
            return Utilities.ColorType.Yellow;
        }
        else if (Input.GetButtonDown(joystickId + "Red"))
        {
            print(string.Format("{0} set to Red", joystickId));
            return Utilities.ColorType.Red;
        }
        else
        {
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
                print("Both players must have a spell chosen.");
            }
            else
            {
                CastSpell();
                ResetSpells();
            }
        }
    }

    void CastSpell()
    {
        Utilities.ColorType spellColor = Utilities.GetCombinedColorType(p1Spell, p2Spell);
        print(string.Format("Casting {0}!", spellColor));
        GameObject spellObject = (GameObject)Instantiate(SpellPrefab, this.gameObject.transform.position, Quaternion.identity);
        Spell spell = spellObject.GetComponent<Spell>();
        spell.SetColor(spellColor);
        spell.SetDirection(aimDirection);
    }

    void ResetSpells()
    {
        p1Spell = Utilities.ColorType.None;
        p2Spell = Utilities.ColorType.None;
    }
}
