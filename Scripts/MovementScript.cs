using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementScript : MonoBehaviour {

    GameObject player;
    Transform mainCamera; //position,rotation of the main camera
    Vector3 cameraForward; //current forward view of camera
    Rigidbody playerRB;
    public float runAcceleration;
    public float decelerationSlide; // This value controls how much momentum affects deceleration. If 1 - it's as default, if < 1 character slides less, if > 1 character slides more.
    public float maxRunSpeed;
    public float jumpHeight;
    bool grounded;
    Vector3 move;
    float turnAmount;
    Quaternion targetRotation;

    // Use this for initialization
    void Start ()
    {
        targetRotation = transform.rotation;
        player = this.gameObject;
        playerRB = player.GetComponent<Rigidbody>();
        mainCamera = Camera.main.transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(playerRB.velocity.magnitude);

        playerRB.velocity = Vector3.ClampMagnitude(playerRB.velocity, maxRunSpeed);

        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && grounded)
        {
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);
            cameraForward = Vector3.Scale(mainCamera.forward, new Vector3(1, 0, 1)).normalized;
            move = Input.GetAxis("Vertical") * cameraForward + Input.GetAxis("Horizontal") * mainCamera.right;
            playerRB.AddForce(move *= 20);
            //Rotation doesn't work properly. Have to try Lerp. 
            turnAmount = Mathf.Atan2(move.x, move.z);
            transform.Rotate(0, turnAmount, 0);
        }
        else
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x * decelerationSlide, playerRB.velocity.y, playerRB.velocity.z * decelerationSlide); // Have to add time.deltatime calculation for frame differences
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

    void OnCollisionStay()
    {
        grounded = true;
    }

    void OnCollisionExit()
    {
        grounded = false;
    }
}
