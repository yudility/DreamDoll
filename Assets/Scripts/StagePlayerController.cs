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
    Rigidbody2D rigidbody;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public float maxSpeed = 3.0f;
    public float jumpPower;
    
    Vector2 lookDirection = new Vector2(1, 0);

    //체력바
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed = 1;
    public bool attacked = false;
    public Image nowHpbar;
   
    
    // 체력바
    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }
    void SetAttackSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
        atkSpeed = speed;
    }


    

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //체력바
        maxHp = 100;
        nowHp = 100;
        atkDmg = 2;

        SetAttackSpeed(1.5f);

    }
    void Update()
    {
        //체력바
        nowHpbar.fillAmount = (float) nowHp / (float) maxHp;

        //Landing Platform
        Debug.DrawRay(rigidbody.position, Vector3.down * 1.5f, Color.yellow);

        RaycastHit2D rayHit = Physics2D.Raycast(rigidbody.position, Vector2.down, 1.5f, LayerMask.GetMask("Platform"));

        if (rayHit.collider != null)
        {
            if (rayHit.distance < 2.0f)
                animator.SetBool("isJumping", false);
                //Debug.Log(rayHit.collider.name);

        }

        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }

        //slide?
        if (Input.GetButtonUp("Horizontal"))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.normalized.x * 0.5f, rigidbody.velocity.y);
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

        rigidbody.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);

        if (rigidbody.velocity.x > maxSpeed)
        {
            rigidbody.velocity = new Vector2(maxSpeed, rigidbody.velocity.y);
        }
        else if (rigidbody.velocity.x < maxSpeed * (-1))
        {
            rigidbody.velocity = new Vector2(maxSpeed * (-1), rigidbody.velocity.y);
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