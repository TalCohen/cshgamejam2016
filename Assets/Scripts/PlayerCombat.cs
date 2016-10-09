using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour {
    public GameObject SpellPrefab;

    private Utilities.ColorType j1Spell;
    private Utilities.ColorType j2Spell;

    private Vector2 aimDirection;

    private bool isAlive;
    private int health;

    private bool isInvincible;
    private float invincibilityTimer;
    private float nextIFrameChange;

    private static float IFRAME_CHANGE_RATIO = 0.3f;
    private static float MAX_INVINCIBILITY_TIME = 2.0f;

    // So we don't have to get it so often
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    
    private int direction;
    private bool directionChanged;
    
    private static int LEFT_DIRECTION = 0;
    private static int UP_DIRECTION = 1;
    private static int RIGHT_DIRECTION = 2;
    private static int DOWN_DIRECTION = 3;

	// Use this for initialization
	void Start () {
        j1Spell = Utilities.ColorType.None;
        j2Spell = Utilities.ColorType.None;
	}
	
	// Update is called once per frame
	void Update () {
        CheckInvincibility();
        GetInput();
	}

    void Awake()
    {
        isAlive = true;
        health = 100;
        isInvincible = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
        direction = -1;
        directionChanged = false;
    }

    private void ResetInvincibility()
    {
        //StartCoroutine("ResetInvincibilityHelper");
        isInvincible = true;
        invincibilityTimer = 0.0f;
        nextIFrameChange = 0.0f;
    }

    private void CheckInvincibility()
    {
        if (isInvincible)
        {
            // Subtract the time from the timer
            invincibilityTimer += Time.deltaTime;

            // Check to see if we should still be invincible
            if (invincibilityTimer < MAX_INVINCIBILITY_TIME)
            {
                // If we should change our iframe
                if (invincibilityTimer > nextIFrameChange)
                {
                    FlashSprite();
                }

            }
            else
            {
                isInvincible = false;
                Color oldColor = spriteRenderer.color;
                spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1);
            }
        }
    }

    private void FlashSprite()
    {
        nextIFrameChange += Mathf.Max(0.1f, IFRAME_CHANGE_RATIO * (1 - (invincibilityTimer/MAX_INVINCIBILITY_TIME)));
        Color oldColor = spriteRenderer.color;
        spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1-oldColor.a);
    }

    private void GetInput()
    {
        UpdateSpells();
        UpdateAimDirection();
        CheckCastSpell();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CastSpell(Utilities.ColorType.Red);
            ResetSpells();
        }
    }

    private void UpdateSpells()
    {
        // Update the spells of each joystick
        Utilities.ColorType spell = GetCurrentSpell("J1");
        if (spell != Utilities.ColorType.None)
        {
            j1Spell = spell;
        }

        spell = GetCurrentSpell("J2");
        if (spell != Utilities.ColorType.None)
        {
            j2Spell = spell;
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
        aimDirection = new Vector2(aimX, aimY).normalized;

        // If they are not actively aiming in any direction
        if (aimDirection == Vector2.zero)
        {
            // Set it to the current animator direction
            switch (direction)
            {
                case LEFT_DIRECTION:
                    aimDirection = new Vector2(-1, 0);
                    break;
                case UP_DIRECTION:
                    aimDirection = new Vector2(0, 1);
                case RIGHT_DIRECTION:
                    aimDirection = new Vector2(1, 0);
                case DOWN_DIRECTION:
                    aimDirection = new Vector2(0, -1);
            }
        }

        // Get which direction is our dominant one
        int newDirection = GetDominantDirection();
        
        // Attempt to change the direction
        ChangeDirection(newDirection);
    }

    private int GetDominantDirection()
    {
        if (aimDirection.x > aimDirection.y)
        {
            if (aimDirection.x > 0)
            {
                return RIGHT_DIRECTION;
            } else
            {
                return LEFT_DIRECTION;
            }
        } else
        {
            if (aimDirection.y > 0)
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

    private void CheckCastSpell()
    {
        // Check to see if either joystick is trying to cast the spell
        if (Input.GetButtonDown("J1CastSpell") || Input.GetButtonDown("J2CastSpell"))
        {
            // Check to see if either joystick doesn't have a spell selected
            if (j1Spell == Utilities.ColorType.None || j2Spell == Utilities.ColorType.None)
            {
                // Give some kind of feedback letting them know they need to choose their spells
                print("Both players must have a spell chosen.");
            }
            else
            {
                // Get the color of the spell (combination of each joystick's chosen color)
                Utilities.ColorType spellColorType = Utilities.GetCombinedColorType(j1Spell, j2Spell);

                // Cast the spell of that color
                CastSpell(spellColorType);

                // Reset the joystick's chosen spells
                ResetSpells();
            }
        }
    }

    private void CastSpell(Utilities.ColorType spellColorType)
    {
        print(string.Format("Casting {0}!", spellColorType));

        // Create the spell object
        GameObject spellObject = (GameObject)Instantiate(SpellPrefab, this.gameObject.transform.position, Quaternion.identity);
        Spell spell = spellObject.GetComponent<Spell>();
        spell.SetColorType(spellColorType);
        spell.SetDirection(aimDirection);
    }

    private void ResetSpells()
    {
        j1Spell = Utilities.ColorType.None;
        j2Spell = Utilities.ColorType.None;
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
        if (other.tag == "Enemy")
        {
            // Hit by enemy, lose health
            Enemy enemy = other.GetComponent<Enemy>();
            TakeDamage(enemy.damage);
            isInvincible = true;
        }
        else if (other.tag == "Spikes")
        {
            // Hit by spikes, lose health
            Spikes spikes = other.GetComponent<Spikes>();
            TakeDamage(spikes.damage);
        }
    }

    private void TakeDamage(int damage)
    {
        // Make sure we aren't invincible
        if (isInvincible)
        {
            return;
        }

        // Lose some health
        health -= damage;
        print("Lost health...");
    

        // Check to see if we're alive
        if (health > 0)
        {
            // Still alive - make immune to damage for a bit
            ResetInvincibility();
        } else
        {
            // Oh no! We've died!
            isAlive = false;
            transform.position = new Vector3();
            print("We've died!");
        }
    }
}
