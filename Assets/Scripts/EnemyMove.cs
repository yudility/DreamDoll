using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;

    public int nextMove;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5);
    }


    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        /*
        //지형체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.Log("frontVec = "+ frontVec);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Debug.Log("경고! 이 앞은 낭떠러지");
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 5);

        }*/
        
        
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        //Debug.Log("nextMove = "+ nextMove);
        float nextThinkTime = Random.Range(2f, 5f);
        //Debug.Log("nextThinkTime = "+ nextThinkTime);
        Invoke("Think", nextThinkTime);

    }
}
