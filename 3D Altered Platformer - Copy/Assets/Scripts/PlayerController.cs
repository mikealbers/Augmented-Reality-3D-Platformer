using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;
    private Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;
    public CharacterController controller;
    public GameObject playerModel;

    //public Rigidbody rb;

	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody> ();
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        /*rb.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }*/

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * CrossPlatformInputManager.GetAxisRaw("Vertical")) + (transform.right * CrossPlatformInputManager.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;


        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        //Move the player in different directions based on camera look direction
        //if (CrossPlatformInputManager.GetAxisRaw("Horizontal") != 0 || CrossPlatformInputManager.GetAxisRaw("Vertical") !=0)
        //{
        //    transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
        //    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
        //    playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        //}

        //Change animations
        anim.SetBool("IsGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(CrossPlatformInputManager.GetAxisRaw("Vertical")) + Mathf.Abs(CrossPlatformInputManager.GetAxisRaw("Horizontal"))));
    }
}
