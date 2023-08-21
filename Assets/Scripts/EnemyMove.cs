using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float movePower = 1f;

    Vector3 movement;
    
    public int ctratureType;
    public int nextMove;
    bool isTracing = false;
    GameObject traceTarget;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    void Awake()
    {
        nextMove = -1;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        Move();
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
    }

    void Move()
    {
        Debug.Log("nextMove = " + nextMove);
        Debug.Log("isTracing = " + isTracing);

        if (isTracing)
        {
            Vector3 playerPos = traceTarget.transform.position;

            if (playerPos.x < transform.position.x) // 플레이어 | 몬스터
            {
                spriteRenderer.flipX = false;
                nextMove = -1;
            }
            else if (playerPos.x > transform.position.x) // 몬스터 | 플레이어 
            {
                spriteRenderer.flipX = true;
                nextMove = 1;
                
            }
            Invoke("Move", 1);
        }
        else
        {
            if (nextMove == 1)
            {
                spriteRenderer.flipX = false;
                nextMove = -1;

            }
            else if (nextMove == -1)
            {
                spriteRenderer.flipX = true;
                nextMove = 1;

            }
            Invoke("Move", 3);
        }
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            traceTarget = collision.gameObject;
            isTracing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTracing = false;
            if (nextMove == 1)
            {
                spriteRenderer.flipX = false;
                nextMove = -1;

            }
            else if (nextMove == -1)
            {
                spriteRenderer.flipX = true;
                nextMove = 1;

            }
            Move();
        }
    }
    

    /* 
    // 원래 스크립트
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public int nextMove;
   
    void Awake()
    {
        nextMove = -1;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 1);
    }


    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

    }

    void Think()
    {
        Debug.Log("nextMove = " + nextMove);

        if (nextMove == 1) {
            nextMove = -1;
            spriteRenderer.flipX = false;
        }
        else if (nextMove == -1) {
            nextMove = 1;
            spriteRenderer.flipX = true;
        }
   
        Invoke("Think", 3);
    }
    */
   
}
