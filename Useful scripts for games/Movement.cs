using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    private float moveX = 1;
    private bool toggle;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        var x = movement.x;
        var y = movement.y;
        
        
        
        if (animator.GetFloat("Speed") > 0 && toggle == true){
            Debug.Log("Playing footstep");
            
            FindObjectOfType<AudioManager>().Play("FootStep");
            toggle = false;
        }
        if (animator.GetFloat("Speed") == 0 && toggle == false)
        {
            FindObjectOfType<AudioManager>().Stop("FootStep");
            toggle = true;
        }
        
        //animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.x == 0 || moveX == movement.x){
            return;
        }
        
        moveX = movement.x;
        animator.SetFloat("Direction", moveX);
        if (animator.GetFloat("Direction") == -1 && animator.GetFloat("Speed") > 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            
        }
        if (animator.GetFloat("Direction") != -1 && animator.GetFloat("Speed") > 0)
        {
            animator.SetFloat("Horizontal", 1);
            
        }
        if(animator.GetFloat("Speed") == 0){ 
            animator.SetFloat("Horizontal", 0);
            
        }
        
        
        
           
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
    }
}
