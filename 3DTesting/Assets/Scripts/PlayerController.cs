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

    [SerializeField]
    private float itemInteractDistance = 2f;

    private float distancetoGround = 0.1f;
    private float moddedGroundDist;
    private bool running = false;
    private bool inAir = false;
    private bool grounded;
    private Quaternion currentRotation;

    private KeyCode up = KeyCode.W;
    private KeyCode down = KeyCode.S;
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode jump = KeyCode.Space;
    private KeyCode run = KeyCode.LeftShift;
    private KeyCode interact = KeyCode.E;

    private DialogueTree currentTalker;

    private RaycastHit groundedRay;

	// Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentRotation = Quaternion.identity;
    }
	
    void ManageTalker()
    {
        Ray talkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = talkRay.origin + (talkRay.direction);
        RaycastHit outHit;
        Physics.Raycast(talkRay.origin, talkRay.direction, out outHit);
        Interactable interactee = null;
        if (outHit.collider != null)
        {
            interactee = outHit.collider.gameObject.GetComponent<Interactable>();
            if(interactee != null)
            {
                if (interactee.GetType() == typeof(ItemInteractable) && outHit.distance > itemInteractDistance) return;
                if (Input.GetKeyDown(interact))
                {
                    interactee.Enact();
                }
            }
        }
    }


	// Update is called once per frame
	void FixedUpdate () {
        movement = Vector3.zero;
        //Get talkable 
        ManageTalker();


        //Movement
        if (GameManager.manager.PlayerCanMove)
        {
            //First check if running
            if(Input.GetKey(run))
            {
                if(!running)
                {
                    anim.SetBool("running", true);
                    running = true;
                }
            }
            else
            {
                if (running)
                {
                    anim.SetBool("running", false);
                    running = false;
                }
            }

            //Next check movement
            if (Input.GetKey(up))
            {
                movement = running ? transform.forward * runspeed : transform.forward * walkspeed;
            }
            else if (Input.GetKey(down))
            {
                movement = running ? -transform.forward * runspeed: -transform.forward * walkspeed;
            }

            if(Input.GetKey(left))
            {
                currentRotation = Quaternion.Euler(currentRotation.eulerAngles - Vector3.up * rotateSpeed);

                rb.MoveRotation(currentRotation);
            }
            else if (Input.GetKey(right))
            {
                currentRotation = Quaternion.Euler(currentRotation.eulerAngles + Vector3.up * rotateSpeed);

                rb.MoveRotation(currentRotation);
            }
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
            //Next check grounding rules.
            if (grounded)
            {
                if(rb.useGravity)
                    rb.useGravity = false;
                if(inAir)
                {
                    anim.SetBool("jump", false);
                    GameManager.manager.PlayerCanMove = false;
                }
                else
                {
                    rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y,-999,0), rb.velocity.z);
                }

                if (Input.GetKey(KeyCode.Space) && GameManager.manager.PlayerCanMove)
                {
                    rb.AddForce(Vector3.up * jumpMod, ForceMode.Acceleration);
                    anim.SetBool("jump", true);
                    anim.SetTrigger("jumpstart");
                    inAir = true;
                }
            }
            else
            {
                if (!rb.useGravity)
                    rb.useGravity = true;
            }

         }

        anim.SetFloat("movement", movement.sqrMagnitude);
	}

    IEnumerator EndJump()
    {
        yield return new WaitForSeconds(0.53f); //Stupid fucking magic numbers because imported animations are awful.
        GameManager.manager.PlayerCanMove = true;
        inAir = false;
    }
    public void AnimationEndJump()
    {
        GameManager.manager.PlayerCanMove = true;
        inAir = false;
    }

    void LateUpdate()
    {
        //moddedGroundDist = -rb.velocity.y + distancetoGround;
        moddedGroundDist = distancetoGround;
        grounded = Physics.Raycast(transform.position + new Vector3(0, 0.05f, 0), Vector3.down,out groundedRay ,moddedGroundDist,~4); 
        // ~4 is all but ignore raycast layer
        Debug.DrawRay(transform.position + new Vector3(0, 0.05f, 0), Vector3.down * moddedGroundDist, Color.red);
    }
}
