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

    // Use this for initialization
    void Start ()
    {
        pickedUp = false;
        character = this.gameObject; //sets character gameObject to the object script is attached to
        charRB = character.GetComponent<Rigidbody>(); //finds rigidbody of the character (may need it later on)
        teamMate = GameObject.FindGameObjectWithTag("ranged" + teamNumber); //finds tag "ranged1" if teamNumber is set to 1. 
        teamMateRB = teamMate.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        distanceToTeamMate = Vector3.Distance(transform.position, teamMate.transform.position); //calculates the distance between the two players

        if (Input.GetKeyDown("k") && distanceToTeamMate < 1)
        {
            pickedUp = true;
        }

        if (pickedUp)
        {
            teamMate.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 2, character.transform.position.z);
        }

        if (Input.GetKeyDown("j") && pickedUp)
        {
            teamMateRB.AddForce(teamMate.transform.forward * throwForce);
            teamMateRB.AddForce(teamMate.transform.up * throwForce);
            pickedUp = false;
        }
	}
}
