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
    public float forceMod = 75;

    private float distancetoGround = 0.1f;
    private float moddedGroundDist;
    private bool running = false;
    private bool inAir = false;
    private bool grounded;
    private bool canMove = true;
    private Quaternion currentRotation;
    private uint mask;

    private DialogueTree currentTalker;

    private List<Item> inventory;
    public List<Item> Inventory
    {
        get { return inventory; }
    }

	// Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentRotation = Quaternion.identity;
    }
	

	// Update is called once per frame
	void FixedUpdate () {
        movement = Vector3.zero;

        //Get talkable 
        //TODO: Put this in its own thing.
        Ray talkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = talkRay.origin + (talkRay.direction);
        RaycastHit outHit;
        Physics.Raycast(talkRay.origin, talkRay.direction, out outHit);
        DialogueTree talker = null;
        if (outHit.collider != null)
        {
            talker = outHit.collider.gameObject.GetComponent<DialogueTree>();
        }

        currentTalker = talker;
        Debug.Log(currentTalker);
        if ( Input.GetKeyDown(KeyCode.E))
        {
            if(talker != null)
            {
                talker.StartTalking();
            }
        }


        //Movement
        if (canMove)
        {
            if (Input.GetKey(KeyCode.W)) //Forward
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    movement = transform.forward * runspeed * forceMod;
                    if (!running)
                    {
                        anim.SetBool("running", true);
                        running = true;
                    }
                }
                else
                {
                    movement = transform.forward * walkspeed * forceMod;
                    if (running)
                    {
                        anim.SetBool("running", false);
                        running = false;
                    }
                }
                rb.AddForce(movement);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                movement = -transform.forward * walkspeed * forceMod;
                rb.AddForce(movement);
            }

            if (Input.GetKey(KeyCode.D)) //Right
            {
                currentRotation = Quaternion.Euler(currentRotation.eulerAngles + Vector3.up * rotateSpeed);
                rb.MoveRotation(currentRotation);
            }
            else if (Input.GetKey(KeyCode.A)) //Left
            {
                currentRotation = Quaternion.Euler(currentRotation.eulerAngles - Vector3.up * rotateSpeed);
                rb.MoveRotation(currentRotation);
            }

            //Do some raycasting to prevent flypaper walls
            Vector3 hMovement = rb.velocity;
            hMovement.y = 0;
            float distance = hMovement.magnitude;
            hMovement.Normalize();
            RaycastHit wallHit;
            Debug.DrawRay(transform.position + new Vector3(0, 0.3f, 0), hMovement, Color.cyan);
            if (rb.SweepTest(hMovement, out wallHit, distance))
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }
        if(grounded)
        {
            if (inAir)
            {
                anim.SetBool("jump", false);
                canMove = false;
            }

            if (Input.GetKey(KeyCode.Space) && canMove)
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

    IEnumerator EndJump()
    {
        yield return new WaitForSeconds(0.53f); //Stupid fucking magic numbers because imported animations are awful.
        canMove = true;
        inAir = false;
    }
    public void AnimationEndJump()
    {
        canMove = true;
        inAir = false;
    }

    void LateUpdate()
    {
        moddedGroundDist = -rb.velocity.y + distancetoGround;
        moddedGroundDist = Mathf.Clamp(moddedGroundDist, -distancetoGround * 2, distancetoGround - (distancetoGround * rb.velocity.y));
        grounded = Physics.Raycast(transform.position + new Vector3(0, 0.05f, 0), Vector3.down, moddedGroundDist,~4); // ~4 is all but ignore raycast layer
        Debug.DrawRay(transform.position + new Vector3(0, 0.05f, 0), Vector3.down * moddedGroundDist, Color.red);
    }
}
