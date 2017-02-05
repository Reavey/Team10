using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementScriptMelee : MonoBehaviour {
    
    Rigidbody playerRB;
    public float runAcceleration;
    public float maxRunSpeed;
    public float jumpHeight;
    bool grounded;
    float turnAmount;
    Quaternion targetRotation;

    // Use this for initialization
    void Start ()
    {
        playerRB = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerRB.velocity.magnitude);
            PlayerMovement();
    }

    void PlayerMovement()
    {
        playerRB.AddForce(Input.GetAxis("Horizontal") * runAcceleration, 0, Input.GetAxis("Vertical") * runAcceleration);
        transform.LookAt(transform.position + new Vector3(Input.GetAxis("Horizontal") * runAcceleration, 0, Input.GetAxis("Vertical") * runAcceleration));

        transform.rotation = new Quaternion(this.transform.rotation.x, 180, this.transform.rotation.z, 0);
        if (transform.rotation != Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))))
        {
            print(Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))));

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
