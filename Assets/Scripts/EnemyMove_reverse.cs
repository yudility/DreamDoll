using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove_reverse : MonoBehaviour
{
    public float movePower = 1f;

    Vector3 movement;

    public int ctratureType;
    public int nextMove;
    bool isTracing = false;
    GameObject traceTarget;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    //체력바
    public GameObject prfHPBar;
    public GameObject canvas;
    RectTransform HPBar;
    public float height = 1.7f;

    void Start()
    {
        //체력바
        HPBar = Instantiate(prfHPBar, canvas.transform).GetComponent<RectTransform>();
    }

    void Update()
    {
        //체력바
        Vector3 _HPBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        HPBar.position = _HPBarPos;
    }

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
                spriteRenderer.flipX = true;
                nextMove = -1;
            }
            else if (playerPos.x > transform.position.x) // 몬스터 | 플레이어 
            {
                spriteRenderer.flipX = false;
                nextMove = 1;

            }
            Invoke("Move", 1);
        }
        else
        {
            if (nextMove == 1)
            {
                spriteRenderer.flipX = true;
                nextMove = -1;

            }
            else if (nextMove == -1)
            {
                spriteRenderer.flipX = false;
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
                spriteRenderer.flipX = true;
                nextMove = -1;

            }
            else if (nextMove == -1)
            {
                spriteRenderer.flipX = false;
                nextMove = 1;

            }
            Move();
        }
    }
}
