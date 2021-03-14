using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject m_player;
    GameObject m_hiddenObject; // created for simulate to transform and rotation of player;


    void Start()
    {
        m_player = GameObject.Find("player");
        m_hiddenObject = new GameObject();
        m_hiddenObject.transform.position = transform.position;
        m_hiddenObject.transform.rotation = transform.rotation;
        m_hiddenObject.transform.parent = m_player.transform;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(m_hiddenObject.transform.position.x, transform.position.y, m_hiddenObject.transform.position.z);
        transform.rotation = m_hiddenObject.transform.rotation;
    }
}