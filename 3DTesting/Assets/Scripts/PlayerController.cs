using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Animator anim;
    Vector3 movement;
    Rigidbody rb;


    public float walkspeed = 1f;
    public float runspeed = 2f;
    public int jumpMod = 250;
    public float rotateSpeed = 1f;

    private float distancetoGround = 0.1f;
    private bool running = false;
    private bool inAir = false;
    private bool grounded;
    private Quaternion currentRotation;


	// Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentRotation = Quaternion.identity;
    }
	


	// Update is called once per frame
	void FixedUpdate () {
        Debug.Log("Grounded: " + grounded);
        movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                movement = transform.forward * runspeed * Time.fixedDeltaTime;
                if (!running)
                {
                    anim.SetBool("running", true);
                    running = true;
                }
            }
            else
            {
                movement = transform.forward * walkspeed * Time.fixedDeltaTime;
                if(running)
                {
                    anim.SetBool("running", false);
                    running = false;
                }
            }
            rb.MovePosition(rb.position + movement);
        }

        if(Input.GetKey(KeyCode.D))
        {
            currentRotation = Quaternion.Euler(currentRotation.eulerAngles + Vector3.up * rotateSpeed);
            rb.MoveRotation(currentRotation);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            currentRotation = Quaternion.Euler(currentRotation.eulerAngles - Vector3.up * rotateSpeed);
            rb.MoveRotation(currentRotation);
        }

        if(grounded)
        {
            if (inAir)
            {
                inAir = false;
                anim.SetBool("jump", false);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpMod, ForceMode.Acceleration);
                anim.SetBool("jump", true);
                anim.SetTrigger("jumpstart");
                inAir = true;
            }
        } 
        else if(!grounded && !inAir)
        {
            anim.SetBool("jump", true);
            anim.SetTrigger("jumpstart");
            inAir = true;
        }
        anim.SetFloat("movement", movement.sqrMagnitude);
	}



    void LateUpdate()
    {
        grounded = Physics.Raycast(transform.position + new Vector3(0, 0.05f, 0), Vector3.down, distancetoGround);
        Debug.DrawRay(transform.position + new Vector3(0, 0.05f, 0), Vector3.down * distancetoGround, Color.red);
    }
}
