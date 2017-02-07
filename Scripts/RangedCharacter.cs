using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedCharacter : MonoBehaviour {

    GameObject character;
    GameObject teamMate;
    Rigidbody charRB; // Assigned for future use if we do power ups? 
    Rigidbody teamMateRB;
    public int teamNumber;
    public int health;
    float attackTime; //going to use it to store time of attack 
    public float attackSpeed; //the attack speed of the target (how much we wait between attacks)
    public int hitPower;
    GameObject[] meleeChar = new GameObject[8];
    GameObject[] rangedChar = new GameObject[8];
    MeleeCharacter[] meleeCharScript = new MeleeCharacter[8]; //to store all the characters' scripts
    RangedCharacter[] rangedCharScript = new RangedCharacter[8]; //storing all the characters' scripts
    bool isAlive;
    GameObject hitObject;

    // Use this for initialization
    void Start ()
    {
        character = this.gameObject; //sets character gameObject to the object script is attached to
        charRB = character.GetComponent<Rigidbody>(); //finds rigidbody of the character (may need it later on)
        teamMate = GameObject.FindGameObjectWithTag("melee" + teamNumber); //finds tag "melee1" if teamNumber is set to 1. 
        teamMateRB = teamMate.GetComponent<Rigidbody>();

        //Stores all scripts for all players
        for (int i = 0; i > 8; i++)
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
		if(isAlive)
        {
            if (Input.GetKeyDown("h"))
            {
                Attack();
            }
        }

        if (health <= 0)
        {
            isAlive = false;
        }
    }

    void Attack()
    {
        if (Time.time > attackTime + attackSpeed)
        {
            GameObject hitGameObject = Instantiate(hitObject, transform.position, transform.rotation) as GameObject;
            Rigidbody rigidbody = hitGameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(Vector3.forward * hitPower); //this throws it forward, we may want to throw it up as well, add another line if so
            attackTime = Time.time;
        }
    }
}
