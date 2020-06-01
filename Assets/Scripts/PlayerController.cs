/*
Author: Liam O'Shea (With extensive use of noted references)
File: BlockRotation.cs
Description: This script implements a Player Controller for use in controlling Sherman.
*/

// References
// GamesPlusJames Youtube Channel
// https://www.youtube.com/watch?v=86Bgt--Ww7w
// https://www.youtube.com/watch?v=2akPDnmSfu8

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    private bool doubleJumped;
    private Animator animator;
    private bool flipped = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate(){
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody2D>().velocity.y < -40)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -40f);
        }

        if(grounded)
            doubleJumped = false;

        animator.SetBool("Grounded", grounded);

        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !grounded)
        {
            Jump();
            doubleJumped = true;
        }

        if(Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        if(Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        animator.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

        Vector3 pointDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //if(GetComponent<Rigidbody2D>().velocity.x > 0)
        if(pointDirection.x > gameObject.transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            flipped = false;
        }
        //else if(GetComponent<Rigidbody2D>().velocity.x < 0)
        else if(pointDirection.x < gameObject.transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            flipped = true;
        }
    }

    public void Jump(){
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }

    public bool IsFlipped(){
        return flipped;
    }

}
