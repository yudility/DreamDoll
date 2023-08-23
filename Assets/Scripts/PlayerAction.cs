using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerAction : MonoBehaviour
{
    public StagePlayerController controller;
    public Transform weaponPosition;

    public GameObject hitBoxCollider;
    public GameObject weaponCollider;

    public SpriteRenderer[] sprite;

    public bool equipWeapon;
    bool invincible;


    private void OnTriggerEnter2D(Collider2D collision) //damaged
    {
        
    }
    public void WeaponColliderOnOff()
    {
        if (equipWeapon)
        {
            weaponCollider.SetActive(!weaponCollider.activeInHierarchy);
        }
    }

    public void EquipWeapon()
    {
        weaponCollider.transform.position = weaponPosition.position;
    }

    


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
