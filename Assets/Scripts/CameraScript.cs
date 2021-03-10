using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject m_player;
    Vector3 m_cameraPos;
    Vector3 m_initialCameraPosDiff;


    void Start()
    {
        m_player = GameObject.Find("player");
        m_cameraPos = transform.position;
        m_initialCameraPosDiff = m_cameraPos - m_player.transform.position;
    }

    void FixedUpdate()
    {
        transform.position = m_initialCameraPosDiff + new Vector3(m_player.transform.position.x, 0, m_player.transform.position.z);
    }
}
