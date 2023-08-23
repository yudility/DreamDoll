using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Object that entered the trigger : " + other);

        /*
        //플레이어 컨트롤러로 바꾸기
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeHealth(1);
            Destroy(gameObject);
        }*/
    }


}
