using Cinemachine;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject tPlayer;
    public Transform tFollowTarget;
    public GameObject sceneConfiner;
    private CinemachineVirtualCamera vcam;
    private CinemachineConfiner cinemachineConfiner;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        cinemachineConfiner = GetComponent<CinemachineConfiner>();
        tPlayer = null;
        sceneConfiner = null;
    }
    // 밑 업데이트가 아닌 start에서 한번만 실행해도 되는게 아닐까?
    void Update()
    {
        if (tPlayer == null)
        {
            tPlayer = GameObject.FindWithTag("Player");
            if (tPlayer != null)
            {
                tFollowTarget = tPlayer.transform;
                vcam.Follow = tFollowTarget;
            }
        }

        if(sceneConfiner == null)
        {
            sceneConfiner = GameObject.Find("cameraConfiner");

            if (sceneConfiner != null)
            {
                cinemachineConfiner.m_BoundingShape2D = sceneConfiner.GetComponent<PolygonCollider2D>();


            }
        }
    }
}