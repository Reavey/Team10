using UnityEngine;
using System.Collections;

public class MovementScriptRanged : MonoBehaviour
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
        transform.rotation = new Quaternion(this.transform.rotation.x, 180, this.transform.rotation.z, 0);
        playerRB.AddForce(Input.GetAxis("RightX") * runAcceleration, 0, Input.GetAxis("RightY") * runAcceleration);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            playerRB.AddForce(0, jumpHeight, 0);
        }

        if (!grounded)
        {
            //Insert Controls in air
        }

        // HAVEN'T DELETED THEM JUST IN CASE OTHER ROTATION DOESN'T WORK AS INTENDED
        //transform.LookAt(transform.position + new Vector3(Input.GetAxis("RightX") * runAcceleration, 0, Input.GetAxis("RightY") * runAcceleration));
        //if (transform.rotation != Quaternion.LookRotation(new Vector3(Input.GetAxis("RightX"), 0, Input.GetAxis("RightY"))))
        //{
        //    //print(Quaternion.LookRotation(new Vector3(Input.GetAxis("RightX"), 0, Input.GetAxis("RightY"))));

        //}
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