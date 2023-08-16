using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_Stage1 : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Stage 1-1");
    }
}
