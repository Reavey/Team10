using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCharacter : MonoBehaviour {

    GameObject character;
    GameObject teamMate;
    Rigidbody charRB; // Assigned for future use if we do power ups? 
    Rigidbody teamMateRB;
    public int teamNumber;
    bool pickedUp;
    public int throwForce;
    float distanceToTeamMate;
    public int health;
    public int minDamage;
    public int maxDamage;
    int damage;
    float attackTime; //going to use it to store time of attack 
    public float attackSpeed; //the attack speed of the target (how much we wait between attacks)
    GameObject[] meleeChar = new GameObject[8];
    GameObject[] rangedChar = new GameObject[8];
    MeleeCharacter[] meleeCharScript = new MeleeCharacter[8]; //to store all the characters' scripts
    RangedCharacter[] rangedCharScript = new RangedCharacter[8]; //storing all the characters' scripts
    bool isAlive;
    

    // Use this for initialization
    void Start ()
    {
        pickedUp = false;
        character = this.gameObject; //sets character gameObject to the object script is attached to
        charRB = character.GetComponent<Rigidbody>(); //finds rigidbody of the character (may need it later on)
        teamMate = GameObject.FindGameObjectWithTag("ranged" + teamNumber); //finds tag "ranged1" if teamNumber is set to 1. 
        teamMateRB = teamMate.GetComponent<Rigidbody>();

        //Stores all scripts for all players
        for (int i = 0 ; i<1 ; i++)
        {
            meleeChar[i] = GameObject.FindWithTag("melee" + i); //finds the player
            meleeCharScript[i] = meleeChar[i].GetComponent<MeleeCharacter>(); //uses script
            rangedChar[i] = GameObject.FindWithTag("ranged" + i); //finds the player
            rangedCharScript[i] = rangedChar[i].GetComponent<RangedCharacter>(); //uses script
        }
        isAlive = true;
        attackTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isAlive) //player can't interact with anything if not alive
        {
            if (rangedCharScript[teamNumber].health > 0) //in case team mate is not alive - doesn't calculate distance
            {
                distanceToTeamMate = Vector3.Distance(transform.position, teamMate.transform.position); //calculates the distance between the two players
            }

            if (Input.GetButtonDown("Fire1") && distanceToTeamMate < 1)
            {
                pickedUp = true; 
            }

            if (pickedUp)
            {
                teamMate.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 2, character.transform.position.z); //sets position of team mate above you
            }

            if (Input.GetKeyDown("j") && pickedUp)
            {
                teamMateRB.AddForce(teamMate.transform.forward * throwForce); //throws it forward
                teamMateRB.AddForce(0, throwForce, 0); //throws it up
                pickedUp = false;
            }

            if(Input.GetKeyDown("h"))
            {
                //this is here so players can't spam h to always hit a target (void OnTriggerStay is used only when inside a trigger - timeout for hit has to be added if you click it outside of it as well.
                attackTime = Time.time; 
            }
        }

        if(health <= 0)
        {
            isAlive = false;
        }
	}

    private void OnTriggerStay (Collider other)
    {
        if (Input.GetKeyDown("h") && Time.time > attackTime + attackSpeed)
        {
            for (int i = 0; i > 8; i++) //goes through all characters to see which he has hit
            {
                if (other.gameObject == meleeChar[i])
                {
                    damage = Random.Range(minDamage, maxDamage); //it's here so if we hit 2 players - they're hit for different damage ==> chooses a random number between min and max dmg
                    meleeCharScript[i].health -= damage;
                }

                if (other.gameObject == rangedChar[i])
                {
                    damage = Random.Range(minDamage, maxDamage); //as above
                    rangedCharScript[i].health -= damage;
                }
            }
        }
    }
}
