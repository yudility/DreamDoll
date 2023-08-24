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
    // �� ������Ʈ�� �ƴ� start���� �ѹ��� �����ص� �Ǵ°� �ƴұ�?
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