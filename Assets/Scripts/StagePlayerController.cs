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
        //animator.SetFloat("attackSpeed", speed);
        atkSpeed = speed;
    }


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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
        Debug.DrawRay(GetComponent<Rigidbody2D>().position, Vector3.down * 1.5f, Color.yellow);

        RaycastHit2D rayHit = Physics2D.Raycast(GetComponent<Rigidbody2D>().position, Vector2.down, 1.5f, LayerMask.GetMask("Platform"));

        if (rayHit.collider != null)
        {
            if (rayHit.distance < 1.5f)
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


        //공격
        if (Input.GetKeyDown(KeyCode.X))
        {
            //animator.SetTrigger("PlayerAttack");
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