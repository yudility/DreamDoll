using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StagePlayerController player = col.GetComponent<StagePlayerController>();
            player.nowHp += 20;
            if (player.nowHp > player.maxHp)
                player.nowHp = player.maxHp;
            Destroy(gameObject);
        }
    }


}
