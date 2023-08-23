using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Lumin;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StagePlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public float maxSpeed = 3.0f;
    public float jumpPower;


    Vector2 lookDirection = new Vector2(1, 0);

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        //Landing Platform
        Debug.DrawRay(GetComponent<Rigidbody2D>().position, Vector3.down * 1.5f, Color.yellow);

        RaycastHit2D rayHit = Physics2D.Raycast(GetComponent<Rigidbody2D>().position, Vector2.down, 1.5f, LayerMask.GetMask("Platform"));

        if (rayHit.collider != null)
        {
            if (rayHit.distance < 2.0f)
                animator.SetBool("isJumping", false);
                //Debug.Log(rayHit.collider.name);

        }

        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }

        //slide?
        if (Input.GetButtonUp("Horizontal"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.normalized.x * 0.5f, GetComponent<Rigidbody2D>().velocity.y);
        }

        //RunLeft
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
    }
    void FixedUpdate()
    {
        //Move
        float horizontal = Input.GetAxis("Horizontal");

        Vector2 move = new Vector2(horizontal, 0.0f);

        GetComponent<Rigidbody2D>().AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);

        if (GetComponent<Rigidbody2D>().velocity.x > maxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (GetComponent<Rigidbody2D>().velocity.x < maxSpeed * (-1))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed * (-1), GetComponent<Rigidbody2D>().velocity.y);
        }

        if (!Mathf.Approximately(move.x, 0.0f))
        {
            lookDirection.Set(move.x, 0.0f);
            lookDirection.Normalize(); 
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Speed", move.magnitude);

        
    }


}