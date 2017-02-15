using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementScriptMelee : MonoBehaviour
{

    Rigidbody playerRB;
    public float runAcceleration;
    public float maxRunSpeed;
    public float jumpHeight;
    bool grounded;
    float turnAmount;
    Quaternion targetRotation;
    float angle;

    // Use this for initialization
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerRB.velocity.magnitude);
        PlayerMovement();
        PlayerRotation();
    }

    void PlayerMovement()
    {
        if(Input.GetAxis("Horizontal") != 0|| Input.GetAxis("Vertical") != 0)
        {

            playerRB.AddForce(Input.GetAxis("Horizontal") * runAcceleration, 0, Input.GetAxis("Vertical") * runAcceleration);
            //transform.LookAt(transform.position + new Vector3(Input.GetAxis("Horizontal") * runAcceleration, 0, Input.GetAxis("Vertical") * runAcceleration));
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            playerRB.AddForce(0, jumpHeight, 0);
        }

        if (!grounded)
        {
            //Insert Controls in air
        }
    }

    void PlayerRotation()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)  //stops the players rotation being re-set when there is no input from the stick
        {
            angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg; // gets the angle the stick is facing
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0)); //sets the models angle to the angle the stick is facing
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    void OnCollisionExit()
    {
        grounded = false;
    }
}