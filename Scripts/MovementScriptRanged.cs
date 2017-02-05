using UnityEngine;
using System.Collections;

public class MovementScriptRanged : MonoBehaviour {

    Rigidbody playerRB;
    public float runAcceleration;
    public float maxRunSpeed;
    public float jumpHeight;
    bool grounded;
    float turnAmount;
    Quaternion targetRotation;

    // Use this for initialization
    void Start()
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
        transform.rotation = new Quaternion(this.transform.rotation.x, 180, this.transform.rotation.z, 0);
        playerRB.AddForce(Input.GetAxis("RightX") * runAcceleration, 0, Input.GetAxis("RightY") * runAcceleration);
        transform.LookAt(transform.position + new Vector3(Input.GetAxis("RightX") * runAcceleration, 0, Input.GetAxis("RightY") * runAcceleration));
        if (transform.rotation != Quaternion.LookRotation(new Vector3(Input.GetAxis("RightX"), 0, Input.GetAxis("RightY"))))
        {
            print(Quaternion.LookRotation(new Vector3(Input.GetAxis("RightX"), 0, Input.GetAxis("RightY"))));

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
