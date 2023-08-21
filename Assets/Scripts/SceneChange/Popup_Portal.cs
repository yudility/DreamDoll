using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Portal : MonoBehaviour
{
    public GameObject Popup;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Popup.SetActive(true);
    }
}
