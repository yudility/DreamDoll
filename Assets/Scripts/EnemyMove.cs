using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    // 몬스터 이동
    public int nextMove;
    bool isTracing = false;
    GameObject traceTarget;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    // 체력바
    public GameObject prfHPBar;
    public GameObject canvas;
    RectTransform HPBar;
    public float height = 1.7f;

    // 피격
    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public int atkSpeed;

    // 피격
    private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, int _atkSpeed)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
    }

    //플레이어 스크립트 이름 바꾸기

    //public Sword_Man player;
    Image nowHpbar;
    void Start()
    {
        //체력바
        HPBar = Instantiate(prfHPBar, canvas.transform).GetComponent<RectTransform>();

        //캐릭별 스텟조정
        if(name.Equals("slime"))
        {
            SetEnemyStatus("slime", 3, 1, 1);
        }
        else if (name.Equals("skeleton"))
        {
            SetEnemyStatus("skeleton", 5, 2, 1);
        }

        //체력바 조정
        nowHpbar = HPBar.transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {   
        //체력바
        Vector3 _HPBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        HPBar.position = _HPBarPos;

        //체력바 조정
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
    }



    



    // 몬스터 이동
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
        if (collision.CompareTag("Player"))
        {
            traceTarget = collision.gameObject;
            isTracing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
